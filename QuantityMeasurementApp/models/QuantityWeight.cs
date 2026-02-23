
using System.ComponentModel.Design;
using System.Runtime.Serialization.Formatters;

namespace QuantityMeasurementApp.models
{
    /// <summary>
    /// Represents a generic measurement consisting of a value and a specific unit.
    /// </summary>
    public class QuantityWeight
    {
        public readonly double value;
        public readonly WeightUnit unit;

        
        public QuantityWeight(double value , WeightUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        //UMethod for Direct numeric conversion 
        //This method return a numeric value
        public static double ConvertValue(double value,WeightUnit source,WeightUnit target)
        {
            double baseValue = value *source.GetConversionFactor();
            return baseValue / target.GetConversionFactor();
        }


        //this method return a new QuantityWeight object
        public  QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            double baseValue = this.unit.ConvertToBase(this.value);
            double convertValue = targetUnit.ConvertFromBase(baseValue);
            return new QuantityWeight(convertValue,targetUnit);
        }

        /// <summary>
        ///  Adds another quantity to the current one.
        /// Result is returned in the unit of the current instance (first operand0).
        /// </summary>
        public QuantityWeight Add(QuantityWeight other)
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
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            return PerformAddition(this,other,targetUnit);
        }

        /// <summary>
        /// Private utility method to centralize addition logic (DRY Principle).
        /// </summary>
        public  QuantityWeight PerformAddition(QuantityWeight q1, QuantityWeight q2, WeightUnit target)
        {
            //sum the base values
            double sumInBase = this.unit.ConvertToBase(q1.value) + q2.unit.ConvertToBase(q2.value);

            //convert sum back to the unit of the first operand
            double finalValue = target.ConvertFromBase(sumInBase);

            return new QuantityWeight(finalValue, target);
        }

        /// <summary>
        /// Determines if two Quantity objects are physically equal.
        /// Uses a base-unit conversion strategy to allow cross-unit comparison
        /// </summary>
        public override bool Equals(object? obj)
        {
            // 1. Check for reference equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            // 2. Check for null and type safety
            if(obj is not QuantityWeight other)
            {
                return false;
            }


            
            return Math.Abs(this.unit.ConvertToBase(this.value) - other.unit.ConvertToBase(other.value)) < 0.001;
        }

        public override int GetHashCode()
        {
            return this.unit.ConvertToBase(this.value).GetHashCode();
        }

        public override string ToString()
        {
            return $"{value} {unit.GetSymbol()}";
        }
    }
}
