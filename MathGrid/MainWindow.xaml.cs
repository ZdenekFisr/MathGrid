using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MathGrid
{
	public partial class MainWindow : Window
	{
		private readonly Game game = new Game();
		private Difficulty difficulty;
		private Result result;
		private const string successText = "Congratulations, this solution is correct.";
		private const string failText = "Sorry, this is not a correct solution.";

		private void SetDifficulty()
		{
			if (VeryEasyRadio.IsChecked.Value) difficulty = Difficulty.VeryEasy;
			else if (EasyRadio.IsChecked.Value) difficulty = Difficulty.Easy;
			else if (MediumRadio.IsChecked.Value) difficulty = Difficulty.Medium;
			else if (HardRadio.IsChecked.Value) difficulty = Difficulty.Hard;
			else if (ExtremeRadio.IsChecked.Value) difficulty = Difficulty.Extreme;
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
			ShowResult();
		}

		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 2);
			ShowResult();
		}

		private void Button3_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 3);
			ShowResult();
		}

		private void Button4_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 4);
			ShowResult();
		}

		private void Button5_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 5);
			ShowResult();
		}

		private void Button6_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 6);
			ShowResult();
		}

		private void Button7_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 7);
			ShowResult();
		}

		private void Button8_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 8);
			ShowResult();
		}

		private void Button9_Click(object sender, RoutedEventArgs e)
		{
			result = game.EnterNumber(gameCanvas, difficulty, 9);
			ShowResult();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.NumPad1:
					result = game.EnterNumber(gameCanvas, difficulty, 1);
					ShowResult();
					break;
				case Key.NumPad2:
					result = game.EnterNumber(gameCanvas, difficulty, 2);
					ShowResult();
					break;
				case Key.NumPad3:
					result = game.EnterNumber(gameCanvas, difficulty, 3);
					ShowResult();
					break;
				case Key.NumPad4:
					result = game.EnterNumber(gameCanvas, difficulty, 4);
					ShowResult();
					break;
				case Key.NumPad5:
					result = game.EnterNumber(gameCanvas, difficulty, 5);
					ShowResult();
					break;
				case Key.NumPad6:
					result = game.EnterNumber(gameCanvas, difficulty, 6);
					ShowResult();
					break;
				case Key.NumPad7:
					result = game.EnterNumber(gameCanvas, difficulty, 7);
					ShowResult();
					break;
				case Key.NumPad8:
					result = game.EnterNumber(gameCanvas, difficulty, 8);
					ShowResult();
					break;
				case Key.NumPad9:
					result = game.EnterNumber(gameCanvas, difficulty, 9);
					ShowResult();
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
				case Key.F:
					game.FixNumber(gameCanvas, difficulty);
					break;
			}
		}

		private void NewGame()
		{
			if (!resetButton.IsEnabled)
			{
				resetButton.IsEnabled = true;
				button1.IsEnabled = true;
				button2.IsEnabled = true;
				button3.IsEnabled = true;
				button4.IsEnabled = true;
				button5.IsEnabled = true;
				button6.IsEnabled = true;
				button7.IsEnabled = true;
				button8.IsEnabled = true;
				button9.IsEnabled = true;
			}
			SetDifficulty();
			game.NewGame(gameCanvas, difficulty, ratioSlider.Value);
			result = Result.Unfinished;
			ShowResult();
		}

		private void Reset()
		{
			game.Reset(gameCanvas);
			ShowResult();
		}

		private void ShowResult()
		{
			switch (result)
			{
				case Result.Success:
					resultTextBlock.Foreground = Brushes.Green;
					resultTextBlock.Text = successText;
					break;
				case Result.Fail:
					resultTextBlock.Foreground = Brushes.Red;
					resultTextBlock.Text = failText;
					break;
				case Result.Unfinished:
					resultTextBlock.Text = string.Empty;
					break;
			}
		}
	}
}
