// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GazeSelectionService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the GazeSelectionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.Windows;

    using Prism.Events;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.DataContainers;
    using TalkingKeyboard.Infrastructure.Enums;
    using TalkingKeyboard.Infrastructure.Helpers;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;
    using TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters;

    /// <summary>
    ///     This class implements the service for providing gaze-based activation of graphical interface elements (e.g.
    ///     buttons). It contains the logic for performing the selection based on the state machine of each gaze-selectable
    ///     element. Future work could extract the logic into classes which implement different algorithms for selection.
    /// </summary>
    /// <seealso cref="TalkingKeyboard.Infrastructure.ServiceInterfaces.IControlActivationService" />
    public class GazeSelectionService : IControlActivationService
    {
        private readonly AveragingFilter averagingFilter;
        private readonly ConcurrentDictionary<SelectableControl, SelectableButtonViewModel> dataPerControl;
        private readonly TimedPoints timedPoints;



        /// <summary>
        ///     Initializes a new instance of the <see cref="GazeSelectionService" /> class.
        /// </summary>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        public GazeSelectionService(IEventAggregator eventAggregator)
        {
            this.timedPoints = new TimedPoints(Configuration.PointKeepAliveTimeSpan);
            this.averagingFilter = new AveragingLastNpointsWithinTimeSpanFilter(3, TimeSpan.FromMilliseconds(75));
            this.dataPerControl = new ConcurrentDictionary<SelectableControl, SelectableButtonViewModel>();
            this.KnownWindows.Add(Application.Current.MainWindow);

            eventAggregator.GetEvent<Events.NewCoordinateEvent>().Subscribe(this.ProcessPoint);
            Application.Current.MainWindow.Closing +=
                (sender, args) => eventAggregator.GetEvent<Events.NewCoordinateEvent>().Unsubscribe(this.ProcessPoint);
        }

        /// <summary>
        ///     Gets or sets the known windows.
        /// </summary>
        /// <value>
        ///     The known windows.
        /// </value>
        public ConcurrentBag<Window> KnownWindows { get; set; } = new ConcurrentBag<Window>();

        /// <summary>
        ///     Checks whether the point falls on a control and updates any relevant information.
        /// </summary>
        /// <remarks>
        ///     The duration a control has been gazed at depends on the last time it was seen and the time established as a maximum
        ///     to keep alive an inactive gaze.
        /// </remarks>
        /// <param name="p">The point to be processed.</param>
        public void ProcessPoint(Point p)
        {
            this.timedPoints.Maintain();
            this.timedPoints.AddPoint(p);
            var nullablePoint = this.averagingFilter.Filter(this.timedPoints);
            if (nullablePoint == null)
            {
                return;
            }

            var point = nullablePoint.Value;
            foreach (var window in this.KnownWindows)
            {
                var seenControl = HitTestHelper.SelectableControlUnderPoint(point, window);

                foreach (var cd in this.dataPerControl)
                {
                    var controlData = cd.Value;
                    var control = cd.Key;

                    var oldestAcceptable = DateTime.Now - controlData.GazeKeepAliveTimeSpan;
                    controlData.CurrentGazeTimeSpan = oldestAcceptable > controlData.LastSeenTime
                                                          ? TimeSpan.Zero
                                                          : controlData.CurrentGazeTimeSpan
                                                            + (DateTime.Now - controlData.LastSeenTime);

                    if (!control.Equals(seenControl))
                    {
                        this.StateMachineUpdateForOtherControls(control, controlData, window);
                    }
                    else
                    {
                        controlData.LastSeenTime = DateTime.Now;
                    }
                }

                if (seenControl == null)
                {
                    continue;
                }

                SelectableButtonViewModel data;
                if (!this.dataPerControl.TryGetValue(seenControl, out data))
                {
                    window.Dispatcher.Invoke(() => data = seenControl.DataContext as SelectableButtonViewModel);
                    if (data == null)
                    {
                        continue;
                    }

                    this.dataPerControl.TryAdd(seenControl, data);
                }

                this.StateMachineUpdateForSeenControl(seenControl, data, window);
            }
        }

        /// <summary>
        ///     Processes the machine update for controls which weren't seen with the current (latest) point.
        /// </summary>
        /// <param name="control">The control (which was not seen).</param>
        /// <param name="data">The view-model for the control.</param>
        /// <param name="window">The window where the control is located.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if the control is in an unexpected state (e.g. infrastructure created a new state and a module considers it,
        ///     but this module has not been updated).
        /// </exception>
        private void StateMachineUpdateForOtherControls(
            SelectableControl control,
            SelectableButtonViewModel data,
            Window window)
        {
            switch (data.State)
            {
                case SelectableState.Idle:
                    break;
                case SelectableState.SeenButWaiting:
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                    }

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
                    {
                        data.CurrentGazeTimeSpan = TimeSpan.Zero;
                    }

                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Processes the machine update for the control which was seen with the current (latest) point.
        /// </summary>
        /// <param name="seenControl">The control which was seen.</param>
        /// <param name="data">The view-model for the control.</param>
        /// <param name="window">The window where the control is located.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if the control is in an unexpected state (e.g. infrastructure created a new state and a module considers it,
        ///     but this module has not been updated).
        /// </exception>
        private void StateMachineUpdateForSeenControl(
            SelectableControl seenControl,
            SelectableButtonViewModel data,
            Window window)
        {
            var buttonText = string.Empty;
            window.Dispatcher.Invoke(() => buttonText = (seenControl as SelectableButton)?.Text);
            if (!data.IsSelectable && buttonText != Infrastructure.Constants.ButtonLabels.RestGaze)
            {
                return;
            }

            switch (data.State)
            {
                case SelectableState.Idle:
                    data.State = SelectableState.SeenButWaiting;
                    break;
                case SelectableState.SeenButWaiting:
                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                    }

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
                    {
                        data.CurrentGazeTimeSpan = TimeSpan.Zero;
                    }

                    if (data.CurrentGazeTimeSpan == TimeSpan.Zero)
                    {
                        data.State = SelectableState.Idle;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}