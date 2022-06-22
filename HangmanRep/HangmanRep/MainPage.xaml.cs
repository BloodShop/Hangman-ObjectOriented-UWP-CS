using Windows.UI.Xaml.Controls;

namespace HangmanProject
{
    public enum Rank { Begginer = 1, Proffesional }
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            GameManager gm = new GameManager(LayoutRoot);
        }
    }
}