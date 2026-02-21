
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
}