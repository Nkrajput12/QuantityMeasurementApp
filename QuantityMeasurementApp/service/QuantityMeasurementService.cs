using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// UC10: Refactored Service to support generic measurement operations.
    /// This service acts as a clean wrapper for the Quantity model logic.
    /// </summary>
    public class QuantityMeasurementService
    {
        /// <summary>
        /// Compares two Quantity objects of the same unit category.
        /// </summary>
        public bool Compare<U>(Quantity<U> q1, Quantity<U> q2) where U : struct, Enum
        {
            if (q1 == null || q2 == null) return false;
            return q1.Equals(q2);
        }

        /// <summary>
        /// UC5: Direct conversion between units without an existing object.
        /// </summary>
        public double DemonstrateConversion<U>(double value, U sourceUnit, U targetUnit) where U : struct, Enum
        {
            var sourceQuantity = new Quantity<U>(value, sourceUnit);
            var convertedQuantity = sourceQuantity.ConvertTo(targetUnit);
            return convertedQuantity.Value;
        }

        /// <summary>
        /// UC5: Converts an existing Quantity object to a target unit.
        /// </summary>
        public Quantity<U> DemonstrateConversion<U>(Quantity<U> source, U targetUnit) where U : struct, Enum
        {
            return source.ConvertTo(targetUnit);
        }

        /// <summary>
        /// UC6: Adds two quantities and returns the result in the unit of the first operand.
        /// </summary>
        public Quantity<U> DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2) where U : struct, Enum
        {
            return q1.Add(q2);
        }

        /// <summary>
        /// Addition with an explicit target unit selection.
        /// </summary>
        public Quantity<U> DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit) where U : struct, Enum
        {
            return q1.Add(q2, targetUnit);
        }
    }
}