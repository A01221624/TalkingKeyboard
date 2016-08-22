using System;
using Prism.Modularity;
using Prism.Regions;

namespace TalkingKeyboard.Modules.CommandBoard
{
    public class CommandBoardModule : IModule
    {
        IRegionManager _regionManager;

        public CommandBoardModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}