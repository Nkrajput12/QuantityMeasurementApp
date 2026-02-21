using System;
using System.Dynamic;
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
    
    /// <summary>
    /// Starts the main application loop, displaying the menu options 
    /// and routing user choices to the appropriate logic.
    /// </summary>
    public void Run()
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
            Console.WriteLine("11. Exit");
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

    
}