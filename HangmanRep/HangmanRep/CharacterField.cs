using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace HangmanProject
{
    internal class CharacterField : IPoint
    {
        // Property signatures:
        private int _wordLength;
        public int X { get; private set; }
        public int Y { get; private set; }
        public List<TextBlock> TextBlocks { get; private set; }

        // Ctor
        public CharacterField(int wordLength)
        {
            X = 15;
            Y = (int)GameManager.windowRectangle.Height / 2;

            TextBlocks = new List<TextBlock>();
            _wordLength = wordLength;
            CreateWordOnBoard();
        }
        public CharacterField(string word) : this(word.Length)
        { }

        // Methods
        private void CreateWordOnBoard()
        {
            for (int i = 0; i < _wordLength; i++)
                CreateTextBlock();
        }
        private TextBlock CreateTextBlock(int height = 50, int width = 50)
        {
            X += 70;
            TextBlock tb = new TextBlock() { Height = height, Width = width, Text = "_", FontSize = 35 };
            Canvas.SetTop(tb, Y);
            Canvas.SetLeft(tb, X);
            TextBlocks.Add(tb);
            return tb;
        }
    }
}