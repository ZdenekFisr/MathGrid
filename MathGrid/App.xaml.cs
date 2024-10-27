using GameLogic.Services.MatrixGeneration;
using GameLogic.Services.MatrixSum;
using GameLogic.Services.MatrixVisibility;
using GameLogic.Services.RandomNumber;
using MathGrid.Services.Game;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MathGrid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceCollection serviceCollection = new();
            serviceCollection.ConfigureServices();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this ServiceCollection services)
        {
            services.AddSingleton<IRandomNumberService, RandomNumberService>();

            services.AddSingleton<IMatrixGenerationService, MatrixGenerationService>();
            services.AddSingleton<IMatrixSumService, MatrixSumService>();
            services.AddSingleton<IMatrixVisibilityService, MatrixVisibilityService>();

            services.AddSingleton<GameService>();

            services.AddSingleton<MainWindow>();
        }
    }
}
