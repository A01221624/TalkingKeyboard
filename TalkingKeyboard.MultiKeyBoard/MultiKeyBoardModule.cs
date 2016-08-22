using System;
using Prism.Modularity;
using Prism.Regions;

namespace TalkingKeyboard.Modules.MultiKeyBoard
{
    public class MultiKeyBoardModule : IModule
    {
        IRegionManager _regionManager;

        public MultiKeyBoardModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}