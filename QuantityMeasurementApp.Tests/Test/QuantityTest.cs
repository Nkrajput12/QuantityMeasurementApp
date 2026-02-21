using NUnit.Framework;
using QuantityMeasurementApp.models;

/// <summary>
/// Test suite for UC3 (Generic Quantity Class).
/// These tests verify that the code follows the DRY principle and handles
/// cross-unit comparisons (Feet to Inches) correctly.
/// </summary>
[TestFixture]
public class QuantityTest
{

    //test two equal quantities with different unit
    [Test]
    public void GivenOneFeetAndTwelveInches_WhenCompared_ReturnTrue()
    {
        Quantity feet = new Quantity(1.0,LengthUnit.Feet);
        Quantity inches = new Quantity(12.0,LengthUnit.Inches);

        Assert.That(feet,Is.EqualTo(inches));
    }

    //test two equal quantities with different unit
    [Test]
    public void GivenTwelveInchesAndOneFeet_WhenCompared_ReturnTrue()
    {
        Quantity inches = new Quantity(12.0,LengthUnit.Inches);
        Quantity feet = new Quantity(1.0,LengthUnit.Feet);

        Assert.That(inches,Is.EqualTo(feet));

    }

    //test two equal quantity with same unit
    [Test]
    public void GivenOneFeetAndTwoFeet_WhenCompared_ReturnFalse()
    {
        Quantity feet1 = new Quantity(1.0, LengthUnit.Feet);
        Quantity feet2 = new Quantity(2.0, LengthUnit.Feet);
        Assert.That(feet1, Is.Not.EqualTo(feet2));
    }

    //test same quantity and compare to itself
    [Test]
    public void GivenQuantityObject_WhenComparedToSelf_ShouldReturnTrue()
    {
        Quantity feet = new Quantity(1.0, LengthUnit.Feet);
        Assert.That(feet.Equals(feet), Is.True);
    }


    //test with null
    [Test]

    public void GivenQuantityObject_WhenComparedWithNull_ShouldReturnFalse()
    {
        Quantity feet = new Quantity(1.0, LengthUnit.Feet);
        Assert.That(feet, Is.Not.EqualTo(null));
    }


    //compare zero inches with zero Feet
    [Test]
    public void GivenZeroFeetAndZeroInches_WhenCompared_ShouldReturnTrue()
    {
        Quantity zeroFeet = new Quantity(0.0, LengthUnit.Feet);
        Quantity zeroInches = new Quantity(0.0, LengthUnit.Inches);
        Assert.That(zeroFeet, Is.EqualTo(zeroInches));
    }



}