using System;
using System.Dynamic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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
        while(!exit)
        {
            Console.WriteLine("1. Conversion");
            Console.WriteLine("2. Comparison");
            Console.WriteLine("3. Addition");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": HandleConversion(); break;
                case "2": CompareRun(); break;
                case "3": HandleAddition(); break;
                case "4": exit = true; break;
                default: Console.WriteLine("Invalid Choice"); break;
            }
        }
    }
    
    /// <summary>
    /// Starts the main application loop, displaying the menu options 
    /// and routing user choices to the appropriate logic.
    /// </summary>
    public void CompareRun()
    {
        bool exit =  false;
        while (!exit)
        {
            Console.WriteLine("1. Compare Feet with Feet");
            Console.WriteLine("2. Compare Inches with Inches");
            Console.WriteLine("3. Compare Yards with Yards");
            Console.WriteLine("4. Compare Centimeter with Centimeter");
            Console.WriteLine("5. Compare Feet with Inches");
            Console.WriteLine("6. compare Yards with Inches");
            Console.WriteLine("7. Compare Centimeter with Inches");
            Console.WriteLine("8. Compare Feet with Yards");
            Console.WriteLine("9. Compare Centimeter with Feet");
            Console.WriteLine("10. Compare Yard with Centimeter");
            Console.WriteLine("11. Back");
            string choice = Console.ReadLine()??"";
            switch (choice)
            {
                case "1":
                PerformComparison(LengthUnit.Feet,LengthUnit.Feet);
                break;

                case "2":
                PerformComparison(LengthUnit.Inches,LengthUnit.Inches);
                break;

                case "3":
                PerformComparison(LengthUnit.Yards,LengthUnit.Yards);
                break;

                case "4":
                PerformComparison(LengthUnit.Centimeters,LengthUnit.Centimeters);
                break;

                case "5":
                PerformComparison(LengthUnit.Feet,LengthUnit.Inches);
                break;
                
                case "6":
                PerformComparison(LengthUnit.Yards,LengthUnit.Inches);
                break;

                case "7":
                PerformComparison(LengthUnit.Centimeters,LengthUnit.Inches);
                break;

                case "8":
                PerformComparison(LengthUnit.Feet,LengthUnit.Yards);
                break;

                case "9":
                PerformComparison(LengthUnit.Centimeters,LengthUnit.Feet);
                break;

                case "10":
                PerformComparison(LengthUnit.Yards,LengthUnit.Centimeters);
                break;

                case "11":
                exit = true;
                break;

                default:
                Console.WriteLine("Invalid Input");
                break;
            }
        }

    }

    /// <summary>
    /// Generic input handler that works for any combination of units.
    /// This eliminates the need for separate 'CompareFeet' and 'CompareInches' methods.
    /// </summary>
    private void PerformComparison(LengthUnit u1 , LengthUnit u2)
    {
        try
        {
            Console.Write($"Enter value1 in {u1.GetSymbol()}: ");
            double val1 = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Enter value2 in {u2.GetSymbol()}: ");
            double val2 = Convert.ToInt32(Console.ReadLine());

            Quantity q1 = new Quantity(val1, u1);
            Quantity q2 = new Quantity(val2, u2);

            Console.WriteLine(service.Compare(q1,q2) ? "Measurement are Equal" : "Measurement are not Equal");
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: "+ex.Message);
        }
    }

    /// <summary>
    /// Generic Method to Convert value From Source to Target
    /// taking value and Index of Length Unit of both Source
    /// and Target and call method DemonstrateLengthConversion
    /// </summary>
    private void HandleConversion()
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

    private void HandleAddition()
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
        Console.WriteLine($"Result = {result}");
    }  
}