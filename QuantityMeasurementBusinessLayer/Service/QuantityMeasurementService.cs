using System;
using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepoLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly ICacheRepository _cacheRepo;
        private readonly LengthUnitConverter _lengthConv = new LengthUnitConverter();
        private readonly WeightUnitConverter _weightConv = new WeightUnitConverter();
        private readonly VolumeUnitConverter _volumeConv = new VolumeUnitConverter();
        private readonly TemperatureUnitConverter _tempConv = new TemperatureUnitConverter();

        // Constructor Injection for the Cache Repo!
        public QuantityMeasurementService(ICacheRepository cacheRepo)
        {
            _cacheRepo = cacheRepo;
        }

        private double GetBaseValue<T>(QuantityDTO dto) where T : struct, Enum
        {
            T unit = Enum.Parse<T>(dto.Unit, true);

            if (typeof(T) == typeof(LengthUnit)) return _lengthConv.ConvertToBase((LengthUnit)(object)unit, dto.Value);
            if (typeof(T) == typeof(WeightUnit)) return _weightConv.ConvertToBase((WeightUnit)(object)unit, dto.Value);
            if (typeof(T) == typeof(VolumeUnit)) return _volumeConv.ConvertToBase((VolumeUnit)(object)unit, dto.Value);
            if (typeof(T) == typeof(TemperatureUnit)) return _tempConv.ConvertToBase((TemperatureUnit)(object)unit, dto.Value);

            throw new Exception("Unsupported Unit Category");
        }

        private QuantityDTO CreateFromBase<T>(double baseValue, string targetUnitStr) where T : struct, Enum
        {
            T target = Enum.Parse<T>(targetUnitStr, true);
            double converted = 0;
            string symbol = "";

            if (typeof(T) == typeof(LengthUnit)) { converted = _lengthConv.ConvertFromBase((LengthUnit)(object)target, baseValue); symbol = _lengthConv.GetSymbol((LengthUnit)(object)target); }
            else if (typeof(T) == typeof(WeightUnit)) { converted = _weightConv.ConvertFromBase((WeightUnit)(object)target, baseValue); symbol = _weightConv.GetSymbol((WeightUnit)(object)target); }
            else if (typeof(T) == typeof(VolumeUnit)) { converted = _volumeConv.ConvertFromBase((VolumeUnit)(object)target, baseValue); symbol = _volumeConv.GetSymbol((VolumeUnit)(object)target); }
            else if (typeof(T) == typeof(TemperatureUnit)) { converted = _tempConv.ConvertFromBase((TemperatureUnit)(object)target, baseValue); symbol = _tempConv.GetSymbol((TemperatureUnit)(object)target); }

            return new QuantityDTO(Math.Round(converted, 2), symbol); // Storing symbol in the Unit field for display
        }

        public QuantityDTO Convert<T>(QuantityDTO source, string targetUnit) where T : struct, Enum
        {
            double baseValue = GetBaseValue<T>(source);
            var result = CreateFromBase<T>(baseValue, targetUnit);

            _cacheRepo.SaveToCache(new CacheRecordDto { OperationType = "Conversion", InputDetails = $"{source.Value} {source.Unit} to {targetUnit}", Result = $"{result.Value} {result.Unit}" });
            return result;
        }

        public QuantityDTO Add<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit) where T : struct, Enum
        {
            double base1 = GetBaseValue<T>(q1);
            double base2 = GetBaseValue<T>(q2);
            var result = CreateFromBase<T>(base1 + base2, targetUnit);

            _cacheRepo.SaveToCache(new CacheRecordDto { OperationType = "Addition", InputDetails = $"{q1.Value} {q1.Unit} + {q2.Value} {q2.Unit}", Result = $"{result.Value} {result.Unit}" });
            return result;
        }

        public QuantityDTO Subtract<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit) where T : struct, Enum
        {
            double base1 = GetBaseValue<T>(q1);
            double base2 = GetBaseValue<T>(q2);
            var result = CreateFromBase<T>(base1 - base2, targetUnit);

            _cacheRepo.SaveToCache(new CacheRecordDto { OperationType = "Subtraction", InputDetails = $"{q1.Value} {q1.Unit} - {q2.Value} {q2.Unit}", Result = $"{result.Value} {result.Unit}" });
            return result;
        }

        public double Divide<T>(QuantityDTO q1, QuantityDTO q2) where T : struct, Enum
        {
            if (typeof(T) == typeof(TemperatureUnit))
                throw new InvalidOperationException("Temperature does not support division.");

            double base1 = GetBaseValue<T>(q1);
            double base2 = GetBaseValue<T>(q2);

            if (Math.Abs(base2) < 1e-6) throw new DivideByZeroException("Cannot divide by zero.");
            
            double result = Math.Round(base1 / base2, 4);

            _cacheRepo.SaveToCache(new CacheRecordDto { OperationType = "Division", InputDetails = $"{q1.Value} {q1.Unit} / {q2.Value} {q2.Unit}", Result = result.ToString() });
            return result;
        }

        
        public bool Compare<T>(QuantityDTO q1, QuantityDTO q2) where T : struct, Enum
        {
            bool equal = Math.Abs(GetBaseValue<T>(q1) - GetBaseValue<T>(q2)) < 1e-6;
            _cacheRepo.SaveToCache(new CacheRecordDto { OperationType = "Comparison", InputDetails = $"{q1.Value} {q1.Unit} == {q2.Value} {q2.Unit}", Result = equal.ToString() });
            return equal;
        }

        public IEnumerable<CacheRecordDto> GetHistory() => _cacheRepo.GetAllHistory();
    }
}