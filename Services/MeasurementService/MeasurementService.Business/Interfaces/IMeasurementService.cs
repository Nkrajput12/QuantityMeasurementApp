using Common.Models.DTOs;

namespace MeasurementService.Business.Interfaces;

public interface IMeasurementService
{
    object Convert<TUnit>(QuantityDto value, string targetUnit, int? userId) where TUnit : struct,Enum;
    object Add<TUnit>(QuantityDto value1, QuantityDto value2, string targetUnit, int? userId) where TUnit : struct,Enum;
    object Subtract<TUnit>(QuantityDto value1, QuantityDto value2, string targetUnit, int? userId) where TUnit : struct,Enum;
    double Divide<TUnit>(QuantityDto value1, QuantityDto value2, int? userId) where TUnit : struct,Enum;
    bool Compare<TUnit>(QuantityDto value1, QuantityDto value2, int? userId) where TUnit : struct,Enum;
}