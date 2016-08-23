using System;
using System.Windows;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using TalkingKeyboard.Modules.CoordinateProvider.Services;

namespace TalkingKeyboard.Modules.CoordinateProvider
{
    public class CoordinateProviderModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;

        public CoordinateProviderModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            EyeXGazeCoordinateService eyeXGazeCoordinateService = new EyeXGazeCoordinateService(_eventAggregator);

            /* TODO: Do this on module unload instead of Application exit */
            Application.Current.Exit += (sender, args) =>
            {
                eyeXGazeCoordinateService.Dispose();
            };
        }
    }
}