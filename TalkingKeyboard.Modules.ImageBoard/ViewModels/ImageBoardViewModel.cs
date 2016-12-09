using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TalkingKeyboard.Modules.ImageBoard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using TalkingKeyboard.Infrastructure.Models;
    public class ImageBoardViewModel : BindableBase, IImageBoardViewModel
    {
        public ImageBoardViewModel()
            : base()
        {
            var imagePathList = Directory.GetFiles("images").Select(
                Path.GetFullPath).ToList();
            this.ImagePaths = new ObservableCollection<string>(imagePathList);

            var imageNameList = imagePathList.Select(
                s => Path.GetFileNameWithoutExtension(s) + " ").ToList();
            this.ImageNames = new ObservableCollection<string>(imageNameList);
        }

        public ObservableCollection<string> ImagePaths { get; set; }

        public ObservableCollection<string> ImageNames { get; set; }

        public ICommand AddImageTextCommand { get; set; }
    }
}
