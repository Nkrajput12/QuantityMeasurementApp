
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


            
            return Math.Abs(this.convertToBase() - other.convertToBase()) < 0.0001;
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
