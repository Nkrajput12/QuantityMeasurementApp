using System;
using QuantityMeasurementApp.models;

namespace QuantityMeasurementApp.service
{
    /// <summary>
    /// Provides utility methods for performing operations and Comparisons
    /// </summary>
    public class FeetUtil
    {
        /// <summary>
        /// Compare two object to determine if they represent 
        /// the same measurement
        /// </summary>
        /// <param name="firstMeasurement"> First Feet class object for comparison</param>
        /// <param name="secondMeasurement"> Second Feet class object for comparison</param>
        /// <returns>Return true if both are equal else return false</returns>
        public bool CompareFeet(Feet firstMeasurement , Feet secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }

    }
}