using GameLogic.Enums;

namespace GameLogic.Services.MatrixGeneration
{
    /// <summary>
    /// Generates matrix of numbers and math sign representations.
    /// </summary>
    public interface IMatrixGenerationService
    {
        /// <summary>
        /// Generates matrix of numbers and math sign representations.
        /// </summary>
        /// <param name="difficulty">Difficulty enum.</param>
        /// <returns>Generated matrix.</returns>
        byte[,] GenerateMatrix(Difficulty difficulty);
    }
}