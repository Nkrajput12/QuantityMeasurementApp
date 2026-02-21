
namespace QuantityMeasurementApp.models
{

    /// <summary>
    /// Defines the supported units of measurement.
    /// Order is important: Index 0 = Inches, Index 1 = Feet.
    /// </summary>
    public enum LengthUnit
    {
        Inches,
        Feet
    }

    /// <summary>
    /// Provides helper methods for the LengthUnit enum to centralize conversion logic.
    /// </summary>
    public static class LengthUnitExtensions
    {
        // Lookup table: Maps enum index to its multiplier for the base unit (Inches)
        private static readonly double[] ToInchesFactor = {1.0, 12.0};


        // <summary>
        /// Retrieves the multiplier needed to convert the unit to Inches.
        /// </summary>
        public static double GetConversionFactor(this LengthUnit unit)
        {
            return ToInchesFactor[(int)unit];
        }

        // Returns the  symbol for the unit for UI display.
        public static string GetSymbol(this LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return "ft";
                
                case LengthUnit.Inches:
                    return "in";

                default:
                    return unit.ToString().ToLower();
            }
        }       
    }
}