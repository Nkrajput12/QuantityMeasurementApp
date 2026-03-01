using System;
using QuantityMeasurementApp.models;
using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// UC10: Represents a generic quantity that handles different unit categories (Length, Weight, etc.).
    /// This class uses generic type constraints to ensure only Enums can be used as units.
    /// </summary>
    public sealed class Quantity<TUnit>
        where TUnit : struct, Enum
    {
        // Used for floating-point comparison to handle precision issues
        private const double Epsilon = 1e-6;

        /// <summary> Gets the numeric value of the quantity. </summary>
        public double Value { get; }

        /// <summary> Gets the specific unit of the quantity. </summary>
        public TUnit Unit { get; }

        /// <summary>
        /// Initializes a new instance of the Quantity class.
        /// </summary>
        /// <param name="value">The magnitude of the measurement.</param>
        /// <param name="unit">The measurement unit.</param>
        /// <exception cref="ArgumentException">Thrown if value is NaN or Infinity.</exception>
        public Quantity(double value, TUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }


        /// <summary>
        /// Converts the current quantity value to its base unit representation.
        /// (e.g., Feet to Inches or Kilograms to Grams).
        /// </summary>
        /// <returns>The value converted to base unit as a double.</returns>
        private double ToBaseUnit()
        {
            
            if (Unit is LengthUnit l) return l.ConvertToBase(Value);
            if (Unit is WeightUnit w) return w.ConvertToBase(Value);
            
            throw new InvalidOperationException("Unsupported Unit Category");
        }

        /// <summary>
        /// Converts the current quantity into a new target unit within the same category.
        /// </summary>
        /// <param name="target">The target unit to convert to.</param>
        /// <returns>A new Quantity object in the target unit.</returns>
        public Quantity<TUnit> ConvertTo(TUnit target)
        {
            double baseValue = ToBaseUnit();
            double converted = 0;

            if (target is LengthUnit l) converted = LengthUnitExtensions.ConvertFromBase(l, baseValue);
            else if (target is WeightUnit w) converted = WeightUnitExtension.ConvertFromBase(w, baseValue);

            return new Quantity<TUnit>(converted, target);
        }

        /// <summary>
        /// Adds another quantity to this one, returning the result in the current unit.
        /// </summary>
        public Quantity<TUnit> Add(Quantity<TUnit> other)
        {
            return Add(other, this.Unit);
        }

        public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit target)
        {
            double baseSum = this.ToBaseUnit() + other.ToBaseUnit();
            double converted = 0;

            // Bridge to the correct static extension method
            if (target is LengthUnit l) converted = LengthUnitExtensions.ConvertFromBase(l, baseSum);
            else if (target is WeightUnit w) converted = WeightUnitExtension.ConvertFromBase(w, baseSum);

            return new Quantity<TUnit>(converted, target);
        }

        /// <summary>
        /// Determines if two quantities are equal by comparing their base unit values.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<TUnit> other)
                return false;

            return Math.Abs(this.ToBaseUnit() - other.ToBaseUnit()) < Epsilon;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        public override int GetHashCode()
        {
            return ToBaseUnit().GetHashCode();
        }

        /// <summary>
        /// Returns a string representing the quantity and its unit symbol.
        /// </summary>
        public override string ToString()
        {
            string symbol = "";
            if (Unit is LengthUnit l) symbol = l.GetSymbol();
            else if (Unit is WeightUnit w) symbol = w.GetSymbol();

            return $"{Value} {symbol}";
        }
    }
}