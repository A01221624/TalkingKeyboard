// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EyeXGazeCoordinateService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the EyeXGazeCoordinateService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.CoordinateProvider.Services
{
    using System;
    using System.Windows;

    using EyeXFramework;

    using Prism.Events;

    using TalkingKeyboard.Infrastructure;

    using Tobii.EyeX.Framework;

    public class EyeXGazeCoordinateService : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EyeXGazeCoordinateService" /> class.
        /// </summary>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        public EyeXGazeCoordinateService(IEventAggregator eventAggregator)
        {
            this.EyeXHost = new EyeXHost();
            this.EyeXHost.Start();

            this.GazePointDataStream = this.EyeXHost.CreateGazePointDataStream(GazePointDataMode.Unfiltered);
            this.GazePointDataStream.Next +=
                (sender, args) =>
                    {
                        eventAggregator.GetEvent<Events.NewCoordinateEvent>().Publish(new Point(args.X, args.Y));
                    };
        }

        /// <summary>
        ///     Gets or sets the EyeX host.
        /// </summary>
        /// <value>
        ///     The EyeX host.
        /// </value>
        public EyeXHost EyeXHost { get; set; }

        /// <summary>
        ///     Gets or sets the gaze point data stream.
        /// </summary>
        /// <value>
        ///     The gaze point data stream.
        /// </value>
        public GazePointDataStream GazePointDataStream { get; set; }

        /// <summary>
        ///     Disposes of the point data stream and the EyeXHost.
        /// </summary>
        public void Dispose()
        {
            this.GazePointDataStream.Dispose();
            this.EyeXHost.Dispose();
        }
    }
}