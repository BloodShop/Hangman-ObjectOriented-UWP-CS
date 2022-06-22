using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HangmanProject
{
    class GameManager
    {
        // Property signatures:
        StackPanel _settings;
        Media _media;
        Word _word;
        CharacterField _characterField;
        HangmanImg _hangmanImg;
        Canvas _canvas;
        List<Button> _keyBoard;
        public static Rect windowRectangle { get; private set; }
        public static int CountMiss { get; private set; }

        // Ctor
        public GameManager(Canvas LayoutRoot, Rank rank = Rank.Begginer)
        {
            windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;
            _canvas = LayoutRoot;
            CountMiss = 0;
            _media = new Media();
            CreateNewObj(rank);
        }

        // Methods
        void CreateNewObj(Rank rank)
        {
            _word = new Word(rank);
            _characterField = new CharacterField(_word.ToString());
            foreach (TextBlock tb in _characterField.TextBlocks)
                _canvas.Children.Add(tb);
            CreateKeyBoardOnBoard(); // Create keyboard
            _hangmanImg = new HangmanImg();
            _canvas.Children.Add(_hangmanImg.TheImage);
            CreateSettings();
        } // Only used once when gameManager is created
        private Button begginer, proffesional, startGametb;
        void CreateSettings()
        {
            SolidColorBrush inBouns = new SolidColorBrush(Colors.Aqua), outBouns = new SolidColorBrush(Colors.Chocolate);
            _settings = new StackPanel() { Margin = new Thickness(5), Orientation = Orientation.Vertical, VerticalAlignment = VerticalAlignment.Top, BorderThickness = new Thickness(0) };

            startGametb = new Button() { Content = "New Game", Width = 100, BorderBrush = inBouns, Background = outBouns, Visibility = Visibility.Visible };
            begginer = new Button() { Content = "Begginer", Width = 100, BorderBrush = inBouns, Background = outBouns, Visibility = Visibility.Collapsed };
            proffesional = new Button() { Content = "Proffesional", Width = 100, BorderBrush = inBouns, Background = outBouns, Visibility = Visibility.Collapsed };
            startGametb.Click += Rank_Click;
            begginer.Click += Rank_Click;
            proffesional.Click += Rank_Click;

            _settings.Children.Add(startGametb);
            _settings.Children.Add(begginer);
            _settings.Children.Add(proffesional);

            _canvas.Children.Add(_settings);
        } // Create stackPanel settings
        void Rank_Click(object sender, RoutedEventArgs e)
        {
            Button tempBtn = sender as Button;
            if (tempBtn.Content == begginer.Content)
                RestartGame(Rank.Begginer);
            else if(tempBtn.Content == proffesional.Content)
                RestartGame(Rank.Proffesional);
            else if ((string)startGametb.Content == "New Game") // not pressed
            {
                startGametb.Content = "Select Rank";
                begginer.Visibility = Visibility.Visible;
                proffesional.Visibility = Visibility.Visible;
            }
            else if ((string)startGametb.Content == "Select Rank") // pressed
            {
                startGametb.Content = "New Game";
                begginer.Visibility = Visibility.Collapsed;
                proffesional.Visibility = Visibility.Collapsed;
            }
        } // Any click on the stackPanel setting buttons
        void RestartGame(Rank rank = Rank.Begginer)
        {
            CountMiss = 0;
            _canvas.Children.Remove(_hangmanImg.TheImage);
            _hangmanImg = new HangmanImg();
            _canvas.Children.Add(_hangmanImg.TheImage);

            foreach (TextBlock tb in _characterField.TextBlocks)
                _canvas.Children.Remove(tb);
            _word = new Word(rank);
            _characterField = new CharacterField(_word.ToString());
            foreach (TextBlock tb in _characterField.TextBlocks)
                _canvas.Children.Add(tb);

            foreach (Button btn in _keyBoard)
                btn.IsEnabled = true;
        } // when-ever the user clicks the rank - the game restarts
        void CreateKeyBoardOnBoard()
        {
            _keyBoard = new List<Button>();
            int xPos = 100, yPos = 50;
            // Another way - Loop: i <- 0 to abc.Length
            for (int i = 65; i < 91; i++) // ASCII [A-Z]
            { // in accordance to the windowSize
                xPos += 75;
                if (xPos >= windowRectangle.Width - 175)
                {
                    xPos = 50;
                    yPos += 100;
                }
                Button tempBtn = CreateBtn((char)i, xPos, yPos);
                _keyBoard.Add(tempBtn);
                _canvas.Children.Add(tempBtn);
            }
        } // Create keyBoard of buttons on canvas
        Button CreateBtn(char content, double xPos, double yPos, int width = 50, int height = 50)
        {
            Button btn = new Button() { Height = height, Width = width, Content = $"{content}", Name = $"{content}" };
            btn.Click += Btn_Click;
            Canvas.SetLeft(btn, xPos);
            Canvas.SetTop(btn, yPos);
            return btn;
        }
        void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button keyPressed = sender as Button;
            keyPressed.IsEnabled = false;
            CheckIfMember(char.Parse(keyPressed.Content.ToString()));
        } // When-ever there is a click on any button sends the letter to the 'CheckIfMember'
        void RevealCharacter(int index, char letter) => _characterField.TextBlocks[index].Text = letter.ToString(); // When the word does contains the letter, it reveals itself
        void RevealHangman() => _hangmanImg.TheImage.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri($"ms-appx:///Images/img{++CountMiss}.png")) }; // When the word does contains the letter, it reveals part of the hangman
        void CheckIfMember(char c)
        {
            int count = 0;
            for (int i = 0; i < _word.Length; i++)
                if (_word[i].Equals(c)) // equallity between the user's char and each char in word 
                {
                    count++;
                    RevealCharacter(i, c);
                }
            if(count == 0)
                RevealHangman();

            CheckGameState();
        } // Checker if the word contains the letter the user pressed
        void CheckGameState()
        {
            if (Win()) MessageToUser("You Won");
            else if (Lose()) MessageToUser($"You Lost\nThe word was {_word}");
        } // Check whether the user have won / lost
        bool Win()
        {
            for (int i = 0; i < _word.Length; i++)
                if(_characterField.TextBlocks[i].Text == "_") 
                    return false;
            _media.WinCheers.Play();
            return true;
        }
        bool Lose()
        {
            if(CountMiss != 11) // amount of images / tries
                return false;
            _media.LoseSound.Play();
            return true;
        }
        async void MessageToUser(string msg) // message which user get when the game is finish whether he won / lost with different content
        {
            await new MessageDialog($"{msg}! You have Missed {CountMiss} times.\nPress Close to Play again").ShowAsync();
            RestartGame();
        }
    }
}