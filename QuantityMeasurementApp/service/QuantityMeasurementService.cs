
using QuantityMeasurementApp.models;

public class QuantityMeasurementService
{
    /// <summary>
        /// Compare two object to determine if they represent 
        /// the same measurement
        /// </summary>
        /// <param name="firstMeasurement"> First Feet class object for comparison</param>
        /// <param name="secondMeasurement"> Second Feet class object for comparison</param>
        /// <returns>Return true if both are equal else return false</returns>
        public bool CompareFeet(Feet firstMeasurement ,Feet secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }

        /// <summary>
        /// Compare two object to determine if they represent 
        /// the same measurement
        /// </summary>
        /// <param name="firstMeasurement"> First Inches class object for comparison</param>
        /// <param name="secondMeasurement"> Second Inches class object for comparison</param>
        /// <returns>Return true if both are equal else return false</returns>

        public bool CompareInches(Inches firstMeasurement , Inches secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }
}