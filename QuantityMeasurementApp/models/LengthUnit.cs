
namespace QuantityMeasurementApp.models
{

    /// <summary>
    /// Defines the supported units of measurement.
    /// Order is important: Index 0 = Inches, Index 1 = Feet.
    /// </summary>
    public enum LengthUnit
    {
        Inches,     //Index 0
        Feet,       //Index 1
        Yards,      //Index 2
        Centimeters //Index 3
    }

    /// <summary>
    /// Provides helper methods for the LengthUnit enum to centralize conversion logic.
    /// </summary>
    public static class LengthUnitExtensions
    {
        // Lookup table: Maps enum index to its multiplier for the base unit (Inches)
        private static readonly double[] ToInchesFactor = {
            1.0, //Inches
            12.0, //Feet (12inches = 1Feet)
            36,     // Yards (36 inches = 1Yard)
            0.393701   //Centimeter (0.393701 inches = 1Centimeter )
        };


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

                case LengthUnit.Yards:
                    return "yd";
                
                case LengthUnit.Centimeters:
                    return "cm";
                default:
                    return unit.ToString().ToLower();
            }
        }       
    }
}