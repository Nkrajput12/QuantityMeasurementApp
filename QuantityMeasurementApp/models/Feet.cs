using System;

namespace  QuantityMeasurementApp.models
{
    /// <summary>
    /// Represents a measurement of length in feet.
    /// This class provides methods for value comparison and equality checks between objects.
    /// </summary>
    public class Feet
    {
        /// <summary>
        /// Holds the numeric value of the measurement in feet.
        /// </summary>
        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the class with the specified measurement value.
        /// </summary>
        public Feet(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// The object to compare with the current instance
        /// <returns>
        /// true if the specified object is a instance 
        /// and has the same numeric value; otherwise, false.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Feet other = (Feet)obj;
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