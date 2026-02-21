using NUnit.Framework;
using QuantityMeasurementApp.models;


/// <summary>
/// InchesTests class for testing the Inches class 
/// Equals method using n unit testing
/// </summary>
[TestFixture]
public class InchesTests
{
    /// <summary>
    /// Take two different Inches values 
    /// and check for not equal
    /// </summary>
    [Test]
    public void GivenOneInchesAndThreeInches_WhenCompare_ReturnNotEqual()
    {
        Inches inches1 = new Inches(1.0);
        Inches inches2 = new Inches(3.0);

        Assert.That(inches1,Is.Not.EqualTo(inches2));

    }

    /// <summary>
    /// take two same Inches values
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenOneInchesAndOneInches_WhenCompare_ReturnEqual()
    {
        Inches inches1 = new Inches(1.0);
        Inches inches2 = new Inches(1.0);

        Assert.That(inches1,Is.EqualTo(inches2));
    }
    
    /// <summary>
    /// Take one Inches value and another null value
    /// and check for not equal
    /// </summary>
    [Test]
    public void GivenOneInchesComparedWithNull_WhenCompare_ReturnNotEqual()
    {
        Inches inches1 = new Inches(1.0);

        Assert.That(inches1,Is.Not.EqualTo(null));
    }

    /// <summary>
    /// compare the same value to itself 
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenOneInchesComparedToSelf_WhenCompare_ReturnEqual()
    {
        Inches inches1 = new Inches(1.0);
        Inches inches2 = inches1;

        Assert.That(inches1,Is.SameAs(inches2));
    }

    /// <summary>
    /// Compare two same negative value 
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenTwoSameNegativeInches_WhenCompare_ReturnEqual()
    {
        Inches inches1 = new Inches(-2.0);
        Inches inches2 = new Inches(-2.0);

        Assert.That(inches1,Is.EqualTo(inches2));
    }

    /// <summary>
    /// Compare two same negative value 
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenTwoDifferentNegativeInches_WhenCompare_ReturnNotEqual()
    {
        Inches inches1 = new Inches(-2.0);
        Inches inches2 = new Inches(-3.0);

        Assert.That(inches1,Is.Not.EqualTo(inches2));
    }

    /// <summary>
    /// compare two same Large values
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenTwoSameLargeInches_WhenCompare_ReturnEqual()
    {
        Inches inches1 = new Inches(9999999999);
        Inches inches2 = new Inches(9999999999);

        Assert.That(inches1,Is.EqualTo(inches2));
    }

    /// <summary>
    /// compare two different Large values
    /// and check for Not equal
    /// </summary>
    [Test]
    public void GivenTwoDifferentLargeInches_WhenCompared_ReturnNotEqual()
    {
        Inches inches1 = new Inches(9999999999);
        Inches inches2 = new Inches(1111111111);

        Assert.That(inches1,Is.Not.EqualTo(inches2));
    }

}