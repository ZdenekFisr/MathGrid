using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MathGrid
{
	/// <summary>
	/// Contains methods to generate a math grid and play a game using WPF controls.
	/// </summary>
	class Game
	{
		private readonly Random random = new Random();
		private const int smallSpace = 2, largeSpace = 4, // thickness of a space between boxes
				sizeVeryEasy = 98, sizeEasy = 73, sizeMedium = 58, sizeHard = 48, sizeExtreme = 28, // size of a box
				countVeryEasy = 6, countEasy = 8, countMedium = 10, countHard = 12, countExtreme = 20; // number of boxes in a row or a column
		private const string plus = "+", minus = "-", multiply = "×";
		private int chosenBox;
		private int[,] entered; // array of numbers entered by the player
		private bool[,] stateOfBox; // serves to de-color a box when selecting a different one
		private bool[,] editable; // red color of an occupied box

		private void TextBlock_MouseDownExtreme(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchBox(textBlock, sizeExtreme, countExtreme);
		}

		private void TextBlock_MouseDownHard(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchBox(textBlock, sizeHard, countHard);
		}

		private void TextBlock_MouseDownMedium(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchBox(textBlock, sizeMedium, countMedium);
		}

		private void TextBlock_MouseDownEasy(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchBox(textBlock, sizeEasy, countEasy);
		}

		private void TextBlock_MouseDownVeryEasy(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchBox(textBlock, sizeVeryEasy, countVeryEasy);
		}

		private void SwitchBox(TextBlock textBlock, int size, int count) // it happens when clicked on a box and only when its color is white
		{
			if (textBlock.Background == Brushes.White)
			{
				int px, py;
				px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
				py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
				SwitchStateOfBox(px, py);
				if (editable[px, py]) textBlock.Background = Brushes.Red;
				else textBlock.Background = Brushes.Yellow;
				chosenBox = GetChosenBoxNumber(px, py, count);
			}
		}

		private void SwitchFixation(TextBlock textBlock, int size, int count) // changes the fixation of a number - right mouse button click
		{
			if (textBlock.Background == Brushes.White || textBlock.Background == Brushes.Yellow)
			{
				int px, py;
				px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
				py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
				if (!editable[px, py])
				{
					if (textBlock.Foreground == Brushes.Black) textBlock.Foreground = Brushes.Blue;
					else textBlock.Foreground = Brushes.Black;
				}
				chosenBox = GetChosenBoxNumber(px, py, count);
			}
		}

		/// <summary>
		/// Changes whether the number is fixed or not.
		/// </summary>
		internal void FixNumber(Canvas canvas, Difficulty difficulty)
		{
			foreach (TextBlock textBlock in canvas.Children)
			{
				if (textBlock.Background == Brushes.Yellow && textBlock.Text != string.Empty)
				{
					switch (difficulty)
					{
						case Difficulty.VeryEasy:
							SwitchFixation(textBlock, sizeVeryEasy, countVeryEasy);
							break;
						case Difficulty.Easy:
							SwitchFixation(textBlock, sizeEasy, countEasy);
							break;
						case Difficulty.Medium:
							SwitchFixation(textBlock, sizeMedium, countMedium);
							break;
						case Difficulty.Hard:
							SwitchFixation(textBlock, sizeHard, countHard);
							break;
						case Difficulty.Extreme:
							SwitchFixation(textBlock, sizeExtreme, countExtreme);
							break;
					}
					break;
				}
			}
		}

		private int GetChosenBoxNumber(int px, int py, int count)
		{
			return py + px * count;
		}

		private void Canvas_MouseDownExtreme(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = sender as Canvas;
			UnselectBox(canvas);
		}

		private void Canvas_MouseDownHard(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = sender as Canvas;
			UnselectBox(canvas);
		}

		private void Canvas_MouseDownMedium(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = sender as Canvas;
			UnselectBox(canvas);
		}

		private void Canvas_MouseDownEasy(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = sender as Canvas;
			UnselectBox(canvas);
		}

		private void Canvas_MouseDownVeryEasy(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = sender as Canvas;
			UnselectBox(canvas);
		}

		private void UnselectBox(Canvas canvas) // when canvas is clicked on, the yellow or red box turns white
		{
			int i = 0;
			foreach (TextBlock textBlock in canvas.Children)
			{
				if ((textBlock.Background == Brushes.Yellow || textBlock.Background == Brushes.Red) && i != chosenBox)
				{
					textBlock.Background = Brushes.White;
					break;
				}
				i++;
			}
		}

		private void TextBlock_MouseRightButtonDownExtreme(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchFixation(textBlock, sizeExtreme, countExtreme);
		}

		private void TextBlock_MouseRightButtonDownHard(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchFixation(textBlock, sizeHard, countHard);
		}

		private void TextBlock_MouseRightButtonDownMedium(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchFixation(textBlock, sizeMedium, countMedium);
		}

		private void TextBlock_MouseRightButtonDownEasy(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchFixation(textBlock, sizeEasy, countEasy);
		}

		private void TextBlock_MouseRightButtonDownVeryEasy(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			SwitchFixation(textBlock, sizeVeryEasy, countVeryEasy);
		}

		private void SwitchStateOfBox(int x, int y) // sets states of all boxes to false and then sets the demanded one to true
		{
			for (int i = 0; i < stateOfBox.GetLength(0); i++)
			{
				for (int j = 0; j < stateOfBox.GetLength(1); j++)
				{
					stateOfBox[i, j] = false;
				}
			}
			stateOfBox[x, y] = true;
		}

		private int[,] GenerateNumbers(Difficulty difficulty)
		{
			int count = GetCount(difficulty, true);
			int[,] a = new int[count, count];
      for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					if (i % 2 == 0 && j % 2 == 0) a[i, j] = random.Next(1, 10);
					else if (i % 2 != 0 && j % 2 != 0) a[i, j] = 0;
					else
					{
						if (i == 1 && j == 0 || i == 0 && j == 1) a[i, j] = random.Next(10, 13);
						else
						{
							if (i == 0 || i == 1)
							{
								if (a[i, j - 2] == 12) a[i, j] = random.Next(10, 12);
								else a[i, j] = random.Next(10, 13);
							}
							else if (j == 0 || j == 1)
							{
								if (a[i - 2, j] == 12) a[i, j] = random.Next(10, 12);
								else a[i, j] = random.Next(10, 13);
							}
							else if (a[i, j - 2] == 12 || a[i - 2, j] == 12) a[i, j] = random.Next(10, 12);
							else a[i, j] = random.Next(10, 13);
						}
					}
				}
			}
			return a;
		}

		private void SumGame(int[,] gameGrid, out int[] sumRows, out int[] sumColumns) // calculates sums of all rows and columns and returns them
		{
			sumRows = new int[gameGrid.GetLength(1) / 2 + 1];
			sumColumns = new int[gameGrid.GetLength(0) / 2 + 1];
			int index = 0;
			for (int i = 0; i < gameGrid.GetLength(0); i += 2) // rows
			{
				if (gameGrid[i, 1] == 12) sumRows[index] += gameGrid[i, 0] * gameGrid[i, 2];
				else sumRows[index] += gameGrid[i, 0];
				for (int j = 2; j < gameGrid.GetLength(1) - 2; j += 2)
				{
					if (gameGrid[i, j - 1] == 12) continue;
					else if (gameGrid[i, j + 1] == 12)
					{
						if (gameGrid[i, j - 1] == 10) sumRows[index] += gameGrid[i, j] * gameGrid[i, j + 2];
						else if (gameGrid[i, j - 1] == 11) sumRows[index] -= gameGrid[i, j] * gameGrid[i, j + 2];
					}
					else
					{
						if (gameGrid[i, j - 1] == 10) sumRows[index] += gameGrid[i, j];
						else if (gameGrid[i, j - 1] == 11) sumRows[index] -= gameGrid[i, j];
					}
				}
				if (gameGrid[i, gameGrid.GetLength(1) - 2] == 10) sumRows[index] += gameGrid[i, gameGrid.GetLength(1) - 1];
				else if (gameGrid[i, gameGrid.GetLength(1) - 2] == 11) sumRows[index] -= gameGrid[i, gameGrid.GetLength(1) - 1];
				index++;
			}
			index = 0;
			for (int j = 0; j < gameGrid.GetLength(1); j += 2) // columns
			{
				if (gameGrid[1, j] == 12) sumColumns[index] += gameGrid[0, j] * gameGrid[2, j];
				else sumColumns[index] += gameGrid[0, j];
				for (int i = 2; i < gameGrid.GetLength(0) - 2; i += 2)
				{
					if (gameGrid[i - 1, j] == 12) continue;
					else if (gameGrid[i + 1, j] == 12)
					{
						if (gameGrid[i - 1, j] == 10) sumColumns[index] += gameGrid[i, j] * gameGrid[i + 2, j];
						else if (gameGrid[i - 1, j] == 11) sumColumns[index] -= gameGrid[i, j] * gameGrid[i + 2, j];
					}
					else
					{
						if (gameGrid[i - 1, j] == 10) sumColumns[index] += gameGrid[i, j];
						else if (gameGrid[i - 1, j] == 11) sumColumns[index] -= gameGrid[i, j];
					}
				}
				if (gameGrid[gameGrid.GetLength(0) - 2, j] == 10) sumColumns[index] += gameGrid[gameGrid.GetLength(0) - 1, j];
				else if (gameGrid[gameGrid.GetLength(0) - 2, j] == 11) sumColumns[index] -= gameGrid[gameGrid.GetLength(0) - 1, j];
				index++;
			}
		}

		private bool[,] ChooseNumbers(int[,] gameGrid, double ratioChosen) // chooses which numbers will be visible at the start based on the selected ratio of visible numbers
		{
			bool[,] chosenBoxes = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1)];
			int index = 0, chosen = (int)((gameGrid.GetLength(0) / 2 + 1) * (gameGrid.GetLength(1) / 2 + 1) * ratioChosen);
			while (index < chosen)
			{
				int x, y;
				do
				{
					x = random.Next(gameGrid.GetLength(0));
					y = random.Next(gameGrid.GetLength(1));
				} while (chosenBoxes[x, y] || x % 2 != 0 || y % 2 != 0);
				chosenBoxes[x, y] = true;
				index++;
			}
			return chosenBoxes;
		}

		private void GenerateTable(Canvas canvas, Difficulty difficulty, int[,] gameGrid, int[] sumRows, int[] sumColumns, bool[,] chosenBoxes) // draws a math grid into a given canvas
		{
			int count = GetCount(difficulty, false), size = GetSize(difficulty);
			editable = new bool[count - 1, count - 1];
			stateOfBox = new bool[count - 1, count - 1];
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					if (i == count - 1 && j == count - 1) break; // box in the right down corner gets skipped
					else if (j == count - 1 && i % 2 == 0) // sums of rows with numbers
					{
						TextBlock textBlock = GenerateTextBlock(sumRows[i / 2].ToString(), size, Brushes.LightBlue);
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + largeSpace);
					}
					else if (j == count - 1) // sums of rows without numbers
					{
						TextBlock textBlock = GenerateTextBlock(string.Empty, size, Brushes.LightBlue);
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + largeSpace);
					}
					else if (i == count - 1 && j % 2 == 0) // sums of columns with numbers
					{
						TextBlock textBlock = GenerateTextBlock(sumColumns[j / 2].ToString(), size, Brushes.LightBlue);
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + largeSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
					}
					else if (i == count - 1) // sums of columns without numbers
					{
						TextBlock textBlock = GenerateTextBlock(string.Empty, size, Brushes.LightBlue);
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + largeSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
					}
					else if (i % 2 == 0 && j % 2 == 0) // boxes with numbers
					{
						string number = string.Empty;
						if (chosenBoxes[i, j])
						{
							number = gameGrid[i, j].ToString();
							editable[i, j] = true;
						}
						TextBlock textBlock = GenerateTextBlock(number, size, Brushes.White);
						switch (difficulty)
						{
							case Difficulty.VeryEasy:
								canvas.MouseLeftButtonDown += Canvas_MouseDownVeryEasy;
								textBlock.MouseLeftButtonDown += TextBlock_MouseDownVeryEasy;
								textBlock.MouseRightButtonDown += TextBlock_MouseRightButtonDownVeryEasy;
								break;
							case Difficulty.Easy:
								canvas.MouseLeftButtonDown += Canvas_MouseDownEasy;
								textBlock.MouseLeftButtonDown += TextBlock_MouseDownEasy;
								textBlock.MouseRightButtonDown += TextBlock_MouseRightButtonDownEasy;
								break;
							case Difficulty.Medium:
								canvas.MouseLeftButtonDown += Canvas_MouseDownMedium;
								textBlock.MouseLeftButtonDown += TextBlock_MouseDownMedium;
								textBlock.MouseRightButtonDown += TextBlock_MouseRightButtonDownMedium;
								break;
							case Difficulty.Hard:
								canvas.MouseLeftButtonDown += Canvas_MouseDownHard;
								textBlock.MouseLeftButtonDown += TextBlock_MouseDownHard;
								textBlock.MouseRightButtonDown += TextBlock_MouseRightButtonDownHard;
								break;
							case Difficulty.Extreme:
								canvas.MouseLeftButtonDown += Canvas_MouseDownExtreme;
								textBlock.MouseLeftButtonDown += TextBlock_MouseDownExtreme;
								textBlock.MouseRightButtonDown += TextBlock_MouseRightButtonDownExtreme;
								break;
						}
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
					}
					else if (i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0) // boxes with signs (+ - ×)
					{
						string sign = string.Empty;
						if (gameGrid[i, j] == 10) sign = plus;
						else if (gameGrid[i, j] == 11) sign = minus;
						else if (gameGrid[i, j] == 12) sign = multiply;
						TextBlock textBlock = GenerateTextBlock(sign, size, Brushes.LightGray);
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
					}
					else // empty boxes
					{
						TextBlock textBlock = GenerateTextBlock(string.Empty, size, Brushes.Gray);
						canvas.Children.Add(textBlock);
						Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
						Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
					}
				}
			}
		}

		private TextBlock GenerateTextBlock(string text, int size, Brush background)
		{
			TextBlock textBlock = new TextBlock
			{
				Height = size,
				Width = size,
				TextAlignment = TextAlignment.Center,
				FontSize = size / 1.5,
				Text = text
			};
			if (text != string.Empty) textBlock.FontStyle = FontStyles.Italic;
			textBlock.Background = background;
			return textBlock;
		}

		/// <summary>
		/// Calls all the methods needed to create a new game.
		/// </summary>
		internal void NewGame(Canvas canvas, Difficulty difficulty, double ratioOfChosenNumbers)
		{
			canvas.Children.Clear();
			int count = GetCount(difficulty, false);
			int[,] gameGrid = GenerateNumbers(difficulty);
			entered = new int[count, count];
			SumGame(gameGrid, out int[] sumRows, out int[] sumColumns);
			bool[,] chosen = ChooseNumbers(gameGrid, ratioOfChosenNumbers);
			GenerateTable(canvas, difficulty, gameGrid, sumRows, sumColumns, chosen);
		}

		/// <summary>
		/// Clears all the entered numbers that are not fixed.
		/// </summary>
		internal void Reset(Canvas canvas)
		{
			foreach (TextBlock textBlock in canvas.Children)
			{
				if (textBlock.Foreground == Brushes.Blue) textBlock.Text = string.Empty;
				if (textBlock.Background == Brushes.Yellow) textBlock.Background = Brushes.White;
			}
			for (int i = 0; i < entered.GetLength(0); i++)
			{
				for (int j = 0; j < entered.GetLength(1); j++)
				{
					entered[i, j] = 0;
				}
			}
		}

		/// <summary>
		/// Puts a new number inside a game canvas and checks the game.
		/// </summary>
		/// <returns>Returns one of three possible outcomes.</returns>
		internal Result EnterNumber(Canvas canvas, Difficulty difficulty, int number)
		{
			int px, py, size = GetSize(difficulty);
			foreach (TextBlock textBlock in canvas.Children)
			{
				if (textBlock.Background == Brushes.Yellow && (textBlock.Text == string.Empty || textBlock.Foreground == Brushes.Blue))
				{
					px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
					py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
					if (textBlock.Text == number.ToString())
					{
						textBlock.Text = string.Empty;
						entered[px, py] = 0;
					}
					else
					{
						textBlock.Text = number.ToString();
						textBlock.Foreground = Brushes.Blue;
						entered[px, py] = number;
					}
					break;
				}
			}

			// game check - if the grid is filled with numbers, checks if it's filled correctly
			bool filled = true;
			foreach (TextBlock textBlock in canvas.Children)
			{
				if ((textBlock.Background == Brushes.White || textBlock.Background == Brushes.Yellow) && textBlock.Text == string.Empty)
				{
					filled = false;
					break;
				}
			}
			if (filled)
			{
				if (GameCheck(canvas, difficulty)) return Result.Success;
				else return Result.Fail;
			}
			else return Result.Unfinished;
		}

		/// <summary>
		/// Moves a selected field one step away from the currently selected one.
		/// </summary>
		internal void ChangeBoxWithArrows(Canvas canvas, Difficulty difficulty, Direction direction)
		{
			int px, py, qx, qy, size = GetSize(difficulty);
			bool wantedBox = false;
			foreach (TextBlock textBlock in canvas.Children)
			{
				if (textBlock.Background == Brushes.Yellow || textBlock.Background == Brushes.Red)
				{
					px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
					py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
					foreach (TextBlock textBlock2 in canvas.Children)
					{
						qx = Convert.ToInt32(Canvas.GetLeft(textBlock2)) / (size + smallSpace);
						qy = Convert.ToInt32(Canvas.GetTop(textBlock2)) / (size + smallSpace);
						switch (direction)
						{
							case Direction.Up:
								if (qy == py - 2 && qx == px) wantedBox = true;
								break;
							case Direction.Right:
								if (qx == px + 2 && qy == py) wantedBox = true;
								break;
							case Direction.Down:
								if (qy == py + 2 && qx == px) wantedBox = true;
								break;
							case Direction.Left:
								if (qx == px - 2 && qy == py) wantedBox = true;
								break;
						}
						if (wantedBox)
						{
							if (textBlock2.FontStyle == FontStyles.Italic) textBlock2.Background = Brushes.Red;
							else textBlock2.Background = Brushes.Yellow;
							textBlock.Background = Brushes.White;
							break;
						}
					}
					break;
				}
			}
		}

		private bool GameCheck(Canvas canvas, Difficulty difficulty) // returns true when the game is filled correctly
		{
			int count = GetCount(difficulty, true), size = GetSize(difficulty);
			int px, py, indexX = 0, indexY = 0;
			string sum1String = string.Empty, sum2String = string.Empty;
			int[,] gameGrid = new int[count, count];
			int[] sum1 = new int[count + 1];
			int[] sum2 = new int[count + 1];
			foreach (TextBlock textBlock in canvas.Children)
			{
				if (textBlock.Background == Brushes.LightBlue && textBlock.Text != string.Empty)
				{
					sum1[indexX] = Convert.ToInt32(textBlock.Text.ToString());
					indexX++;
				}
				else if (textBlock.Background != Brushes.LightBlue)
				{
					px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
					py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
					if (textBlock.Text == string.Empty) gameGrid[px, py] = 0;
					else if (textBlock.Text == plus) gameGrid[px, py] = 10;
					else if (textBlock.Text == minus) gameGrid[px, py] = 11;
					else if (textBlock.Text == multiply) gameGrid[px, py] = 12;
					else gameGrid[px, py] = int.Parse(textBlock.Text);
				}
			}
			SumGame(gameGrid, out int[] sumRows, out int[] sumColumns);
			for (int i = 0; i < sumRows.Length; i++)
			{
				sum2[indexY] = sumRows[i];
				indexY++;
			}
			for (int i = 0; i < sumColumns.Length; i++)
			{
				sum2[indexY] = sumColumns[i];
				indexY++;
			}
			for (int i = 0; i < sum1.Length; i++) sum1String += sum1[i] + " ";
			for (int i = 0; i < sum2.Length; i++) sum2String += sum2[i] + " ";
			if (sum1String == sum2String) return true;
			else return false;
		}

		private int GetCount(Difficulty difficulty, bool lowerCountByOne)
		{
			switch (difficulty)
			{
				case Difficulty.VeryEasy:
					return lowerCountByOne ? countVeryEasy - 1 : countVeryEasy;
				case Difficulty.Easy:
					return lowerCountByOne ? countEasy - 1 : countEasy;
				case Difficulty.Medium:
					return lowerCountByOne ? countMedium - 1 : countMedium;
				case Difficulty.Hard:
					return lowerCountByOne ? countHard - 1 : countHard;
				case Difficulty.Extreme:
					return lowerCountByOne ? countExtreme - 1 : countExtreme;
				default:
					return 0;
			}
		}

		private int GetSize(Difficulty difficulty)
		{
			switch (difficulty)
			{
				case Difficulty.VeryEasy:
					return sizeVeryEasy;
				case Difficulty.Easy:
					return sizeEasy;
				case Difficulty.Medium:
					return sizeMedium;
				case Difficulty.Hard:
					return sizeHard;
				case Difficulty.Extreme:
					return sizeExtreme;
				default:
					return 0;
			}
		}
	}
}
