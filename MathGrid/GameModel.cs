using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MathGrid
{
    class GameModel // a class with algortihms for the game; also includes WPF controls
    {
        public Random r = new Random();
        private const int smallSpace = 2, largeSpace = 4,
            sizeVeryEasy = 98, sizeEasy = 73, sizeMedium = 58, sizeHard = 48, sizeExtreme = 28,
            countVeryEasy = 6, countEasy = 8, countMedium = 10, countHard = 12, countExtreme = 20; // constants needed to generate the grid
        private int chosenBox;
        private int[,] entered; // array of numbers entered by the player
        private bool[,] stateOfBox; // serves to de-color a box when selecting a different one
        private bool[,] selectable; // red color of an occupied box

        private void Rectangle_MouseDownExtreme(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchBox(textBlock, sizeExtreme, countExtreme);
        }

        private void Rectangle_MouseDownHard(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchBox(textBlock, sizeHard, countHard);
        }

        private void Rectangle_MouseDownMedium(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchBox(textBlock, sizeMedium, countMedium);
        }

        private void Rectangle_MouseDownEasy(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchBox(textBlock, sizeEasy, countEasy);
        }

        private void Rectangle_MouseDownVeryEasy(object sender, MouseButtonEventArgs e)
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
                if (selectable[px, py]) textBlock.Background = Brushes.Red;
                else textBlock.Background = Brushes.Yellow;
                chosenBox = py + (px * count);
            }
        }

        private void SwitchEditable(TextBlock textBlock, int size, int count) // changes the editability of a number - right mouse button click
        {
            if (textBlock.Background == Brushes.White || textBlock.Background == Brushes.Yellow)
            {
                int px, py;
                px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
                py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
                if (!selectable[px, py])
                {
                    if (textBlock.Foreground == Brushes.Black) textBlock.Foreground = Brushes.Blue;
                    else textBlock.Foreground = Brushes.Black;
                }
                chosenBox = py + (px * count);
            }
        }

        private void Canvas_MouseDownExtreme(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            BoxesTurnWhite(canvas, sizeExtreme);
        }

        private void Canvas_MouseDownHard(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            BoxesTurnWhite(canvas, sizeHard);
        }

        private void Canvas_MouseDownMedium(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            BoxesTurnWhite(canvas, sizeMedium);
        }

        private void Canvas_MouseDownEasy(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            BoxesTurnWhite(canvas, sizeEasy);
        }

        private void Canvas_MouseDownVeryEasy(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            BoxesTurnWhite(canvas, sizeVeryEasy);
        }

        private void BoxesTurnWhite(Canvas canvas, int velikost) // when canvas is clicked on, all the boxes with numbers except the chosen one turn white
        {
            int i = 0;
            foreach (TextBlock textBlock in canvas.Children)
            {
                int px, py;
                px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (velikost + smallSpace);
                py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (velikost + smallSpace);
                if (textBlock.Background != Brushes.Gray && textBlock.Background != Brushes.LightGray && textBlock.Background != Brushes.LightBlue && i != chosenBox) textBlock.Background = Brushes.White;
                i++;
            }
        }

        private void Rectangle_MouseRightButtonDownExtreme(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchEditable(textBlock, sizeExtreme, countExtreme);
        }

        private void Rectangle_MouseRightButtonDownHard(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchEditable(textBlock, sizeHard, countHard);
        }

        private void Rectangle_MouseRightButtonDownMedium(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchEditable(textBlock, sizeMedium, countMedium);
        }

        private void Rectangle_MouseRightButtonDownEasy(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchEditable(textBlock, sizeEasy, countEasy);
        }

        public void Rectangle_MouseRightButtonDownVeryEasy(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            SwitchEditable(textBlock, sizeVeryEasy, countVeryEasy);
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

        public int[,] GenerateNumbers(Difficulty difficulty) // generates a new game grid
        {
            GetSizeAndCount(difficulty, true, out int count);
            int[,] a = new int[count, count]; // array of numbers that is then translated to the game
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0) a[i, j] = r.Next(1, 10);
                    else if (i % 2 != 0 && j % 2 != 0) a[i, j] = 0;
                    else
                    {
                        if (i == 1 && j == 0 || i == 0 && j == 1) a[i, j] = r.Next(10, 13);
                        else
                        {
                            if (i == 0 || i == 1)
                            {
                                if (a[i, j - 2] == 12) a[i, j] = r.Next(10, 12);
                                else a[i, j] = r.Next(10, 13);
                            }
                            else if (j == 0 || j == 1)
                            {
                                if (a[i - 2, j] == 12) a[i, j] = r.Next(10, 12);
                                else a[i, j] = r.Next(10, 13);
                            }
                            else if (a[i, j - 2] == 12 || a[i - 2, j] == 12) a[i, j] = r.Next(10, 12);
                            else a[i, j] = r.Next(10, 13);
                        }
                    }
                }
            }
            return a;
        }

        public void SumGame(int[,] gameGrid, out int[] sumRows, out int[] sumColumns) // calculates sums of all rows and columns and returns them
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

        public bool[,] ChooseNumbers(int[,] gameGrid, double ratioChosen) // chooses which numbers will be visible at the start based on the selected ratio of visible numbers
        {
            bool[,] chosenBoxes = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1)];
            int index = 0, chosen = (int)((gameGrid.GetLength(0) / 2 + 1) * (gameGrid.GetLength(1) / 2 + 1) * ratioChosen);
            while (index < chosen)
            {
                int x, y;
                do
                {
                    x = r.Next(gameGrid.GetLength(0));
                    y = r.Next(gameGrid.GetLength(1));
                } while (chosenBoxes[x, y] || x % 2 != 0 || y % 2 != 0);
                chosenBoxes[x, y] = true;
                index++;
            }
            return chosenBoxes;
        }

        public void GenerateTable(Canvas canvas, Difficulty difficulty, int[,] gameGrid, int[] sumRows, int[] sumColums, bool[,] chosenBoxes)
        {
            GetSizeAndCount(difficulty, false, out int count, out int size);
            selectable = new bool[count - 1, count - 1];
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
                        TextBlock textBlock = GenerateTextBlock("", size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
                        Canvas.SetTop(textBlock, j * (size + smallSpace) + largeSpace);
                    }
                    else if (i == count - 1 && j % 2 == 0) // sums of columns with numbers
                    {
                        TextBlock textBlock = GenerateTextBlock(sumColums[j / 2].ToString(), size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + smallSpace) + largeSpace);
                        Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
                    }
                    else if (i == count - 1) // sums of columns without numbers
                    {
                        TextBlock textBlock = GenerateTextBlock("", size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + smallSpace) + largeSpace);
                        Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
                    }
                    else if (i % 2 == 0 && j % 2 == 0) // boxes with numbers
                    {
                        string number = "";
                        if (chosenBoxes[i, j])
                        {
                            number = gameGrid[i, j].ToString();
                            selectable[i, j] = true;
                        }
                        TextBlock textBlock = GenerateTextBlock(number, size, Brushes.White);
                        switch (difficulty)
                        {
                            case Difficulty.VeryEasy:
                                canvas.MouseLeftButtonDown += Canvas_MouseDownVeryEasy;
                                textBlock.MouseLeftButtonDown += Rectangle_MouseDownVeryEasy;
                                textBlock.MouseRightButtonDown += Rectangle_MouseRightButtonDownVeryEasy;
                                break;
                            case Difficulty.Easy:
                                canvas.MouseLeftButtonDown += Canvas_MouseDownEasy;
                                textBlock.MouseLeftButtonDown += Rectangle_MouseDownEasy;
                                textBlock.MouseRightButtonDown += Rectangle_MouseRightButtonDownEasy;
                                break;
                            case Difficulty.Medium:
                                canvas.MouseLeftButtonDown += Canvas_MouseDownMedium;
                                textBlock.MouseLeftButtonDown += Rectangle_MouseDownMedium;
                                textBlock.MouseRightButtonDown += Rectangle_MouseRightButtonDownMedium;
                                break;
                            case Difficulty.Hard:
                                canvas.MouseLeftButtonDown += Canvas_MouseDownHard;
                                textBlock.MouseLeftButtonDown += Rectangle_MouseDownHard;
                                textBlock.MouseRightButtonDown += Rectangle_MouseRightButtonDownHard;
                                break;
                            case Difficulty.Extreme:
                                canvas.MouseLeftButtonDown += Canvas_MouseDownExtreme;
                                textBlock.MouseLeftButtonDown += Rectangle_MouseDownExtreme;
                                textBlock.MouseRightButtonDown += Rectangle_MouseRightButtonDownExtreme;
                                break;
                        }
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
                        Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
                    }
                    else if (i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0) // boxes with signs (+ - ×)
                    {
                        string sign = "";
                        if (gameGrid[i, j] == 10) sign = "+";
                        else if (gameGrid[i, j] == 11) sign = "-";
                        else if (gameGrid[i, j] == 12) sign = "×";
                        TextBlock textBlock = GenerateTextBlock(sign, size, Brushes.LightGray);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + smallSpace) + smallSpace);
                        Canvas.SetTop(textBlock, j * (size + smallSpace) + smallSpace);
                    }
                    else // empty boxes
                    {
                        TextBlock textBlock = GenerateTextBlock("", size, Brushes.Gray);
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
            if (text != "") textBlock.FontStyle = FontStyles.Italic;
            textBlock.Background = background;
            return textBlock;
        }

        public void NewGame(Canvas canvas, Difficulty difficulty, double ratioOfChosenNumbers)
        {
            canvas.Children.Clear();
            GetSizeAndCount(difficulty, false, out int count);
            int[,] gameGrid = GenerateNumbers(difficulty);
            entered = new int[count, count];
            SumGame(gameGrid, out int[] sumRows, out int[] sumColumns);
            bool[,] chosen = ChooseNumbers(gameGrid, ratioOfChosenNumbers);
            GenerateTable(canvas, difficulty, gameGrid, sumRows, sumColumns, chosen);
        }

        public Result EnterNumber(Canvas canvas, Difficulty difficulty, int number)
        {
            int px, py, size = GetSize(difficulty);
            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Background == Brushes.Yellow && (textBlock.Text == "" || textBlock.Foreground == Brushes.Blue))
                {
                    px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
                    py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
                    if (textBlock.Text == number.ToString())
                    {
                        textBlock.Text = "";
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
                if ((textBlock.Background == Brushes.White || textBlock.Background == Brushes.Yellow) && textBlock.Text == "")
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

        public void ChangeBoxWithArrows(Canvas canvas, Difficulty difficulty, Direction direction)
        {
            int px, py, qx, qy;
            bool wantedBox = false;
            GetSizeAndCount(difficulty, false, out _, out int size);
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

        public bool GameCheck(Canvas canvas, Difficulty difficulty)
        {
            GetSizeAndCount(difficulty, true, out int count, out int size);
            int px, py, indexX = 0, indexY = 0;
            string rowString = "", columnString = "";
            int[,] gameGrid = new int[count, count];
            int[] sum1 = new int[count + 1];
            int[] sum2 = new int[count + 1];
            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Background == Brushes.LightBlue && textBlock.Text != "")
                {
                    sum1[indexX] = Convert.ToInt32(textBlock.Text.ToString());
                    indexX++;
                }
                else if (textBlock.Background != Brushes.LightBlue)
                {
                    px = Convert.ToInt32(Canvas.GetLeft(textBlock)) / (size + smallSpace);
                    py = Convert.ToInt32(Canvas.GetTop(textBlock)) / (size + smallSpace);
                    if (textBlock.Text == "") gameGrid[px, py] = 0;
                    else if (textBlock.Text == "+") gameGrid[px, py] = 10;
                    else if (textBlock.Text == "-") gameGrid[px, py] = 11;
                    else if (textBlock.Text == "×") gameGrid[px, py] = 12;
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
            for (int i = 0; i < sum1.Length; i++) rowString += sum1[i] + " ";
            for (int i = 0; i < sum2.Length; i++) columnString += sum2[i] + " ";
            if (rowString == columnString) return true;
            else return false;
        }

        public void Reset(Canvas canvas)
        {
            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Foreground == Brushes.Blue) textBlock.Text = "";
                if (textBlock.Background == Brushes.Yellow) textBlock.Background = Brushes.White;
            }
            for (int i = 0; i < entered.GetLength(0); i++)
            {
                for (int j = 0; j < entered.GetLength(1); j++)
                {
                    entered[j, i] = 0;
                }
            }
        }

        private void GetSizeAndCount(Difficulty difficulty, bool lowerCountByOne, out int count)
        {
            switch (difficulty)
            {
                case Difficulty.VeryEasy:
                    count = countVeryEasy;
                    break;
                case Difficulty.Easy:
                    count = countEasy;
                    break;
                case Difficulty.Medium:
                    count = countMedium;
                    break;
                case Difficulty.Hard:
                    count = countHard;
                    break;
                case Difficulty.Extreme:
                    count = countExtreme;
                    break;
                default:
                    count = 0;
                    break;
            }
            if (lowerCountByOne) count--;
        }

        private void GetSizeAndCount(Difficulty difficulty, bool lowerCountByOne, out int count, out int size)
        {
            switch (difficulty)
            {
                case Difficulty.VeryEasy:
                    size = sizeVeryEasy;
                    count = countVeryEasy;
                    break;
                case Difficulty.Easy:
                    size = sizeEasy;
                    count = countEasy;
                    break;
                case Difficulty.Medium:
                    size = sizeMedium;
                    count = countMedium;
                    break;
                case Difficulty.Hard:
                    size = sizeHard;
                    count = countHard;
                    break;
                case Difficulty.Extreme:
                    size = sizeExtreme;
                    count = countExtreme;
                    break;
                default:
                    size = 0;
                    count = 0;
                    break;
            }
            if (lowerCountByOne) count--;
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
