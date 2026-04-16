using MeasurementService.Business.Interfaces;
using MeasurementService.Business.Converters;
using Common.Models.DTOs;
using Common.Models.Enums;

namespace MeasurementService.Business.Services;

public class MeasurementService : IMeasurementService
{
    public object Convert<TUnit>(QuantityDto value, string targetUnit, int? userId) where TUnit : struct,Enum
    {
        var targetUnitEnum = Enum.Parse<TUnit>(targetUnit);
        
        if (typeof(TUnit) == typeof(LengthUnit))
        {
            var sourceUnit = Enum.Parse<LengthUnit>(value.Unit);
            var target = (LengthUnit)(object)targetUnitEnum;
            var meters = LengthConverter.ConvertToMeters(value.Value, sourceUnit);
            var result = LengthConverter.ConvertFromMeters(meters, target);
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(WeightUnit))
        {
            var sourceUnit = Enum.Parse<WeightUnit>(value.Unit);
            var target = (WeightUnit)(object)targetUnitEnum;
            var kg = WeightConverter.ConvertToKilograms(value.Value, sourceUnit);
            var result = WeightConverter.ConvertFromKilograms(kg, target);
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(VolumeUnit))
        {
            var sourceUnit = Enum.Parse<VolumeUnit>(value.Unit);
            var target = (VolumeUnit)(object)targetUnitEnum;
            var liters = VolumeConverter.ConvertToLiters(value.Value, sourceUnit);
            var result = VolumeConverter.ConvertFromLiters(liters, target);
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(TemperatureUnit))
        {
            var sourceUnit = Enum.Parse<TemperatureUnit>(value.Unit);
            var target = (TemperatureUnit)(object)targetUnitEnum;
            var result = TemperatureConverter.Convert(value.Value, sourceUnit, target);
            return new QuantityDto { Value = result, Unit = targetUnit };
        }

        throw new ArgumentException("Invalid unit type");
    }

    public object Add<TUnit>(QuantityDto value1, QuantityDto value2, string targetUnit, int? userId) where TUnit : struct,Enum
    {
        var targetUnitEnum = Enum.Parse<TUnit>(targetUnit);

        if (typeof(TUnit) == typeof(LengthUnit))
        {
            var unit1 = Enum.Parse<LengthUnit>(value1.Unit);
            var unit2 = Enum.Parse<LengthUnit>(value2.Unit);
            var target = (LengthUnit)(object)targetUnitEnum;
            
            var meters1 = LengthConverter.ConvertToMeters(value1.Value, unit1);
            var meters2 = LengthConverter.ConvertToMeters(value2.Value, unit2);
            var result = LengthConverter.ConvertFromMeters(meters1 + meters2, target);
            
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(WeightUnit))
        {
            var unit1 = Enum.Parse<WeightUnit>(value1.Unit);
            var unit2 = Enum.Parse<WeightUnit>(value2.Unit);
            var target = (WeightUnit)(object)targetUnitEnum;
            
            var kg1 = WeightConverter.ConvertToKilograms(value1.Value, unit1);
            var kg2 = WeightConverter.ConvertToKilograms(value2.Value, unit2);
            var result = WeightConverter.ConvertFromKilograms(kg1 + kg2, target);
            
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(VolumeUnit))
        {
            var unit1 = Enum.Parse<VolumeUnit>(value1.Unit);
            var unit2 = Enum.Parse<VolumeUnit>(value2.Unit);
            var target = (VolumeUnit)(object)targetUnitEnum;
            
            var liters1 = VolumeConverter.ConvertToLiters(value1.Value, unit1);
            var liters2 = VolumeConverter.ConvertToLiters(value2.Value, unit2);
            var result = VolumeConverter.ConvertFromLiters(liters1 + liters2, target);
            
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(TemperatureUnit))
        {
            throw new InvalidOperationException("Temperature addition is not supported");
        }

        throw new ArgumentException("Invalid unit type");
    }

    public object Subtract<TUnit>(QuantityDto value1, QuantityDto value2, string targetUnit, int? userId) where TUnit : struct,Enum
    {
        var targetUnitEnum = Enum.Parse<TUnit>(targetUnit);

        if (typeof(TUnit) == typeof(LengthUnit))
        {
            var unit1 = Enum.Parse<LengthUnit>(value1.Unit);
            var unit2 = Enum.Parse<LengthUnit>(value2.Unit);
            var target = (LengthUnit)(object)targetUnitEnum;
            
            var meters1 = LengthConverter.ConvertToMeters(value1.Value, unit1);
            var meters2 = LengthConverter.ConvertToMeters(value2.Value, unit2);
            var result = LengthConverter.ConvertFromMeters(meters1 - meters2, target);
            
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(WeightUnit))
        {
            var unit1 = Enum.Parse<WeightUnit>(value1.Unit);
            var unit2 = Enum.Parse<WeightUnit>(value2.Unit);
            var target = (WeightUnit)(object)targetUnitEnum;
            
            var kg1 = WeightConverter.ConvertToKilograms(value1.Value, unit1);
            var kg2 = WeightConverter.ConvertToKilograms(value2.Value, unit2);
            var result = WeightConverter.ConvertFromKilograms(kg1 - kg2, target);
            
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(VolumeUnit))
        {
            var unit1 = Enum.Parse<VolumeUnit>(value1.Unit);
            var unit2 = Enum.Parse<VolumeUnit>(value2.Unit);
            var target = (VolumeUnit)(object)targetUnitEnum;
            
            var liters1 = VolumeConverter.ConvertToLiters(value1.Value, unit1);
            var liters2 = VolumeConverter.ConvertToLiters(value2.Value, unit2);
            var result = VolumeConverter.ConvertFromLiters(liters1 - liters2, target);
            
            return new QuantityDto { Value = result, Unit = targetUnit };
        }
        else if (typeof(TUnit) == typeof(TemperatureUnit))
        {
            var unit1 = Enum.Parse<TemperatureUnit>(value1.Unit);
            var unit2 = Enum.Parse<TemperatureUnit>(value2.Unit);
            
            var celsius1 = TemperatureConverter.Convert(value1.Value, unit1, TemperatureUnit.Celsius);
            var celsius2 = TemperatureConverter.Convert(value2.Value, unit2, TemperatureUnit.Celsius);
            
            return new QuantityDto { Value = celsius1 - celsius2, Unit = "Celsius" };
        }

