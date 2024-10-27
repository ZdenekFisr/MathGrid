using GameLogic.Enums;

namespace GameLogic.Extensions
{
    public static class GridSizeExtensions
    {
        /// <summary>
        /// Converts difficulty to a number of rows and columns of the grid.
        /// </summary>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <returns>Grid size.</returns>
        public static byte ToGridSize(this Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.VeryEasy => Constants.CountVeryEasy,
                Difficulty.Easy => Constants.CountEasy,
                Difficulty.Medium => Constants.CountMedium,
                Difficulty.Hard => Constants.CountHard,
                Difficulty.Extreme => Constants.CountExtreme,
                _ => 0
            };
        }

        /// <summary>
        /// Converts difficulty to a number of rows and columns of the grid, excluding the sum row and column.
        /// </summary>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <returns>Grid size lowered by one.</returns>
        public static byte ToGridSizeWithoutSums(this Difficulty difficulty)
            => (byte)(difficulty.ToGridSize() - 1);
    }
}
