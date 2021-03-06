﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Modules.ImageBoard.Model;

namespace TalkingKeyboard.Modules.ImageBoard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using TalkingKeyboard.Infrastructure.Models;
    public class ImageBoardViewModel : BindableBase, IImageBoardViewModel
    {
        private static readonly List<string> ImagesFolderAlternativeList = new List<string>
        {
            "images",
            "Images",
            "IMAGES",
            "imágenes",
            "Imágenes",
            "IMÁGENES",
            "imagenes",
            "Imagenes",
            "IMAGENES"
        };
        private readonly string limitPath;
        private string currentPath;

        public ImageBoardViewModel()
            : base()
        {
            this.ButtonProperties = new ObservableCollection<ImageButtonProperties>();
            limitPath = findImagesPath();
            if (limitPath == null)
            {
                throw new Exception("Images folder must exist.");
            }
            currentPath = limitPath;
            LoadFolder(currentPath);
            RegisterCommands();

        }

        private string findImagesPath()
        {
            return ImagesFolderAlternativeList.Select(Path.GetFullPath).FirstOrDefault(Directory.Exists);
        }

        private ImageButtonProperties LoadImageButton(string path)
        {
            var fullPath = Path.GetFullPath(path);
            var name = Path.GetFileNameWithoutExtension(fullPath).Remove(0,1) + " ";
            return new ImageButtonProperties(false, name, fullPath, path);
        }

        private ImageButtonProperties LoadFolderButton(string path)
        {
            var fileList = Directory.GetFiles(path).ToList();

            foreach (var e in fileList)
            {
                if (e.Contains("portada"))
                {
                    var imagePath = Path.GetFullPath(e);
                    return new ImageButtonProperties(true, "", imagePath, path);
                }
            }
            return null;
        }

        private void LoadFolder(string path)
        {
            this.ButtonProperties.Clear();
            currentPath = Path.GetFullPath(path);
            var fileList = Directory.GetFiles(path).ToList();
            var folderList = Directory.GetDirectories(path).ToList();
            var list = folderList.Union(fileList).OrderBy(i => i);
            var buttonCount = 0;
            foreach (var e in list)
            {
                var isDir = File.GetAttributes(e).HasFlag(FileAttributes.Directory);
                if (isDir)
                {
                    this.ButtonProperties.Add(LoadFolderButton(e));
                    buttonCount++;
                }
                else
                {
                    if (e.Contains("portada")) continue;
                    this.ButtonProperties.Add(LoadImageButton(e));
                    buttonCount++;
                }
            }
            //foreach (var e in fileList)
            //{
            //    if (e.Contains("portada")) continue;
            //    this.ButtonProperties.Add(LoadImageButton(e));
            //    buttonCount++;
            //}
            //foreach (var e in folderList)
            //{
            //    this.ButtonProperties.Add(LoadFolderButton(e));
            //    buttonCount++;
            //}
            for (var i = buttonCount; i < 5; i++)
            {
                this.ButtonProperties.Add(null);
            }
        }

        public ObservableCollection<ImageButtonProperties> ButtonProperties { get; set; }

        public ObservableCollection<string> ImageNames { get; set; }

        public ICommand AddImageTextCommand { get; set; }

        public ICommand AppendTextOrNavigateImagesCommand { get; set; }

        public ICommand ReturnImageMenuCommand { get; set; }

        private void RegisterCommands()
        {
            this.AppendTextOrNavigateImagesCommand = new DelegateCommand<ImageButtonProperties>(this.AppendTextOrNavigateImages);
            Commands.AppendTextOrNavigateImagesCommand.RegisterCommand(this.AppendTextOrNavigateImagesCommand);

            this.ReturnImageMenuCommand = new DelegateCommand(this.ReturnImageMenu);
            Commands.ReturnImageMenuCommand.RegisterCommand(this.ReturnImageMenuCommand);
        }

        private void AppendTextOrNavigateImages(ImageButtonProperties button)
        {
            if (button == null) return;
            if (button.IsFolder)
            {
                LoadFolder(button.Path);
            }
            else
            {
                Commands.AppendTextCommand.Execute(button.Text);
            }
        }

        private void ReturnImageMenu()
        {
            string path = Path.GetFullPath(currentPath)
                .ToLowerInvariant()
                .Equals(Path.GetFullPath(limitPath)
                    .ToLowerInvariant()) ? Path.GetFullPath(limitPath) : Path.GetFullPath(Directory.GetParent(currentPath).FullName);
            LoadFolder(path);
        }
    }
}
