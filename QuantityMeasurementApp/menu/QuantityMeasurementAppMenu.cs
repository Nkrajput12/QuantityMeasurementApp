using System;
using System.Dynamic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using QuantityMeasurementApp.models;

/// <summary>
/// Provides a console-based interface for the Quantity Measurement Application.
/// Handles user input, menu navigation, and coordinates the comparison of 
/// measurement units using service utilities.
/// </summary>
public class QuantityMeasurementAppMenu
{
    private QuantityMeasurementService service = new QuantityMeasurementService();
    

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Quantity Measurement App");
            Console.WriteLine("-----------------------");
            Console.WriteLine("\n1. Length Measurement");
            Console.WriteLine("2. Weight Measurement");
            Console.WriteLine("3. Exit");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": RunLength(); break;
                case "2": RunWeight(); break;
                case "3": exit = true; break;
                default: Console.WriteLine("Invalid choice"); break;
            }
        }
    }
    public void RunWeight()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Weight Measurement");
            Console.WriteLine("-----------------------");
            Console.WriteLine("\n1. Conversion");
            Console.WriteLine("2. Comparison");
            Console.WriteLine("3. Addition");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": HandleWeightConversion(); break;
                case "2": HandleWeightComparison(); break;
                case "3": HandleLengthAddition(); break;
                case "4": back = true; break;
                default: Console.WriteLine("Invalid Choice"); break;
            }

        }
    }
    public void RunLength()
    {
        bool exit = false;
        while(!exit)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Length Measurement");
            Console.WriteLine("-----------------------");
            Console.WriteLine("\n1. Conversion");
            Console.WriteLine("2. Comparison");
            Console.WriteLine("3. Addition");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": HandleLengthConversion(); break;
                case "2": HandleLengthComparison(); break;
                case "3": HandleLengthAddition(); break;
                case "4": exit = true; break;
                default: Console.WriteLine("Invalid Choice"); break;
            }
        }
    }
    
    /// <summary>
    /// This Method Handle the Length Comparison Logic
    /// </summary>
    private void HandleLengthComparison()
    {
        try
        {
            Console.WriteLine("\n--- Length Comparison ---");
            // Display options dynamically to avoid hardcoding
            Console.WriteLine("Available Units: 0:Inches, 1:Feet, 2:Yards, 3:CM");

            // 1. Get First Quantity
            Console.Write("Select First Unit Index: ");
            LengthUnit u1 = (LengthUnit)int.Parse(Console.ReadLine() ?? "0");
            Console.Write($"Enter value in {u1.GetSymbol()}: ");
            double val1 = double.Parse(Console.ReadLine() ?? "0");

            // 2. Get Second Quantity
            Console.Write("Select Second Unit Index: ");
            LengthUnit u2 = (LengthUnit)int.Parse(Console.ReadLine() ?? "0");
            Console.Write($"Enter value in {u2.GetSymbol()}: ");
            double val2 = double.Parse(Console.ReadLine() ?? "0");

            // 3. Create Objects
            Quantity q1 = new Quantity(val1, u1);
            Quantity q2 = new Quantity(val2, u2);


            bool isEqual = q1.Equals(q2);
        
            Console.WriteLine("\n-----------------------");
            Console.WriteLine($"Result: {q1} {(isEqual ? "==" : "!=")} {q2}");
            Console.WriteLine(isEqual ? " Measurements are Equal" : " Measurements are NOT Equal");
            Console.WriteLine("-----------------------\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n Error: {ex.Message}. Please enter valid numbers and indices.");
        }
    }

    /// <summary>
    /// Generic Method to Convert value From Source to Target
    /// taking value and Index of Length Unit of both Source
    /// and Target and call method DemonstrateLengthConversion
    /// </summary>
    private void HandleLengthConversion()
    {
        Console.Write("Enter Value: ");
        double val = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Units : 0:Inches , 1:Feet, 2:Yard, 3:CM");
        Console.Write("Enter Source Unit Index: ");
        LengthUnit source = (LengthUnit)int.Parse(Console.ReadLine() ?? "");
        Console.Write("Enter Target Unit Index: ");
        LengthUnit target = (LengthUnit)int.Parse(Console.ReadLine()??"");
        try
        {
            double result = service.DemonstrateLengthConversion(val,source,target);
            Console.WriteLine($"{val}{source.GetSymbol()} = {result}{target.GetSymbol()}");
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: "+ex.Message);
        }

    }
    /// <summary>
    /// Method Handle the Length Addition Logic
    /// </summary>
    private void HandleLengthAddition()
    {
        Console.WriteLine("Unit : 0:Inches, 1:Feet, 2:Yards 3:Centimeter");

        //Get first Quantity
        Console.Write("Enter Value 1: ");
        double val1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Unit 1(Index): ");
        LengthUnit u1 = (LengthUnit)int.Parse(Console.ReadLine()??"");
        Quantity q1 = new Quantity(val1,u1);
        
        //Get Second Quantity
        Console.Write("Enter Value 2: ");
        double val2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Unit 2(Index): ");
        LengthUnit u2 = (LengthUnit)int.Parse(Console.ReadLine()??"");
        Quantity q2 = new Quantity(val2,u2);

        Console.Write("Explicit target selection (Y/N): ");
        string c = Console.ReadLine()?.ToLower() ?? "n";
        Quantity result;
        if(c == "y")
        {
            Console.WriteLine("Enter the unit index of Target unit");
            LengthUnit targetUnit = (LengthUnit)int.Parse(Console.ReadLine() ?? "");
            result = q1.Add(q2,targetUnit);
        }
        else
        {
            result = q1.Add(q2);
        }        
        Console.WriteLine($"\nCalculation {q1} + {q2}");
        Console.WriteLine ($"Result = {result}");
    }  


    /// <summary>
    /// Method Handle The Weight Comparison Logic
    /// </summary>
    private void HandleWeightComparison()
    {
        try
        {
            Console.WriteLine("Units: 0:Grams, 1:Kilograms, 2:Pounds");
            
            Console.Write("Enter Value 1: ");
            double v1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Unit 1 Index: ");
            WeightUnit u1 = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Value 2: ");
            double v2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Unit 2 Index: ");
            WeightUnit u2 = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");

            QuantityWeight w1 = new QuantityWeight(v1, u1);
            QuantityWeight w2 = new QuantityWeight(v2, u2);

            Console.WriteLine(w1.Equals(w2) ? "Weights are Equal" : "Weights are not Equal");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    /// <summary>
    /// Method Handle the Weight Conversion Logic
    /// </summary>
    private void HandleWeightConversion()
    {
        Console.Write("Enter Value: ");
        double val = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Units: 0:Grams, 1:Kilograms, 2:Pounds");
        
        Console.Write("Enter Source Unit Index: ");
        WeightUnit source = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Enter Target Unit Index: ");
        WeightUnit target = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");

        QuantityWeight w = new QuantityWeight(val, source);
        QuantityWeight result = w.ConvertTo(target); 
        
        Console.WriteLine($"{val}{source.GetSymbol()} = {result.value}{target.GetSymbol()}");
    }


    /// <summary>
    /// This Method Handle the Weight Addition 
    /// </summary>
    private void HandleWeightAddition()
    {
        Console.WriteLine("Units: 0:Grams, 1:Kilograms, 2:Pounds");
        
        Console.Write("Enter Value 1: ");
        double v1 = Convert.ToDouble(Console.ReadLine());
        WeightUnit u1 = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Enter Value 2: ");
        double v2 = Convert.ToDouble(Console.ReadLine());
        WeightUnit u2 = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");

        QuantityWeight w1 = new QuantityWeight(v1, u1);
        QuantityWeight w2 = new QuantityWeight(v2, u2);

        Console.Write("Explicit target selection (Y/N): ");
        string choice = Console.ReadLine()?.ToLower() ?? "n";
        
        QuantityWeight result;
        if (choice == "y")
        {
            Console.Write("Enter Target Unit Index: ");
            WeightUnit target = (WeightUnit)int.Parse(Console.ReadLine() ?? "0");
            result = w1.Add(w2, target);
        }
        else
        {
            result = w1.Add(w2, u1); // Default to first unit
        }

        Console.WriteLine($"\nResult = {result.value} {result.unit.GetSymbol()}");
    }
}