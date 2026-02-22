
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using QuantityMeasurementApp.models;


// Provides comparison services for different measurements.
public class QuantityMeasurementService
{   
    /// <summary>
    /// Compares two Quantity objects. 
    /// The conversion math is handled internally by the Quantity model.
    /// </summary>
    public bool Compare(Quantity q1,Quantity q2)
    {
        if(q1 == null || q2 == null)
        {
            return false;
        }

        return q1.Equals(q2);
    }

    /// <summary>
    /// Takes the value input and Convert from Source to Value
    /// and Return numeric value by calling ConvertValue Method
    /// from Quantity class
    /// </summary>
    /// <param name="value"> The value we want to Convert </param>
    /// <param name="from">Convert from which Measurement </param>
    /// <param name="to"> Convert to Which Measurement</param>
    /// <returns> Return double value after Conversion </returns>
    public double DemonstrateLengthConversion(double value, LengthUnit from,LengthUnit to)
    {
        return Quantity.ConvertValue(value,from,to);
    }


    /// <summary>
    /// This Method use to Convert the Quantity obj
    /// To desire Length Unit 
    /// </summary>
    /// <param name="Source"> The obj of Quantity</param>
    /// <param name="target">The Length unit we want to convert</param>
    /// <returns> Return the Convert Quantity obj</returns>
    public Quantity DemonstrateLengthConversion(Quantity Source,LengthUnit target)
    {
        return Source.ConvertTo(target);
    }


}