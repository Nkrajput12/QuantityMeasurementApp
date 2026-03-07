namespace QuantityMeasurementApp.models
{
    public enum WeightUnit
    {
        Grams, //Index 0
        Kilograms, //Index 1
        Pound //Index 2
    }

    /// <summary>
    /// Provides helper methods for the LengthUnit enum to centralize conversion logic.
    /// </summary>
    public static class WeightUnitExtension
    {
        private static readonly double[] ToKgFactor =
        {
            0.001, //Grams
            1.0,    //Kilograms
            0.453592 //Pound
        };


        //Method to get the conversion factor
        public static double GetConversionFactor(this WeightUnit unit)
        {
            return ToKgFactor[(int)unit];
        }

        //Method to Convert the value to the base (Kg)
        public static double ConvertToBase(this WeightUnit unit,double value)
        {
            return value*unit.GetConversionFactor();
        }

        //Method to Convert the value to the specific unit and return the double value
        public static double ConvertFromBase(this WeightUnit unit, double baseValue)
        {
            return baseValue/unit.GetConversionFactor();
        }

        // Returns the  symbol for the unit for UI display
        public static string GetSymbol(this WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Kilograms:
                    return "Kg";
                
                case WeightUnit.Grams:
                    return "g";

                case WeightUnit.Pound:
                    return "lb";
                default:
                    return unit.ToString().ToLower();
            }

        }
    }
}