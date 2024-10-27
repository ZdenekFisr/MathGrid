using GameLogic.Enums;

namespace GameLogic.Extensions
{
    public static class CellSizeExtensions
    {
        /// <summary>
        /// Converts difficulty to a cell size.
        /// </summary>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <returns>Cell size.</returns>
        public static short ToCellSize(this Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.VeryEasy => Constants.SizeVeryEasy,
                Difficulty.Easy => Constants.SizeEasy,
                Difficulty.Medium => Constants.SizeMedium,
                Difficulty.Hard => Constants.SizeHard,
                Difficulty.Extreme => Constants.SizeExtreme,
                _ => 0
            };
        }
    }
}
