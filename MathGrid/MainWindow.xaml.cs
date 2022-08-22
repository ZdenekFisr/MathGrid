using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MathGrid
{
    public partial class MainWindow : Window
    {
        private readonly GameModel game = new GameModel();
        private Difficulty difficulty;
        private Result result;
        private const string success = "Congratulations, this solution is correct.";
        private const string fail = "Sorry, this is not a correct solution.";

        private void SetDifficulty()
        {
            if (VeryEasyRadio.IsChecked.Value) difficulty = Difficulty.VeryEasy;
            else if (EasyRadio.IsChecked.Value) difficulty = Difficulty.Easy;
            else if (MediumRadio.IsChecked.Value) difficulty = Difficulty.Medium;
            else if (HardRadio.IsChecked.Value) difficulty = Difficulty.Hard;
            else if (ExtremeRadio.IsChecked.Value) difficulty = Difficulty.Extreme;
            else difficulty = 0;
        }

        public MainWindow()
        {
            InitializeComponent();
            VeryEasyRadio.IsChecked = true;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 1);
            GetResult();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 2);
            GetResult();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 3);
            GetResult();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 4);
            GetResult();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 5);
            GetResult();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 6);
            GetResult();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 7);
            GetResult();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 8);
            GetResult();
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            result = game.EnterNumber(gameCanvas, difficulty, 9);
            GetResult();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                    result = game.EnterNumber(gameCanvas, difficulty, 1);
                    GetResult();
                    break;
                case Key.NumPad2:
                    result = game.EnterNumber(gameCanvas, difficulty, 2);
                    GetResult();
                    break;
                case Key.NumPad3:
                    result = game.EnterNumber(gameCanvas, difficulty, 3);
                    GetResult();
                    break;
                case Key.NumPad4:
                    result = game.EnterNumber(gameCanvas, difficulty, 4);
                    GetResult();
                    break;
                case Key.NumPad5:
                    result = game.EnterNumber(gameCanvas, difficulty, 5);
                    GetResult();
                    break;
                case Key.NumPad6:
                    result = game.EnterNumber(gameCanvas, difficulty, 6);
                    GetResult();
                    break;
                case Key.NumPad7:
                    result = game.EnterNumber(gameCanvas, difficulty, 7);
                    GetResult();
                    break;
                case Key.NumPad8:
                    result = game.EnterNumber(gameCanvas, difficulty, 8);
                    GetResult();
                    break;
                case Key.NumPad9:
                    result = game.EnterNumber(gameCanvas, difficulty, 9);
                    GetResult();
                    break;
                case Key.Up:
                    game.ChangeBoxWithArrows(gameCanvas, difficulty, Direction.Up);
                    break;
                case Key.Right:
                    game.ChangeBoxWithArrows(gameCanvas, difficulty, Direction.Right);
                    break;
                case Key.Down:
                    game.ChangeBoxWithArrows(gameCanvas, difficulty, Direction.Down);
                    break;
                case Key.Left:
                    game.ChangeBoxWithArrows(gameCanvas, difficulty, Direction.Left);
                    break;
                case Key.N:
                    NewGame();
                    break;
                case Key.R:
                    Reset();
                    break;
            }
        }

        private void NewGame()
        {
            SetDifficulty();
            game.NewGame(gameCanvas, difficulty, ratioSlider.Value);
            resultTextBlock.Text = string.Empty;
        }

        private void Reset()
        {
            game.Reset(gameCanvas);
            GetResult();
        }

        private void GetResult()
        {
            switch (result)
            {
                case Result.Success:
                    resultTextBlock.Foreground = Brushes.Green;
                    resultTextBlock.Text = success;
                    break;
                case Result.Fail:
                    resultTextBlock.Foreground = Brushes.Red;
                    resultTextBlock.Text = fail;
                    break;
                case Result.Unfinished:
                    resultTextBlock.Text = string.Empty;
                    break;
            }
        }
    }
}
