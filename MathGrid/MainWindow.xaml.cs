using GameLogic.Enums;
using MathGrid.Services.Game;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MathGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameService _gameService;

        private Difficulty difficulty;
        private GameState result;
        private const string successText = "Congratulations, this solution is correct.";
        private const string failText = "Sorry, this is not a correct solution.";

        public MainWindow(
            GameService gameService)
        {
            InitializeComponent();
            _gameService = gameService;
        }

        private void SetDifficulty()
        {
            if (VeryEasyRadioButton.IsChecked == true) difficulty = Difficulty.VeryEasy;
            else if (EasyRadioButton.IsChecked == true) difficulty = Difficulty.Easy;
            else if (MediumRadioButton.IsChecked == true) difficulty = Difficulty.Medium;
            else if (HardRadioButton.IsChecked == true) difficulty = Difficulty.Hard;
            else if (ExtremeRadioButton.IsChecked == true) difficulty = Difficulty.Extreme;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void NewGame()
        {
            if (!ResetButton.IsEnabled)
            {
                ResetButton.IsEnabled = true;
                Enter1Button.IsEnabled = true;
                Enter2Button.IsEnabled = true;
                Enter3Button.IsEnabled = true;
                Enter4Button.IsEnabled = true;
                Enter5Button.IsEnabled = true;
                Enter6Button.IsEnabled = true;
                Enter7Button.IsEnabled = true;
                Enter8Button.IsEnabled = true;
                Enter9Button.IsEnabled = true;
            }
            SetDifficulty();
            _gameService.NewGame(GameCanvas, difficulty, RatioSlider.Value);
            result = GameState.Unfinished;
            ShowResult();
        }

        private void Reset()
        {
            _gameService.Reset(GameCanvas);
            ShowResult();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 1);
            ShowResult();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 2);
            ShowResult();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 3);
            ShowResult();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 4);
            ShowResult();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 5);
            ShowResult();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 6);
            ShowResult();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 7);
            ShowResult();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 8);
            ShowResult();
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            result = _gameService.EnterNumber(GameCanvas, difficulty, 9);
            ShowResult();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 1);
                    ShowResult();
                    break;
                case Key.NumPad2:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 2);
                    ShowResult();
                    break;
                case Key.NumPad3:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 3);
                    ShowResult();
                    break;
                case Key.NumPad4:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 4);
                    ShowResult();
                    break;
                case Key.NumPad5:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 5);
                    ShowResult();
                    break;
                case Key.NumPad6:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 6);
                    ShowResult();
                    break;
                case Key.NumPad7:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 7);
                    ShowResult();
                    break;
                case Key.NumPad8:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 8);
                    ShowResult();
                    break;
                case Key.NumPad9:
                    result = _gameService.EnterNumber(GameCanvas, difficulty, 9);
                    ShowResult();
                    break;
                case Key.Up:
                    _gameService.ChangeBoxWithArrows(GameCanvas, difficulty, Direction.Up);
                    break;
                case Key.Right:
                    _gameService.ChangeBoxWithArrows(GameCanvas, difficulty, Direction.Right);
                    break;
                case Key.Down:
                    _gameService.ChangeBoxWithArrows(GameCanvas, difficulty, Direction.Down);
                    break;
                case Key.Left:
                    _gameService.ChangeBoxWithArrows(GameCanvas, difficulty, Direction.Left);
                    break;
                case Key.N:
                    NewGame();
                    break;
                case Key.R:
                    Reset();
                    break;
                case Key.F:
                    _gameService.FixateNumber(GameCanvas, difficulty);
                    break;
            }
        }

        private void ShowResult()
        {
            switch (result)
            {
                case GameState.Success:
                    ResultTextBlock.Foreground = Brushes.Green;
                    ResultTextBlock.Text = successText;
                    break;
                case GameState.Fail:
                    ResultTextBlock.Foreground = Brushes.Red;
                    ResultTextBlock.Text = failText;
                    break;
                case GameState.Unfinished:
                    ResultTextBlock.Text = string.Empty;
                    break;
            }
        }
    }
}