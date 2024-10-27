namespace GameLogic.Services.MatrixSum
{
    /// <inheritdoc cref="IMatrixSumService"/>
    public class MatrixSumService : IMatrixSumService
    {
        /// <inheritdoc cref="IMatrixSumService.SumMatrix(byte[,], out short[], out short[])"/>
        public void SumMatrix(byte[,] matrix, out short[] rowsSum, out short[] columnsSum)
        {
            rowsSum = new short[matrix.GetLength(1) / 2 + 1];
            columnsSum = new short[matrix.GetLength(0) / 2 + 1];

            int index = 0;
            for (int i = 0; i < matrix.GetLength(0); i += 2)
            {
                if (matrix[i, 1] == Constants.MultiplicationRepresentation)
                    rowsSum[index] += (short)(matrix[i, 0] * matrix[i, 2]);
                else
                    rowsSum[index] += matrix[i, 0];

                for (int j = 2; j < matrix.GetLength(1) - 2; j += 2)
                {
                    if (matrix[i, j - 1] == Constants.MultiplicationRepresentation)
                        continue;
                    else if (matrix[i, j + 1] == Constants.MultiplicationRepresentation)
                    {
                        if (matrix[i, j - 1] == Constants.PlusRepresentation)
                            rowsSum[index] += (short)(matrix[i, j] * matrix[i, j + 2]);
                        else if (matrix[i, j - 1] == Constants.MinusRepresentation)
                            rowsSum[index] -= (short)(matrix[i, j] * matrix[i, j + 2]);
                    }
                    else
                    {
                        if (matrix[i, j - 1] == Constants.PlusRepresentation)
                            rowsSum[index] += matrix[i, j];
                        else if (matrix[i, j - 1] == Constants.MinusRepresentation)
                            rowsSum[index] -= matrix[i, j];
                    }
                }

                if (matrix[i, matrix.GetLength(1) - 2] == Constants.PlusRepresentation)
                    rowsSum[index] += matrix[i, matrix.GetLength(1) - 1];
                else if (matrix[i, matrix.GetLength(1) - 2] == Constants.MinusRepresentation)
                    rowsSum[index] -= matrix[i, matrix.GetLength(1) - 1];

                index++;
            }

            index = 0;
            for (int j = 0; j < matrix.GetLength(1); j += 2)
            {
                if (matrix[1, j] == Constants.MultiplicationRepresentation)
                    columnsSum[index] += (short)(matrix[0, j] * matrix[2, j]);
                else
                    columnsSum[index] += matrix[0, j];

                for (int i = 2; i < matrix.GetLength(0) - 2; i += 2)
                {
                    if (matrix[i - 1, j] == Constants.MultiplicationRepresentation)
                        continue;
                    else if (matrix[i + 1, j] == Constants.MultiplicationRepresentation)
                    {
                        if (matrix[i - 1, j] == Constants.PlusRepresentation)
                            columnsSum[index] += (short)(matrix[i, j] * matrix[i + 2, j]);
                        else if (matrix[i - 1, j] == Constants.MinusRepresentation)
                            columnsSum[index] -= (short)(matrix[i, j] * matrix[i + 2, j]);
                    }
                    else
                    {
                        if (matrix[i - 1, j] == Constants.PlusRepresentation)
                            columnsSum[index] += matrix[i, j];
                        else if (matrix[i - 1, j] == Constants.MinusRepresentation)
                            columnsSum[index] -= matrix[i, j];
                    }
                }

                if (matrix[matrix.GetLength(0) - 2, j] == Constants.PlusRepresentation)
                    columnsSum[index] += matrix[matrix.GetLength(0) - 1, j];
                else if (matrix[matrix.GetLength(0) - 2, j] == Constants.MinusRepresentation)
                    columnsSum[index] -= matrix[matrix.GetLength(0) - 1, j];

                index++;
            }
        }
    }
}
