using System;

namespace QuantityMeasurementApp.models
{
    public enum TemperatureUnit
    {
        Celsius,    // Index 0 (Base Unit)
        Fahrenheit, // Index 1
        Kelvin      // Index 2
    }

    public static class TemperatureUnitExtension
    {
        // Convert to Base (Celsius)
        public static double ConvertToBase(this TemperatureUnit unit, double value) => unit switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => (value - 32.0) * 5.0 / 9.0,
            TemperatureUnit.Kelvin => value - 273.15,
            _ => throw new ArgumentException("Invalid Temperature Unit")
        };

        // Convert from Base (Celsius) to target unit
        public static double ConvertFromBase(this TemperatureUnit unit, double baseValue) => unit switch
        {
            TemperatureUnit.Celsius => baseValue,
            TemperatureUnit.Fahrenheit => (baseValue * 9.0 / 5.0) + 32.0,
            TemperatureUnit.Kelvin => baseValue + 273.15,
            _ => throw new ArgumentException("Invalid Temperature Unit")
        };

        // UI Symbol
        public static string GetSymbol(this TemperatureUnit unit) => unit switch
        {
            TemperatureUnit.Celsius => "°C",
            TemperatureUnit.Fahrenheit => "°F",
            TemperatureUnit.Kelvin => "K",
            _ => unit.ToString()
        };
    }
}