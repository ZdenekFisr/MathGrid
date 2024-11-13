using FluentAssertions;
using GameLogic;
using GameLogic.Enums;
using GameLogic.Services.MatrixGeneration;
using GameLogic.Services.RandomNumber;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    [TestClass]
    public class MatrixGenerationTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServiceProvider _serviceProvider;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            ServiceCollection serviceCollection = new();

            serviceCollection.AddSingleton<IRandomNumberService, RandomNumberService>();
            serviceCollection.AddSingleton<IMatrixGenerationService, MatrixGenerationService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceProvider.Dispose();
        }

        private void PerformGenerateMatrixTest(Difficulty difficulty)
        {
            byte[,] matrix = _serviceProvider.GetRequiredService<IMatrixGenerationService>().GenerateMatrix(difficulty);

            for (byte i = 0; i < matrix.GetLength(0); i++)
            {
                for (byte j = 0; j < matrix.GetLength(1); j++)
                {
                    // number cells
                    if (i % 2 == 0 && j % 2 == 0)
                        matrix[i, j].Should().BeInRange(Constants.MinNumber, Constants.MaxNumber);

                    // empty cells
                    else if (i % 2 == 1 && j % 2 == 1)
                        matrix[i, j].Should().Be(Constants.EmptyCell);

                    // cells with math signs
                    else
                        matrix[i, j].Should().BeInRange(Constants.PlusRepresentation, Constants.MultiplicationRepresentation);

                    // assert that multiplication signs don't have a multiplication sign as a neighbour two cells away in each direction
                    if (matrix[i, j] == Constants.MultiplicationRepresentation)
                    {
                        // up
                        if (i > 1)
                            matrix[i - 2, j].Should().NotBe(Constants.MultiplicationRepresentation);

                        // down
                        if (i < matrix.GetLength(0) - 2)
                            matrix[i + 2, j].Should().NotBe(Constants.MultiplicationRepresentation);

                        // left
                        if (j > 1)
                            matrix[i, j - 2].Should().NotBe(Constants.MultiplicationRepresentation);

                        // right
                        if (j < matrix.GetLength(1) - 2)
                            matrix[i, j + 2].Should().NotBe(Constants.MultiplicationRepresentation);
                    }
                }
            }
        }

        [TestMethod]
        public void GenerateMatrix_VeryEasy()
            => PerformGenerateMatrixTest(Difficulty.VeryEasy);

        [TestMethod]
        public void GenerateMatrix_Easy()
            => PerformGenerateMatrixTest(Difficulty.Easy);

        [TestMethod]
        public void GenerateMatrix_Medium()
            => PerformGenerateMatrixTest(Difficulty.Medium);

        [TestMethod]
        public void GenerateMatrix_Hard()
            => PerformGenerateMatrixTest(Difficulty.Hard);

        [TestMethod]
        public void GenerateMatrix_Extreme()
            => PerformGenerateMatrixTest(Difficulty.Extreme);
    }
}