namespace GameLogic.Services.MatrixVisibility
{
    /// <summary>
    /// Randomly chooses visible numbers in a matrix.
    /// </summary>
    public interface IMatrixVisibilityService
    {
        /// <summary>
        /// Randomly chooses visible numbers in a matrix.
        /// </summary>
        /// <param name="gridSize">Number of rows and columns in a matrix.</param>
        /// <param name="ratioOfVisibleNumbers">Ration of visible numbers.</param>
        /// <returns>Matrix of boolean values. "True" represents a visible number.</returns>
        bool[,] ChooseVisibleNumbers(byte gridSize, double ratioOfVisibleNumbers);
    }
}