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
            Console.WriteLine("1. Compare Feet Equality");
            Console.WriteLine("2. Compare Inches Equality");
            Console.WriteLine("3. Exit");
            string choice = Console.ReadLine()??"";
            switch (choice)
            {
                case "1":
                CompareFeetInput();
                break;

                case "2":
                CompareInchesInput();
                break;

                case "3":
                exit = true;
                break;

                default:
                Console.WriteLine("Invalid Input");
                break;
            }
        }

    }

    /// <summary>
    /// Captures numerical input from the console, instantiates Feet objects, 
    /// and invokes FeetUtil to compare them for equality.
    /// and capture the Exceptions like Format and General exception
    /// </summary>
    private void CompareFeetInput()
    {
        try
        {
            Console.Write("Enter first value in feet: ");
            double val1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter second value in feet: ");
            double val2 = Convert.ToDouble(Console.ReadLine());

            Feet feet1 = new Feet(val1);
            Feet feet2 = new Feet(val2);

            bool Equal = service.CompareFeet(feet1,feet2);

            Console.WriteLine(Equal ? "The Measurement are Equal" : "The Measurement are not Equal");
        }
        catch(FormatException ex)
        {
            Console.WriteLine("Format Exception "+ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine("General Exception: "+ex.Message);
        }
    }

    /// <summary>
    /// Captures numerical input from the console, instantiates Inches objects, 
    /// and invokes InchesUtil to compare them for equality.
    /// and capture the Exceptions like Format and General exception
    /// </summary>
    public void CompareInchesInput()
    {
        try
        {
            Console.Write("Enter first value in Inches: ");
            double val1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter second value in Inches: ");
            double val2 = Convert.ToDouble(Console.ReadLine());

            Inches inches1 = new Inches(val1);
            Inches inches2 = new Inches(val2);

            bool Equal = service.CompareInches(inches1,inches2);

            Console.WriteLine(Equal ? "The Measurement are Equal" : "The Measurement are not Equal");
        }
        catch(FormatException ex)
        {
            Console.WriteLine("Format Exception "+ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine("General Exception: "+ex.Message);
        }
    }

    
}