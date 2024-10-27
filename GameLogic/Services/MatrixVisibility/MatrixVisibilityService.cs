using GameLogic.Services.RandomNumber;

namespace GameLogic.Services.MatrixVisibility
{
    /// <inheritdoc cref="IMatrixVisibilityService"/>
    public class MatrixVisibilityService(
        IRandomNumberService randomNumberService)
        : IMatrixVisibilityService
    {
        private readonly IRandomNumberService _randomNumberService = randomNumberService;

        /// <inheritdoc cref="IMatrixVisibilityService.ChooseVisibleNumbers(byte, double)"/>
        public bool[,] ChooseVisibleNumbers(byte gridSize, double ratioOfVisibleNumbers)
        {
            bool[,] visibleNumbers = new bool[gridSize, gridSize];
            int numberOfVisibleNumbers = (int)((gridSize + 1) * (gridSize + 1) / 4 * ratioOfVisibleNumbers);
            int x, y;

            for (int i = 0; i < numberOfVisibleNumbers; i++)
            {
                do
                {
                    x = _randomNumberService.GenerateRandomNumber(0, (byte)(gridSize - 1));
                    y = _randomNumberService.GenerateRandomNumber(0, (byte)(gridSize - 1));
                } while (visibleNumbers[x, y] || x % 2 != 0 || y % 2 != 0);

                visibleNumbers[x, y] = true;
            }
            return visibleNumbers;
        }
    }
}