        throw new ArgumentException("Invalid unit type");
    }

    public double Divide<TUnit>(QuantityDto value1, QuantityDto value2, int? userId) where TUnit : struct,Enum
    {
        if (typeof(TUnit) == typeof(LengthUnit))
        {
            var unit1 = Enum.Parse<LengthUnit>(value1.Unit);
            var unit2 = Enum.Parse<LengthUnit>(value2.Unit);
            
            var meters1 = LengthConverter.ConvertToMeters(value1.Value, unit1);
            var meters2 = LengthConverter.ConvertToMeters(value2.Value, unit2);
            
            if (meters2 == 0) throw new DivideByZeroException();
            return meters1 / meters2;
        }
        else if (typeof(TUnit) == typeof(WeightUnit))
        {
            var unit1 = Enum.Parse<WeightUnit>(value1.Unit);
            var unit2 = Enum.Parse<WeightUnit>(value2.Unit);
            
            var kg1 = WeightConverter.ConvertToKilograms(value1.Value, unit1);
            var kg2 = WeightConverter.ConvertToKilograms(value2.Value, unit2);
            
            if (kg2 == 0) throw new DivideByZeroException();
            return kg1 / kg2;
        }
        else if (typeof(TUnit) == typeof(VolumeUnit))
        {
            var unit1 = Enum.Parse<VolumeUnit>(value1.Unit);
            var unit2 = Enum.Parse<VolumeUnit>(value2.Unit);
            
            var liters1 = VolumeConverter.ConvertToLiters(value1.Value, unit1);
            var liters2 = VolumeConverter.ConvertToLiters(value2.Value, unit2);
            
            if (liters2 == 0) throw new DivideByZeroException();
            return liters1 / liters2;
        }
        else if (typeof(TUnit) == typeof(TemperatureUnit))
        {
            throw new InvalidOperationException("Temperature division is not supported");
        }

        throw new ArgumentException("Invalid unit type");
    }

    public bool Compare<TUnit>(QuantityDto value1, QuantityDto value2, int? userId) where TUnit : struct,Enum
    {
        const double tolerance = 0.0001;

        if (typeof(TUnit) == typeof(LengthUnit))
        {
            var unit1 = Enum.Parse<LengthUnit>(value1.Unit);
            var unit2 = Enum.Parse<LengthUnit>(value2.Unit);
            
            var meters1 = LengthConverter.ConvertToMeters(value1.Value, unit1);
            var meters2 = LengthConverter.ConvertToMeters(value2.Value, unit2);
            
            return Math.Abs(meters1 - meters2) < tolerance;
        }
        else if (typeof(TUnit) == typeof(WeightUnit))
        {
            var unit1 = Enum.Parse<WeightUnit>(value1.Unit);
            var unit2 = Enum.Parse<WeightUnit>(value2.Unit);
            
            var kg1 = WeightConverter.ConvertToKilograms(value1.Value, unit1);
            var kg2 = WeightConverter.ConvertToKilograms(value2.Value, unit2);
            
            return Math.Abs(kg1 - kg2) < tolerance;
        }
        else if (typeof(TUnit) == typeof(VolumeUnit))
        {
            var unit1 = Enum.Parse<VolumeUnit>(value1.Unit);
            var unit2 = Enum.Parse<VolumeUnit>(value2.Unit);
            
            var liters1 = VolumeConverter.ConvertToLiters(value1.Value, unit1);
            var liters2 = VolumeConverter.ConvertToLiters(value2.Value, unit2);
            
            return Math.Abs(liters1 - liters2) < tolerance;
        }
        else if (typeof(TUnit) == typeof(TemperatureUnit))
        {
            var unit1 = Enum.Parse<TemperatureUnit>(value1.Unit);
            var unit2 = Enum.Parse<TemperatureUnit>(value2.Unit);
            
            var celsius1 = TemperatureConverter.Convert(value1.Value, unit1, TemperatureUnit.Celsius);
            var celsius2 = TemperatureConverter.Convert(value2.Value, unit2, TemperatureUnit.Celsius);
            
            return Math.Abs(celsius1 - celsius2) < tolerance;
        }

        throw new ArgumentException("Invalid unit type");
    }
}
