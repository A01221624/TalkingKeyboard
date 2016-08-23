using System;
using System.Diagnostics;
using System.Windows;
using EyeXFramework;
using Microsoft.Practices.Unity.Utility;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;

namespace TalkingKeyboard.Modules.CoordinateProvider.Services
{
    public class EyeXGazeCoordinateService : IDisposable
    {
        public EyeXHost EyeXHost { get; set; }
        public GazePointDataStream GazePointDataStream { get; set; }

        public EyeXGazeCoordinateService(IEventAggregator eventAggregator)
        {
            EyeXHost = new EyeXHost();
            EyeXHost.Start();

            GazePointDataStream = EyeXHost.CreateGazePointDataStream(GazePointDataMode.Unfiltered);
            GazePointDataStream.Next += (sender, args) =>
            {
                eventAggregator.GetEvent<NewCoordinateEvent>().Publish(new Point(args.X, args.Y));
            };
            
        }

        public void Dispose()
        {
            GazePointDataStream.Dispose();
            EyeXHost.Dispose();
        }
    }
}