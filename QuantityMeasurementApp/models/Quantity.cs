
using System.ComponentModel.Design;
using System.Runtime.Serialization.Formatters;

namespace QuantityMeasurementApp.models
{
    /// <summary>
    /// Represents a generic measurement consisting of a value and a specific unit.
    /// This class replaces the separate Feet and Inches classes to follow the DRY principle.
    /// </summary>
    public class Quantity
    {
        public readonly double value;
        public readonly LengthUnit unit;

        
        public Quantity(double value , LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        //UC5: Method for Direct numeric conversion 
        //This method return a numeric value
        public static double ConvertValue(double value,LengthUnit source,LengthUnit target)
        {
            double baseValue = value *source.GetConversionFactor();
            return baseValue / target.GetConversionFactor();
        }


        //UC5: this method return a new Quantity object
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
        /// UC6: Adds another quantity to the current one.
        /// Result is returned in the unit of the current instance (first operand0).
        /// </summary>
        public Quantity Add(Quantity other)
        {
            if(other == null)
            {
                throw new ArgumentNullException("the other Quantity can't be null");
            }                        
            return Add(other,this.unit);
        }

        /// <summary>
        /// Add two quantities with an specific target unit
        /// </summary>
        public Quantity Add(Quantity other, LengthUnit targetUnit)
        {
            return PerformAddition(this,other,targetUnit);
        }

        /// <summary>
        /// Private utility method to centralize addition logic (DRY Principle).
        /// </summary>
        public  Quantity PerformAddition(Quantity q1, Quantity q2, LengthUnit target)
        {
            //Convert both to base unit
            double firstInBase = q1.value* q1.unit.GetConversionFactor();
            double otherInBase = q2.value* q2.unit.GetConversionFactor();

            //sum the base values
            double sumInBase = firstInBase + otherInBase;

            //convert sum back to the unit of the first operand
            double finalValue = sumInBase/target.GetConversionFactor();

            return new Quantity(finalValue, target);
        }

        /// <summary>
        /// UC1: Determines if two Quantity objects are physically equal.
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
