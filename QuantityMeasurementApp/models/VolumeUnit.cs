namespace QuantityMeasurementApp.models
{
    /// <summary>
    /// Defines the supported units of measurement.
    /// Order is important: Index 0 = Inches, Index 1 = Feet.
    /// </summary>
    public enum VolumeUnit
    {
        Litre,        //Index 0
        MilliLiter,   //Index 1
        Gallon        //Index 2
    }
    
    public static class VolumeUnitExtension
    {
        private static readonly double[] ToLitreFactor = {
            1.0, //Litre
            0.001, //milliliter
            3.78541  //Gallon
        };
        
        // <summary>
        /// Retrieves the multiplier needed to convert the unit to Liter.
        /// </summary>
        public static double GetConversionFactor(this VolumeUnit unit)
        {
            return ToLitreFactor[(int)unit];
        }
        
        //Method to convert the value to the base(Liter)
        public static double ConvertToBase(this VolumeUnit unit, double value)
        {
            return value*unit.GetConversionFactor();
        }
        
        //Method to convert the value from the base 
        public static double ConvertFromBase(this VolumeUnit unit ,double baseValue)
        {
            return baseValue/unit.GetConversionFactor();
        }

        //Method to return the symbol to display
        public static string GetSymbol(this VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.Litre:
                    return "L";
                
                case VolumeUnit.MilliLiter:
                    return "ML";

                case VolumeUnit.Gallon:
                    return "gal";
                
                default:
                    return unit.ToString().ToLower();
            }
        }       
    }

}