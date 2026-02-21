using System;

namespace QuantityMeasurementApp.models
{
    /// <summary>
    /// Represent a measurement length in Inches
    /// This class provides methods for value comparison and equality checks between objects.
    /// </summary>
    public class Inches
    {
        /// <summary>
        /// Holds the numeric value of the measurement in Inches.
        /// </summary>
        public readonly double value;

        /// <summary>
        /// Initializes a new instance of the class with the specified measurement value.
        /// </summary>
        /// <param name="value">value holds the value in double represent Inches</param>
        public Inches(double value)
        {
            this.value = value;
        }

        /// <summary>
        ///  Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <returns> Return True: if obj is Inches type and equal to the class obj
        /// else Return false if obj is null not similar type to the class...</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Inches other = (Inches)obj;

            return value.Equals(other.value);
            
        }

        /// <summary>
        /// Returns a hash code for the current instance.
        /// </summary>
        /// <returns>A 32-bit integer hash code.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}