using System;

namespace QuantityMeasurementModelLayer.Interfaces
{
    public interface IMeasurable<TUnit> : IUnitConverter<TUnit> where TUnit : struct, Enum
    {
        double GetConversionFactor(TUnit unit);
    }
}