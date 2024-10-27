namespace GameLogic.Services.MatrixSum
{
    /// <summary>
    /// Sums a matrix using special rules.
    /// </summary>
    public interface IMatrixSumService
    {
        /// <summary>
        /// Sums a matrix. Math sign representations are replaced with corresponding math operations and the emty cells are ignored. Multiplication has priority.
        /// </summary>
        /// <param name="matrix">Input matrix with numbers, math sign representations and empty cells.</param>
        /// <param name="rowsSum">Sums of rows.</param>
        /// <param name="columnsSum">Sums of columns.</param>
        void SumMatrix(byte[,] matrix, out short[] rowsSum, out short[] columnsSum);
    }
}
