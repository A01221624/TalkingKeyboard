using System;

namespace TalkingKeyboard.Modules.ImageBoard.Model
{
    public class ImageButtonProperties
    {
        public ImageButtonProperties(bool isFolder, string text, string imagePath, string path)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (imagePath == null)
            {
                throw new ArgumentNullException(nameof(imagePath));
            }
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            IsFolder = isFolder;
            Text = text;
            ImagePath = imagePath;
            Path = path;
        }

        public bool IsFolder { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public string Path { get; set; }
    }
}