using GameLogic;
using GameLogic.Enums;
using GameLogic.Extensions;
using GameLogic.Services.MatrixGeneration;
using GameLogic.Services.MatrixSum;
using GameLogic.Services.MatrixVisibility;
using MathGrid.Enums;
using MathGrid.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MathGrid.Services.Game
{
    /// <summary>
    /// Contains methods that manipulate <see cref="Canvas"/> and its children while the game is generated and played. It also notifies the caller about the state of the game.
    /// </summary>
    public class GameService(
        IMatrixGenerationService matrixGenerationService,
        IMatrixSumService matrixSumService,
        IMatrixVisibilityService matrixVisibilityService)
    {
        private readonly IMatrixGenerationService _matrixGenerationService = matrixGenerationService;
        private readonly IMatrixSumService _matrixSumService = matrixSumService;
        private readonly IMatrixVisibilityService _matrixVisibilityService = matrixVisibilityService;

        private short chosenBox;
        private byte[,]? enteredNumbers; // array of numbers entered by the player
        private bool[,]? stateOfBox; // serves to de-color a box when selecting a different one
        private bool[,]? editable; // red color of an occupied box

        private void TextBlock_MouseDownExtreme(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchBox(textBlock, Constants.SizeExtreme, Constants.CountExtreme);
        }

        private void TextBlock_MouseDownHard(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchBox(textBlock, Constants.SizeHard, Constants.CountHard);
        }

        private void TextBlock_MouseDownMedium(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchBox(textBlock, Constants.SizeMedium, Constants.CountMedium);
        }

        private void TextBlock_MouseDownEasy(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchBox(textBlock, Constants.SizeEasy, Constants.CountEasy);
        }

        private void TextBlock_MouseDownVeryEasy(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchBox(textBlock, Constants.SizeVeryEasy, Constants.CountVeryEasy);
        }

        private void SwitchBox(TextBlock textBlock, short size, byte count) // it happens when clicked on a box and only when its color is white
        {
            if (editable is null || textBlock.Background != Brushes.White)
                return;

            sbyte px, py;
            px = GetHorizontalPosition(textBlock, size);
            py = GetVerticalPosition(textBlock, size);

            SwitchStateOfBox(px, py);

            textBlock.Background = editable[px, py]
                ? Brushes.Red
                : Brushes.Yellow;

            chosenBox = GetChosenBoxNumber(px, py, count);
        }

        private void SwitchFixation(TextBlock textBlock, short size, byte count) // changes the fixation of a number - right mouse button click
        {
            if (editable is null || (textBlock.Background != Brushes.White && textBlock.Background != Brushes.Yellow))
                return;

            sbyte px, py;
            px = GetHorizontalPosition(textBlock, size);
            py = GetVerticalPosition(textBlock, size);

            if (!editable[px, py])
            {
                textBlock.Foreground = textBlock.Foreground == Brushes.Black
                    ? textBlock.Foreground = Brushes.Blue
                    : textBlock.Foreground = Brushes.Black;
            }
            chosenBox = GetChosenBoxNumber(px, py, count);
        }

        /// <summary>
        /// Changes whether the number is fixed or not.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="difficulty">Difficulty enum.</param>
        internal void FixateNumber(Canvas canvas, Difficulty difficulty)
        {
            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Background == Brushes.Yellow && textBlock.Text != string.Empty)
                {
                    switch (difficulty)
                    {
                        case Difficulty.VeryEasy:
                            SwitchFixation(textBlock, Constants.SizeVeryEasy, Constants.CountVeryEasy);
                            break;
                        case Difficulty.Easy:
                            SwitchFixation(textBlock, Constants.SizeEasy, Constants.CountEasy);
                            break;
                        case Difficulty.Medium:
                            SwitchFixation(textBlock, Constants.SizeMedium, Constants.CountMedium);
                            break;
                        case Difficulty.Hard:
                            SwitchFixation(textBlock, Constants.SizeHard, Constants.CountHard);
                            break;
                        case Difficulty.Extreme:
                            SwitchFixation(textBlock, Constants.SizeExtreme, Constants.CountExtreme);
                            break;
                    }
                    break;
                }
            }
        }

        private short GetChosenBoxNumber(short px, short py, byte count)
            => (short)(py + px * count);


        private void Canvas_MouseDownExtreme(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas canvas)
                return;
            UnselectBox(canvas);
        }

        private void Canvas_MouseDownHard(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas canvas)
                return;
            UnselectBox(canvas);
        }

        private void Canvas_MouseDownMedium(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas canvas)
                return;
            UnselectBox(canvas);
        }

        private void Canvas_MouseDownEasy(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas canvas)
                return;
            UnselectBox(canvas);
        }

        private void Canvas_MouseDownVeryEasy(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas canvas)
                return;
            UnselectBox(canvas);
        }

        private void UnselectBox(Canvas canvas) // when canvas is clicked on, the yellow or red box turns white
        {
            short i = 0;
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
            if (sender is not TextBlock textBlock)
                return;
            SwitchFixation(textBlock, Constants.SizeExtreme, Constants.CountExtreme);
        }

        private void TextBlock_MouseRightButtonDownHard(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchFixation(textBlock, Constants.SizeHard, Constants.CountHard);
        }

        private void TextBlock_MouseRightButtonDownMedium(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchFixation(textBlock, Constants.SizeMedium, Constants.CountMedium);
        }

        private void TextBlock_MouseRightButtonDownEasy(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchFixation(textBlock, Constants.SizeEasy, Constants.CountEasy);
        }

        private void TextBlock_MouseRightButtonDownVeryEasy(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;
            SwitchFixation(textBlock, Constants.SizeVeryEasy, Constants.CountVeryEasy);
        }

        private void SwitchStateOfBox(short x, short y) // sets states of all boxes to false and then sets the demanded one to true
        {
            if (stateOfBox is null)
                return;

            for (byte i = 0; i < stateOfBox.GetLength(0); i++)
            {
                for (byte j = 0; j < stateOfBox.GetLength(1); j++)
                {
                    stateOfBox[i, j] = false;
                }
            }
            stateOfBox[x, y] = true;
        }

        private void GenerateTable(Canvas canvas, Difficulty difficulty, byte[,] gameGrid, short[] sumRows, short[] sumColumns, bool[,] chosenBoxes) // draws a math grid into a given canvas
        {
            byte count = difficulty.ToGridSize();
            short size = difficulty.ToCellSize();
            editable = new bool[count - 1, count - 1];
            stateOfBox = new bool[count - 1, count - 1];

            for (byte i = 0; i < count; i++)
            {
                for (byte j = 0; j < count; j++)
                {
                    if (i == count - 1 && j == count - 1)
                        break; // box in the right down corner gets skipped
                    else if (j == count - 1 && i % 2 == 0) // sums of rows with numbers
                    {
                        TextBlock textBlock = GenerateTextBlock(sumRows[i / 2].ToString(), size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.SmallSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.LargeSpace);
                    }
                    else if (j == count - 1) // sums of rows without numbers
                    {
                        TextBlock textBlock = GenerateTextBlock(string.Empty, size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.SmallSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.LargeSpace);
                    }
                    else if (i == count - 1 && j % 2 == 0) // sums of columns with numbers
                    {
                        TextBlock textBlock = GenerateTextBlock(sumColumns[j / 2].ToString(), size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.LargeSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.SmallSpace);
                    }
                    else if (i == count - 1) // sums of columns without numbers
                    {
                        TextBlock textBlock = GenerateTextBlock(string.Empty, size, Brushes.LightBlue);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.LargeSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.SmallSpace);
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
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.SmallSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.SmallSpace);
                    }
                    else if (i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0) // boxes with signs (+ - ×)
                    {
                        string sign = gameGrid[i, j] switch
                        {
                            Constants.PlusRepresentation => Constants.PlusSign,
                            Constants.MinusRepresentation => Constants.MinusSign,
                            Constants.MultiplicationRepresentation => Constants.MultiplicationSign,
                            _ => string.Empty
                        };

                        TextBlock textBlock = GenerateTextBlock(sign, size, Brushes.LightGray);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.SmallSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.SmallSpace);
                    }
                    else // empty boxes
                    {
                        TextBlock textBlock = GenerateTextBlock(string.Empty, size, Brushes.Gray);
                        canvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, i * (size + Constants.SmallSpace) + Constants.SmallSpace);
                        Canvas.SetTop(textBlock, j * (size + Constants.SmallSpace) + Constants.SmallSpace);
                    }
                }
            }
        }

        private TextBlock GenerateTextBlock(string text, short size, Brush background)
        {
            TextBlock textBlock = new()
            {
                Height = size,
                Width = size,
                TextAlignment = TextAlignment.Center,
                FontSize = size / 1.5,
                Text = text
            };
            if (text != string.Empty)
                textBlock.FontStyle = FontStyles.Italic;
            textBlock.Background = background;
            return textBlock;
        }

        /// <summary>
        /// Calls all the methods needed to create a new game.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <param name="ratioOfVisibleNumbers">Ratio of visible numbers.</param>
        public void NewGame(Canvas canvas, Difficulty difficulty, double ratioOfVisibleNumbers)
        {
            canvas.Children.Clear();
            byte count = difficulty.ToGridSizeWithoutSums();

            byte[,] gameGrid = _matrixGenerationService.GenerateMatrix(difficulty);
            enteredNumbers = new byte[count, count];
            _matrixSumService.SumMatrix(gameGrid, out short[] sumRows, out short[] sumColumns);
            bool[,] chosen = _matrixVisibilityService.ChooseVisibleNumbers(count, ratioOfVisibleNumbers);

            GenerateTable(canvas, difficulty, gameGrid, sumRows, sumColumns, chosen);
        }

        /// <summary>
        /// Clears all the entered numbers that are not fixed.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public void Reset(Canvas canvas)
        {
            if (enteredNumbers is null)
                return;

            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Foreground == Brushes.Blue)
                    textBlock.Text = string.Empty;
                if (textBlock.Background == Brushes.Yellow)
                    textBlock.Background = Brushes.White;
            }
            for (byte i = 0; i < enteredNumbers.GetLength(0); i++)
            {
                for (byte j = 0; j < enteredNumbers.GetLength(1); j++)
                {
                    enteredNumbers[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Puts a new number inside a game canvas and checks the game.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <param name="number">Entered number.</param>
        /// <returns>Current game state.</returns>
        public GameState EnterNumber(Canvas canvas, Difficulty difficulty, string number)
        {
            if (enteredNumbers is null)
                return GameState.Unfinished;

            sbyte px, py;
            short size = difficulty.ToCellSize();
            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Background == Brushes.Yellow && (textBlock.Text == string.Empty || textBlock.Foreground == Brushes.Blue))
                {
                    px = GetHorizontalPosition(textBlock, size);
                    py = GetVerticalPosition(textBlock, size);
                    if (textBlock.Text == number)
                    {
                        textBlock.Text = string.Empty;
                        enteredNumbers[px, py] = 0;
                    }
                    else
                    {
                        textBlock.Text = number;
                        textBlock.Foreground = Brushes.Blue;
                        enteredNumbers[px, py] = byte.Parse(number);
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
                return GameCheck(canvas, difficulty)
                    ? GameState.Success
                    : GameState.Fail;
            else
                return GameState.Unfinished;
        }

        /// <summary>
        /// Moves a selected field one step away from the currently selected one.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <param name="direction">Direction enum.</param>
        public void ChangeBoxWithArrows(Canvas canvas, Difficulty difficulty, Direction direction)
        {
            sbyte px, py, qx, qy;
            short size = difficulty.ToCellSize();
            bool wantedBox = false, start = true;
            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Background == Brushes.Yellow || textBlock.Background == Brushes.Red)
                {
                    start = false;
                    px = GetHorizontalPosition(textBlock, size);
                    py = GetVerticalPosition(textBlock, size);

                    foreach (TextBlock textBlock2 in canvas.Children)
                    {
                        qx = GetHorizontalPosition(textBlock2, size);
                        qy = GetVerticalPosition(textBlock2, size);
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
                            textBlock2.Background = textBlock2.FontStyle == FontStyles.Italic
                                ? Brushes.Red
                                : Brushes.Yellow;

                            textBlock.Background = Brushes.White;
                            break;
                        }
                    }
                    break;
                }
            }

            if (start)
            {
                foreach (TextBlock textBlock in canvas.Children)
                {
                    textBlock.Background = textBlock.FontStyle == FontStyles.Italic
                        ? Brushes.Red
                        : Brushes.Yellow;
                    break;
                }
            }
        }

        private bool GameCheck(Canvas canvas, Difficulty difficulty) // returns true when the game is filled correctly
        {
            byte count = difficulty.ToGridSizeWithoutSums(), indexX = 0, indexY = 0;
            short size = difficulty.ToCellSize();
            sbyte px, py;
            string sum1String = string.Empty, sum2String = string.Empty;
            byte[,] gameGrid = new byte[count, count];
            short[] sum1 = new short[count + 1];
            short[] sum2 = new short[count + 1];

            foreach (TextBlock textBlock in canvas.Children)
            {
                if (textBlock.Background == Brushes.LightBlue && textBlock.Text != string.Empty)
                {
                    sum1[indexX] = Convert.ToInt16(textBlock.Text.ToString());
                    indexX++;
                }
                else if (textBlock.Background != Brushes.LightBlue)
                {
                    px = GetHorizontalPosition(textBlock, size);
                    py = GetVerticalPosition(textBlock, size);

                    if (textBlock.Text == string.Empty)
                        gameGrid[px, py] = 0;
                    else if (textBlock.Text == Constants.PlusSign)
                        gameGrid[px, py] = Constants.PlusRepresentation;
                    else if (textBlock.Text == Constants.MinusSign)
                        gameGrid[px, py] = Constants.MinusRepresentation;
                    else if (textBlock.Text == Constants.MultiplicationSign)
                        gameGrid[px, py] = Constants.MultiplicationRepresentation;
                    else
                        gameGrid[px, py] = byte.Parse(textBlock.Text);
                }
            }

            _matrixSumService.SumMatrix(gameGrid, out short[] sumRows, out short[] sumColumns);
            for (byte i = 0; i < sumRows.Length; i++)
            {
                sum2[indexY] = sumRows[i];
                indexY++;
            }
            for (byte i = 0; i < sumColumns.Length; i++)
            {
                sum2[indexY] = sumColumns[i];
                indexY++;
            }

            sum1String = string.Join(" ", sum1String);
            sum2String = string.Join(" ", sum2String);
            return sum1String == sum2String;
        }

        private sbyte GetHorizontalPosition(TextBlock textBlock, short cellSize)
            => (sbyte)(Canvas.GetLeft(textBlock) / (cellSize + Constants.SmallSpace));

        private sbyte GetVerticalPosition(TextBlock textBlock, short cellSize)
            => (sbyte)(Canvas.GetTop(textBlock) / (cellSize + Constants.SmallSpace));
    }
}
