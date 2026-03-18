using System;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementApp.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n-----------------------");
                Console.WriteLine("Quantity Measurement App");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1. Length\n2. Weight\n3. Volume\n4. Temperature\n5. View History\n6. Exit");
                
                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1": RunCategory<LengthUnit>("Length"); break;
                    case "2": RunCategory<WeightUnit>("Weight"); break;
                    case "3": RunCategory<VolumeUnit>("Volume"); break;
                    case "4": RunCategory<TemperatureUnit>("Temperature"); break;
                    case "5": ViewHistory(); break;
                    case "6": exit = true; break;
                    default: Console.WriteLine("Invalid choice"); break;
                }
            }
        }

        private void ViewHistory()
        {
            Console.WriteLine("\n--- Operation History ---");
            var history = _service.GetHistory();
            foreach(var record in history)
            {
                Console.WriteLine($"[{record.Timestamp:HH:mm:ss}] {record.OperationType}: {record.InputDetails} => {record.Result}");
            }
        }

        private void RunCategory<T>(string name) where T : struct, Enum
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine($"\n--- {name} ---");
                Console.WriteLine("1. Conversion\n2. Comparison\n3. Addition\n4. Subtraction\n5. Division\n6. Back");
                string choice = Console.ReadLine() ?? "";

                if (choice == "6") { back = true; continue; }

                PrintUnits<T>();

                try
                {
                    switch (choice)
                    {
                        case "1": HandleConversion<T>(); break;
                        case "2": HandleComparison<T>(); break;
                        case "3": HandleMath<T>("Add"); break;
                        case "4": HandleMath<T>("Subtract"); break;
                        case "5": HandleDivide<T>(); break;
                        default: Console.WriteLine("Invalid Choice"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void PrintUnits<T>() where T : struct, Enum
        {
            Console.WriteLine("Available Units:");
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Console.Write($"{(int)value}:{value}  ");
            }
            Console.WriteLine();
        }

        private void HandleConversion<T>() where T : struct, Enum
        {
            Console.Write("Enter Value: "); double val = double.Parse(Console.ReadLine()!);
            Console.Write("Source Unit Index: "); string src = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;
            Console.Write("Target Unit Index: "); string tgt = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;

            var result = _service.Convert<T>(new QuantityDTO(val, src), tgt);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private void HandleComparison<T>() where T : struct, Enum
        {
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); string u1 = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;
            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); string u2 = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;

            bool equal = _service.Compare<T>(new QuantityDTO(v1, u1), new QuantityDTO(v2, u2));
            Console.WriteLine($"Are they equal? {equal}");
        }

        private void HandleMath<T>(string operation) where T : struct, Enum
        {
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); string u1 = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;
            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); string u2 = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;
            Console.Write("Target Unit Index: "); string tgt = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;

            QuantityDTO q1 = new QuantityDTO(v1, u1);
            QuantityDTO q2 = new QuantityDTO(v2, u2);
            QuantityDTO result = operation == "Add" ? _service.Add<T>(q1, q2, tgt) : _service.Subtract<T>(q1, q2, tgt);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private void HandleDivide<T>() where T : struct, Enum
        {
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); string u1 = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;
            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); string u2 = Enum.GetName(typeof(T), int.Parse(Console.ReadLine()!))!;

            double result = _service.Divide<T>(new QuantityDTO(v1, u1), new QuantityDTO(v2, u2));
            Console.WriteLine($"Result (Ratio): {result}");
        }
    }
}