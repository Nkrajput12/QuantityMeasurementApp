using System;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Interfaces;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMeasurementAppMenu : IQuantityMeasurementAppMenu
    {
        private readonly QuantityMeasurementService measurementService = new QuantityMeasurementService();

        private IUnitConverter<T> ResolveConverter<T>() where T : struct, Enum
        {
            switch (typeof(T).Name)
            {
                case nameof(LengthUnit):
                    return (IUnitConverter<T>)(object)new LengthUnitConverter();
                case nameof(WeightUnit):
                    return (IUnitConverter<T>)(object)new WeightUnitConverter();
                case nameof(VolumeUnit):
                    return (IUnitConverter<T>)(object)new VolumeUnitConverter();
                case nameof(TemperatureUnit):
                    return (IUnitConverter<T>)(object)new TemperatureUnitConverter();
                default:
                    throw new NotSupportedException($"Unsupported unit type {typeof(T).Name}");
            }
        }

        public void Run()
        {
            bool terminateProgram = false;

            while (!terminateProgram)
            {
                Console.WriteLine("\n-----------------------");
                Console.WriteLine("Quantity Measurement App");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1. Length Measurement");
                Console.WriteLine("2. Weight Measurement");
                Console.WriteLine("3. Volume Measurement");
                Console.WriteLine("4. Temperature Measurement");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string menuChoice = Console.ReadLine() ?? "";

                switch (menuChoice)
                {
                    case "1":
                        RunCategory<LengthUnit>("Length", "0:Inches, 1:Feet, 2:Yards, 3:CM");
                        break;
                    case "2":
                        RunCategory<WeightUnit>("Weight", "0:Grams, 1:Kilograms, 2:Pounds");
                        break;
                    case "3":
                        RunCategory<VolumeUnit>("Volume", "0:Liter, 1:MilliLiter, 2:Gallon");
                        break;
                    case "4":
                        RunCategory<TemperatureUnit>("Temperature", "0:Celsius, 1:Fahrenheit, 2:Kelvin");
                        break;
                    case "5":
                        terminateProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void RunCategory<T>(string categoryTitle, string unitOptions) where T : struct, Enum
        {
            bool goBack = false;

            while (!goBack)
            {
                Console.WriteLine($"\n--- {categoryTitle} Measurement ---");
                Console.WriteLine("1. Conversion\n2. Comparison\n3. Addition\n4. Subtraction\n5. Divide\n6. Back");
                Console.Write("Select an action: ");

                string actionChoice = Console.ReadLine() ?? "";

                switch (actionChoice)
                {
                    case "1": HandleConversion<T>(unitOptions); break;
                    case "2": HandleComparison<T>(unitOptions); break;
                    case "3": HandleAddition<T>(unitOptions); break;
                    case "4": HandleSubtraction<T>(unitOptions); break;
                    case "5": HandleDivision<T>(unitOptions); break;
                    case "6": goBack = true; break;
                    default:
                        Console.WriteLine("Invalid Choice. Please try again.");
                        break;
                }
            }
        }


        private Quantity<T> ReadQuantity<T>(string label, IUnitConverter<T> converter) where T : struct, Enum
        {
            Console.Write($"Value {label}: ");
            double v = double.Parse(Console.ReadLine()!);

            Console.Write($"Unit {label} Index: ");
            T u = (T)(object)int.Parse(Console.ReadLine()!);

            return new Quantity<T>(v, u, converter);
        }

        private void HandleComparison<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine($"\n{unitOptions}");
                var converter = ResolveConverter<T>();

                Quantity<T> q1 = ReadQuantity("1", converter);
                Quantity<T> q2 = ReadQuantity("2", converter);

                var result = measurementService.Compare(q1, q2);

                Console.WriteLine($"\nResult: {q1} {(result.AreEqual ? "==" : "!=")} {q2}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleConversion<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine($"\n{unitOptions}");
                var converter = ResolveConverter<T>();

                Quantity<T> q = ReadQuantity("", converter);

                Console.Write("Target Unit Index: ");
                T target = (T)(object)int.Parse(Console.ReadLine()!);

                var result = measurementService.DemonstrateConversion(q, target);

                Console.WriteLine($"Result: {result.Value} {result.UnitSymbol}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleAddition<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine($"\n{unitOptions}");
                var converter = ResolveConverter<T>();

                Quantity<T> q1 = ReadQuantity("1", converter);
                Quantity<T> q2 = ReadQuantity("2", converter);

                var result = measurementService.DemonstrateAddition(q1, q2);

                Console.WriteLine($"Result: {result.Value} {result.UnitSymbol}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleSubtraction<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine($"\n{unitOptions}");
                var converter = ResolveConverter<T>();

                Quantity<T> q1 = ReadQuantity("1", converter);
                Quantity<T> q2 = ReadQuantity("2", converter);

                var result = measurementService.Subtract(q1, q2, q1.Unit);

                Console.WriteLine($"Result: {result.Value} {result.UnitSymbol}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleDivision<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine($"\n{unitOptions}");
                var converter = ResolveConverter<T>();

                Quantity<T> q1 = ReadQuantity("1", converter);
                Quantity<T> q2 = ReadQuantity("2", converter);

                var result = measurementService.Divide(q1.Value, q1.Unit, q2.Value, q2.Unit);

                Console.WriteLine($"Result Ratio: {result.Ratio}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}