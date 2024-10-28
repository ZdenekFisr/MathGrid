using GameLogic.Enums;
using MathGrid.Enums;
using MathGrid.Services.Game;
using System.Windows;
using System.Windows.Controls;
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

        private GameState result;
        private const string successText = "Congratulations, this solution is correct.";
        private const string failText = "Sorry, this is not a correct solution.";

        public Difficulty SelectedDifficulty { get; set; } = Difficulty.Easy;

        public MainWindow(
            GameService gameService)
        {
            InitializeComponent();
            DataContext = this;
            _gameService = gameService;
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
            _gameService.NewGame(GameCanvas, SelectedDifficulty, RatioSlider.Value);
            result = GameState.Unfinished;
            ShowResult();
        }

        private void Reset()
        {
            _gameService.Reset(GameCanvas);
            ShowResult();
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "1");
                    ShowResult();
                    break;
                case Key.NumPad2:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "2");
                    ShowResult();
                    break;
                case Key.NumPad3:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "3");
                    ShowResult();
                    break;
                case Key.NumPad4:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "4");
                    ShowResult();
                    break;
                case Key.NumPad5:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "5");
                    ShowResult();
                    break;
                case Key.NumPad6:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "6");
                    ShowResult();
                    break;
                case Key.NumPad7:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "7");
                    ShowResult();
                    break;
                case Key.NumPad8:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "8");
                    ShowResult();
                    break;
                case Key.NumPad9:
                    result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, "9");
                    ShowResult();
                    break;

                case Key.Up:
                    _gameService.ChangeBoxWithArrows(GameCanvas, SelectedDifficulty, Direction.Up);
                    break;
                case Key.Right:
                    _gameService.ChangeBoxWithArrows(GameCanvas, SelectedDifficulty, Direction.Right);
                    break;
                case Key.Down:
                    _gameService.ChangeBoxWithArrows(GameCanvas, SelectedDifficulty, Direction.Down);
                    break;
                case Key.Left:
                    _gameService.ChangeBoxWithArrows(GameCanvas, SelectedDifficulty, Direction.Left);
                    break;

                case Key.N:
                    NewGame();
                    break;
                case Key.R:
                    Reset();
                    break;
                case Key.F:
                    _gameService.FixateNumber(GameCanvas, SelectedDifficulty);
                    break;

                case Key.A:
                    RatioSlider.Value -= RatioSlider.TickFrequency;
                    break;
                case Key.D:
                    RatioSlider.Value += RatioSlider.TickFrequency;
                    break;
                case Key.S:
                    ChangeDifficulty(1);
                    break;
                case Key.W:
                    ChangeDifficulty(-1);
                    break;
            }
        }

        private void ChangeDifficulty(sbyte direction)
        {
            var values = (Difficulty[])Enum.GetValues(typeof(Difficulty));
            sbyte currentIndex = (sbyte)Array.IndexOf(values, SelectedDifficulty);
            sbyte newIndex = (sbyte)((currentIndex + direction + values.Length) % values.Length);
            SelectedDifficulty = values[newIndex];

            switch (SelectedDifficulty)
            {
                case Difficulty.VeryEasy:
                    VeryEasyRadioButton.IsChecked = true;
                    break;
                case Difficulty.Easy:
                    EasyRadioButton.IsChecked = true;
                    break;
                case Difficulty.Medium:
                    MediumRadioButton.IsChecked = true;
                    break;
                case Difficulty.Hard:
                    HardRadioButton.IsChecked = true;
                    break;
                case Difficulty.Extreme:
                    ExtremeRadioButton.IsChecked = true;
                    break;
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button enterButton)
                return;

            string? buttonContent = enterButton.Content.ToString();
            if (buttonContent is null)
                return;

            result = _gameService.EnterNumber(GameCanvas, SelectedDifficulty, buttonContent);
            ShowResult();
        }
    }
}