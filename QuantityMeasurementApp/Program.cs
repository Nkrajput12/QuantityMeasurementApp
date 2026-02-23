using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// The main entry point for the Quantity Measurement App
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the execution starting point of the console application.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed at startup</param>
        public static void Main(String[] args)
        {
            QuantityMeasurementAppMenu menu = new QuantityMeasurementAppMenu();

            menu.Run();
        }
    }
}
