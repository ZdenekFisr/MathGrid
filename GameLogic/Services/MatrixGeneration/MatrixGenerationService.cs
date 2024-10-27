using GameLogic.Enums;
using GameLogic.Extensions;
using GameLogic.Services.RandomNumber;

namespace GameLogic.Services.MatrixGeneration
{
    /// <inheritdoc cref="IMatrixGenerationService"/>
    public class MatrixGenerationService(
        IRandomNumberService randomNumberService)
        : IMatrixGenerationService
    {
        private readonly IRandomNumberService _randomNumberService = randomNumberService;

        /// <inheritdoc cref="IMatrixGenerationService.GenerateMatrix(Difficulty)"/>
        public byte[,] GenerateMatrix(Difficulty difficulty)
        {
            byte count = difficulty.ToGridSizeWithoutSums();
            byte[,] matrix = new byte[count, count];
            for (byte i = 0; i < count; i++)
            {
                for (byte j = 0; j < count; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                        matrix[i, j] = GenerateNumber();
                    else if (i % 2 != 0 && j % 2 != 0)
                        matrix[i, j] = 0;
                    else
                    {
                        if (i == 1 && j == 0 || i == 0 && j == 1)
                            matrix[i, j] = GenerateMathSign();
                        else
                        {
                            if (i == 0 || i == 1)
                            {
                                if (matrix[i, j - 2] == Constants.MultiplicationRepresentation)
                                    matrix[i, j] = GeneratePlusOrMinusSign();
                                else
                                    matrix[i, j] = GenerateMathSign();
                            }
                            else if (j == 0 || j == 1)
                            {
                                if (matrix[i - 2, j] == Constants.MultiplicationRepresentation)
                                    matrix[i, j] = GeneratePlusOrMinusSign();
                                else
                                    matrix[i, j] = GenerateMathSign();
                            }
                            else if (matrix[i, j - 2] == Constants.MultiplicationRepresentation || matrix[i - 2, j] == Constants.MultiplicationRepresentation)
                                matrix[i, j] = GeneratePlusOrMinusSign();
                            else
                                matrix[i, j] = GenerateMathSign();
                        }
                    }
                }
            }
            return matrix;
        }

        private byte GenerateNumber()
            => _randomNumberService.GenerateRandomNumber(Constants.MinNumber, Constants.MaxNumber);

        private byte GenerateMathSign()
            => _randomNumberService.GenerateRandomNumber(Constants.PlusRepresentation, Constants.MultiplicationRepresentation);

        private byte GeneratePlusOrMinusSign()
            => _randomNumberService.GenerateRandomNumber(Constants.PlusRepresentation, Constants.MinusRepresentation);
    }
}
