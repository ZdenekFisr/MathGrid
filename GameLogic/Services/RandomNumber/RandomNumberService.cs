namespace GameLogic.Services.RandomNumber
{
    /// <inheritdoc cref="IRandomNumberService"/>
    public class RandomNumberService : IRandomNumberService
    {
        private readonly Random random = new();

        /// <inheritdoc cref="IRandomNumberService.GenerateRandomNumber(byte, byte)"/>
        public byte GenerateRandomNumber(byte minValue, byte maxValue)
            => (byte)random.Next(minValue, maxValue + 1);
    }
}
