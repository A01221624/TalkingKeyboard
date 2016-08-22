using System;
using Prism.Modularity;
using Prism.Regions;

namespace TalkingKeyboard.Modules.CoordinateProvider
{
    public class CoordinateProviderModule : IModule
    {
        IRegionManager _regionManager;

        public CoordinateProviderModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}