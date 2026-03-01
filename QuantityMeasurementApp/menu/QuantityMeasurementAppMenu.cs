using System;
using QuantityMeasurementApp.models;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services; 

public class QuantityMeasurementAppMenu
{
    private QuantityMeasurementService service = new QuantityMeasurementService();

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n-----------------------");
            Console.WriteLine("Quantity Measurement App");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Length Measurement");
            Console.WriteLine("2. Weight Measurement");
            Console.WriteLine("3. Volume Measurement");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": RunCategory<LengthUnit>("Length", "0:Inches, 1:Feet, 2:Yards, 3:CM"); break;
                case "2": RunCategory<WeightUnit>("Weight", "0:Grams, 1:Kilograms, 2:Pounds"); break;
                case "3": RunCategory<VolumeUnit>("Volume", "0:Liter, 1:MilliLiter, 2:Gallon"); break;
                case "4": exit = true; break;
                default: Console.WriteLine("Invalid choice"); break;
            }
        }
    }

    /// <summary>
    /// UC10: Generic Category Runner. 
    /// This replaces RunLength and RunWeight with a single polymorphic method.
    /// </summary>
    private void RunCategory<T>(string name, string unitList) where T : struct, Enum
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine($"\n--- {name} Measurement ---");
            Console.WriteLine("1. Conversion\n2. Comparison\n3. Addition\n4. subtraction\n5. Divide\n6. back");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": HandleConversion<T>(unitList); break;
                case "2": HandleComparison<T>(unitList); break;
                case "3": HandleAddition<T>(unitList); break;
                case "4": HandleSubtraction<T>(unitList); break;
                case "5": HandleDivision<T>(unitList); break;
                case "6": back = true; break;
                default: Console.WriteLine("Invalid Choice"); break;
            }
        }
    }


    /// <summary>
    /// UC12: Handles Division (Scalar Ratio)
    /// </summary>
    private void HandleDivision<T>(string unitList) where T : struct, Enum
    {
        try
        {
            Console.WriteLine(unitList);
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); T u1 = (T)(object)int.Parse(Console.ReadLine()!);
            
            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); T u2 = (T)(object)int.Parse(Console.ReadLine()!);

            double ratio = service.Divide(v1,u1,v2,u2 );
            Console.WriteLine($"\nResult Ratio: {ratio} (Dimensionless)");
        }
        catch (ArithmeticException ex) { Console.WriteLine("Math Error: " + ex.Message); }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
    }

    /// <summary>
    /// UC12: Handles Subtraction of two quantities
    /// </summary>
    private void HandleSubtraction<T>(string unitList) where T : struct, Enum
    {
        try
        {
            Console.WriteLine(unitList);
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); T u1 = (T)(object)int.Parse(Console.ReadLine()!);
            
            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); T u2 = (T)(object)int.Parse(Console.ReadLine()!);

            Quantity<T> q1 = new Quantity<T>(v1, u1);
            Quantity<T> q2 = new Quantity<T>(v2, u2);

            Console.Write("Explicit target selection (Y/N): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Target Unit Index: "); T tgt = (T)(object)int.Parse(Console.ReadLine()!);
                Console.WriteLine($"Result: {service.Subtract(q1, q2, tgt)}");
            }
            else
            {
                Console.WriteLine($"Result: {service.Subtract(q1, q2,q1.Unit)}");
            }
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
    }
    private void HandleComparison<T>(string unitList) where T : struct, Enum
    {
        try
        {
            Console.WriteLine(unitList);
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); T u1 = (T)(object)int.Parse(Console.ReadLine()!);

            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); T u2 = (T)(object)int.Parse(Console.ReadLine()!);

            Quantity<T> q1 = new Quantity<T>(v1, u1);
            Quantity<T> q2 = new Quantity<T>(v2, u2);

            bool equal = service.Compare(q1, q2);
            Console.WriteLine($"\nResult: {q1} {(equal ? "==" : "!=")} {q2}");
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
    }

    private void HandleConversion<T>(string unitList) where T : struct, Enum
    {
        try
        {
            Console.WriteLine(unitList);
            Console.Write("Enter Value: "); double val = double.Parse(Console.ReadLine()!);
            Console.Write("Source Unit Index: "); T src = (T)(object)int.Parse(Console.ReadLine()!);
            Console.Write("Target Unit Index: "); T tgt = (T)(object)int.Parse(Console.ReadLine()!);

            Quantity<T> q = new Quantity<T>(val, src);
            Quantity<T> result = service.DemonstrateConversion(q, tgt);
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
    }

    private void HandleAddition<T>(string unitList) where T : struct, Enum
    {
        try
        {
            Console.WriteLine(unitList);
            Console.Write("Value 1: "); double v1 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 1 Index: "); T u1 = (T)(object)int.Parse(Console.ReadLine()!);
            
            Console.Write("Value 2: "); double v2 = double.Parse(Console.ReadLine()!);
            Console.Write("Unit 2 Index: "); T u2 = (T)(object)int.Parse(Console.ReadLine()!);

            Quantity<T> q1 = new Quantity<T>(v1, u1);
            Quantity<T> q2 = new Quantity<T>(v2, u2);

            Console.Write("Explicit target selection (Y/N): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Target Unit Index: "); T tgt = (T)(object)int.Parse(Console.ReadLine()!);
                Console.WriteLine($"Result: {service.DemonstrateAddition(q1, q2, tgt)}");
            }
            else
            {
                Console.WriteLine($"Result: {service.DemonstrateAddition(q1, q2)}");
            }
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
    }
}