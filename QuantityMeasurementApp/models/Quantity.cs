
using System.Runtime.Serialization.Formatters;

namespace QuantityMeasurementApp.models
{
    /// <summary>
    /// Represents a generic measurement consisting of a value and a specific unit.
    /// This class replaces the separate Feet and Inches classes to follow the DRY principle.
    /// </summary>
    public class Quantity
    {
        private readonly double value;
        private readonly LengthUnit unit;

        
        public Quantity(double value , LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        //Method for Direct numeric conversion 
        //This method return a numeric value
        public static double ConvertValue(double value,LengthUnit source,LengthUnit target)
        {
            if(source == null || target == null)
            {
                throw new ArgumentException("unit cannot be null");
            }

            double baseValue = value *source.GetConversionFactor();
            return baseValue / target.GetConversionFactor();
        }


        //this method return a new Quantity object
        public Quantity ConvertTo(LengthUnit targetUnit)
        {
            double convertValue = ConvertValue(this.value,this.unit,targetUnit);
            return new Quantity(convertValue,targetUnit);
        }

        //Convert to value to the base Type (in)
        private double convertToBase()
        {
            return value*unit.GetConversionFactor();
        }

        /// <summary>
        /// Determines if two Quantity objects are physically equal.
        /// Uses a base-unit conversion strategy to allow cross-unit comparison (e.g., 1ft == 12in).
        /// </summary>
        public override bool Equals(object? obj)
        {
            // 1. Check for reference equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            // 2. Check for null and type safety
            if(obj is not Quantity other)
            {
                return false;
            }


            
            return Math.Abs(this.convertToBase() - other.convertToBase()) < 0.001;
        }

        public override int GetHashCode()
        {
            return convertToBase().GetHashCode();
        }

        public override string ToString()
        {
            return $"{value} {unit.GetSymbol()}";
        }
    }
}
