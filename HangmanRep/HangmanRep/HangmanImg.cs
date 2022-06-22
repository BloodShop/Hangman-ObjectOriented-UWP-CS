using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace HangmanProject
{
    internal class HangmanImg : IPoint
    {
        // Property signatures:
        private List<Rectangle> _images;
        public Rectangle TheImage { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        // Ctor
        public HangmanImg()
        {
            X = (int)GameManager.windowRectangle.Width - 400;
            Y = (int)GameManager.windowRectangle.Height - 375;
            GetImageSource();
            CreateTheImage();
        }

        // Methods
        private void CreateTheImage()
        {
            TheImage = _images[0];
            Canvas.SetLeft(TheImage, X);
            Canvas.SetTop(TheImage, Y);
        }
        private void GetImageSource()
        {
            _images = new List<Rectangle>();
            for (int i = 0; i < 12; i++)
                _images.Add(new Rectangle() { Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri($"ms-appx:///Images/img{i}.png")) }, Width = 300, Height = 350 });
        }
    }
}
