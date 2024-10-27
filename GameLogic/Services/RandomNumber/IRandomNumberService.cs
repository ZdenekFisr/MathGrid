namespace GameLogic.Services.RandomNumber
{
    /// <summary>
    /// Contains methods to generate a random number.
    /// </summary>
    public interface IRandomNumberService
    {
        /// <summary>
        /// Generates a byte-sized random number.
        /// </summary>
        /// <param name="minValue">Inclusive lower bound.</param>
        /// <param name="maxValue">Inclusive upper bound.</param>
        /// <returns>Generated number.</returns>
        byte GenerateRandomNumber(byte minValue, byte maxValue);
    }
}
