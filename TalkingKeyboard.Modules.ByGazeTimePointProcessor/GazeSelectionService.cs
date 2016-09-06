using System;
using System.Collections.Concurrent;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.DataContainers;
using TalkingKeyboard.Infrastructure.Enums;
using TalkingKeyboard.Infrastructure.Helpers;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters;

namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor
{
    public class GazeSelectionService : IControlActivationService
    {
        private readonly AveragingFilter _averagingFilter;
        private readonly IUnityContainer _container;
        private readonly ConcurrentDictionary<SelectableControl, SelectableButtonViewModel> _dataPerControl;
        private readonly TimedPoints _timedPoints;

        public GazeSelectionService(IEventAggregator eventAggregator, IUnityContainer container)
        {
            _container = container;
            _timedPoints = new TimedPoints(Configuration.PointKeepAliveTimeSpan);
            _averagingFilter = new AveragingLastNpointsWithinTimeSpanFilter(3, TimeSpan.FromMilliseconds(75));
            _dataPerControl = new ConcurrentDictionary<SelectableControl, SelectableButtonViewModel>();
            Windows.Add(Application.Current.MainWindow);

            eventAggregator.GetEvent<NewCoordinateEvent>().Subscribe(ProcessPoint);
            Application.Current.MainWindow.Closing +=
                (sender, args) => eventAggregator.GetEvent<NewCoordinateEvent>().Unsubscribe(ProcessPoint);
        }

        /// <summary>
        ///     Checks whether the point falls on a control and updates any relevant information.
        /// </summary>
        /// <remarks>
        ///     The duration a control has been gazed at depends on the last time it was seen and the time established as a maximum
        ///     to keep alive an inactive gaze.
        /// </remarks>
        /// <param name="p"></param>
        public void ProcessPoint(Point p)
        {
            _timedPoints.Maintain();
            _timedPoints.AddPoint(p);
            var nullablePoint = _averagingFilter.Filter(_timedPoints);
            if (nullablePoint == null) return;
            var point = nullablePoint.Value;
            foreach (var window in Windows)
            {
                var seenControl = HitTestHelper.SelectableControlUnderPoint(point, window);

                foreach (var cd in _dataPerControl)
                {
                    var controlData = cd.Value;
                    var control = cd.Key;

                    var oldestAcceptable = DateTime.Now - controlData.GazeKeepAliveTimeSpan;
                    controlData.CurrentGazeTimeSpan = oldestAcceptable > controlData.LastSeenTime
                        ? TimeSpan.Zero
                        : controlData.CurrentGazeTimeSpan + (DateTime.Now - controlData.LastSeenTime);

                    if (!control.Equals(seenControl))
                    {
                        StateMachineUpdateForOtherControls(control, controlData, window);
                    }
                    else
                    {
                        controlData.LastSeenTime = DateTime.Now;
                    }
                }

                if (seenControl == null) continue;
                SelectableButtonViewModel data = null;
                if (!_dataPerControl.TryGetValue(seenControl, out data))
                {
                    window.Dispatcher.Invoke(() => data = seenControl.DataContext as SelectableButtonViewModel);
                    if (data == null) continue;
                    _dataPerControl.TryAdd(seenControl, data);
                }
                StateMachineUpdateForSeenControl(seenControl, data, window);
            }
        }

        public ConcurrentBag<Window> Windows { get; set; } = new ConcurrentBag<Window>();

        private void StateMachineUpdateForOtherControls(SelectableControl control, SelectableButtonViewModel data,
            Window window)
        {
            switch (data.State)
            {
                case SelectableState.Idle:
                    break;
                case SelectableState.SeenButWaiting:
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                        data.State = SelectableState.Idle;
                    break;
                case SelectableState.AnimationRunning:
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                        window.Dispatcher.Invoke(control.StopAnimation);
                    }
                    else
                    {
                        data.State = SelectableState.AnimationOnHold;
                        window.Dispatcher.Invoke(control.PauseAnimation);
                    }
                    break;
                case SelectableState.AnimationOnHold:
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                        window.Dispatcher.Invoke(control.StopAnimation);
                    }
                    break;
                case SelectableState.RecentlySelected:
                    if (data.CurrentGazeTimeSpan >= data.GazeTimeSpanBeforeCooldown)
                        data.CurrentGazeTimeSpan = TimeSpan.Zero;
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                        data.State = SelectableState.Idle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StateMachineUpdateForSeenControl(SelectableControl seenControl, SelectableButtonViewModel data,
            Window window)
        {
            switch (data.State)
            {
                case SelectableState.Idle:
                    data.State = SelectableState.SeenButWaiting;
                    break;
                case SelectableState.SeenButWaiting:
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                        data.State = SelectableState.Idle;
                    if (data.CurrentGazeTimeSpan >= data.GazeTimeSpanBeforeAnimationBegins)
                    {
                        window.Dispatcher.Invoke(seenControl.PlayAnimation);
                        data.State = SelectableState.AnimationRunning;
                    }
                    break;
                case SelectableState.AnimationRunning:
                    if (data.CurrentGazeTimeSpan >= data.GazeTimeSpanBeforeSelectionOccurs)
                    {
                        window.Dispatcher.Invoke(seenControl.Select);
                        data.State = SelectableState.RecentlySelected;
                    }
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                        window.Dispatcher.Invoke(seenControl.StopAnimation);
                    }
                    break;
                case SelectableState.AnimationOnHold:
                    window.Dispatcher.Invoke(seenControl.ResumeAnimation);
                    data.State = SelectableState.AnimationRunning;
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                        window.Dispatcher.Invoke(seenControl.StopAnimation);
                    }
                    break;
                case SelectableState.RecentlySelected:
                    if (data.CurrentGazeTimeSpan >= data.GazeTimeSpanBeforeCooldown)
                        data.CurrentGazeTimeSpan = TimeSpan.Zero;
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                        data.State = SelectableState.Idle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}