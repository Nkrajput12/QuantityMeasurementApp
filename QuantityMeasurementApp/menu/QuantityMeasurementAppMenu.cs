using System;
using System.Runtime.CompilerServices;
using QuantityMeasurementApp.models;
using QuantityMeasurementApp.service;

/// <summary>
/// Provides a console-based interface for the Quantity Measurement Application.
/// Handles user input, menu navigation, and coordinates the comparison of 
/// measurement units using service utilities.
/// </summary>
public class QuantityMeasurementAppMenu
{
    private FeetUtil feet = new FeetUtil();
    
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
            Console.WriteLine("2. Exit");
            string choice = Console.ReadLine()??"";
            switch (choice)
            {
                case "1":
                CompareFeetInput();
                break;

                case "2":
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

            bool Equal = feet.CompareFeet(feet1,feet2);

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