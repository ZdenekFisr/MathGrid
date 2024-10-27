using FluentAssertions;
using GameLogic.Services.MatrixSum;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    [TestClass]
    public class MatrixSumTests
    {
        private ServiceProvider _serviceProvider;

        [TestInitialize]
        public void Setup()
        {
            ServiceCollection serviceCollection = new();

            serviceCollection.AddSingleton<IMatrixSumService, MatrixSumService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceProvider.Dispose();
        }

        [TestMethod]
        public void SumMatrix()
        {
            byte[,] matrix = {
                { 9, 10, 9, 10, 1, 11, 2, 10, 4, 11, 9 },
                { 11, 0, 12, 0, 11, 0, 11, 0, 10, 0, 12 },
                { 3, 12, 3, 11, 7, 11, 7, 11, 8, 12, 8 },
                { 10, 0, 10, 0, 11, 0, 10, 0, 10, 0, 11 },
                { 3, 10, 6, 12, 1, 10, 7, 12, 9, 11, 7 },
                { 11, 0, 12, 0, 11, 0, 11, 0, 10, 0, 10 },
                { 9, 12, 6, 10, 2, 12, 1, 10, 5, 12, 4 },
                { 12, 0, 10, 0, 10, 0, 12, 0, 10, 0, 12 },
                { 6, 10, 5, 10, 4, 11, 3, 12, 8, 11, 6 },
                { 10, 0, 12, 0, 10, 0, 10, 0, 11, 0, 10 },
                { 4, 12, 7, 10, 7, 10, 4, 11, 2, 11, 1 }
            };

            short[] rowsSumExpected = [12, -69, 65, 76, -15, 36];
            short[] colsSumExpected = [-41, 98, 2, 3, 32, 90];

            _serviceProvider.GetRequiredService<IMatrixSumService>().SumMatrix(matrix, out short[] rowsSumActual, out short[] colsSumActual);

            rowsSumActual.Should().BeEquivalentTo(rowsSumExpected);
            colsSumActual.Should().BeEquivalentTo(colsSumExpected);
        }
    }
}
