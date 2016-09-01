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
        private readonly IUnityContainer _container;
        private readonly AveragingFilter _averagingFilter;
        private readonly TimedPoints _timedPoints;

        public GazeSelectionService(IEventAggregator eventAggregator, IUnityContainer container)
        {
            _container = container;
            _timedPoints = new TimedPoints(Configuration.PointKeepAliveTimeSpan);
            _averagingFilter = new AveragingLastNpointsWithinTimeSpanFilter(3, TimeSpan.FromMilliseconds(75));
            Windows.Add(Application.Current.MainWindow);

            eventAggregator.GetEvent<NewCoordinateEvent>().Subscribe(ProcessPoint);
            Application.Current.MainWindow.Closing += (sender, args) => eventAggregator.GetEvent<NewCoordinateEvent>().Unsubscribe(ProcessPoint);
        }

        private ConcurrentBag<SelectableControl> KnownControls { get; } = new ConcurrentBag<SelectableControl>();

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
                if (seenControl == null) continue;
                SelectableButtonViewModel data = null;
                window.Dispatcher.Invoke(() => data = seenControl.DataContext as SelectableButtonViewModel);
                if (data == null) continue;
                KnownControls.Add(seenControl);
                var oldestAcceptable = DateTime.Now - data.GazeKeepAliveTimeSpan;
                data.CurrentGazeTimeSpan = oldestAcceptable > data.LastSeenTime
                    ? TimeSpan.Zero
                    : data.CurrentGazeTimeSpan + (DateTime.Now - data.LastSeenTime);
                data.LastSeenTime = DateTime.Now;

                foreach (var control in KnownControls)
                {
                    SelectableButtonViewModel controlData = null;
                    window.Dispatcher.Invoke(() => controlData = control.DataContext as SelectableButtonViewModel);
                    if (controlData == null) continue;
                    if (control.Equals(seenControl)) StateMachineUpdateForSeenControl(control, data, window);
                    else StateMachineUpdateForOtherControls(control, data, window);
                }
            }
        }

        private void StateMachineUpdateForOtherControls(SelectableControl control, SelectableButtonViewModel data, Window window)
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

        private void StateMachineUpdateForSeenControl(SelectableControl seenControl, SelectableButtonViewModel data, Window window)
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

        public ConcurrentBag<Window> Windows { get; set; } = new ConcurrentBag<Window>();
    }
}