using System;
using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Services
{
    public interface IQuantityMeasurementService
    {
        QuantityDTO Convert<T>(QuantityDTO source, string targetUnit) where T : struct, Enum;
        QuantityDTO Add<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit) where T : struct, Enum;
        QuantityDTO Subtract<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit) where T : struct, Enum;
        double Divide<T>(QuantityDTO q1, QuantityDTO q2) where T : struct, Enum;
        bool Compare<T>(QuantityDTO q1, QuantityDTO q2) where T : struct, Enum;
        IEnumerable<CacheRecordDto> GetHistory();
    }
}