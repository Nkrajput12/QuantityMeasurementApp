using NUnit.Framework;
using NUnit.Framework.Internal;
using QuantityMeasurementApp.models;

namespace QuantityMeasurementApp.Tests;


/// <summary>
/// FeetTests class for testing the feet class 
/// Equals method using n unit testing
/// </summary>
[TestFixture]
public class FeetTests
{
    
    /// <summary>
    /// Take two different Feet values 
    /// and check for not equal
    /// </summary>
    [Test]
    public void GivenOneFeetAndThreeFeet_WhenCompared_ReturnNotEqual()
    {
        Feet feet1 = new Feet(1.0);
        Feet feet2 = new Feet(2.0);

        Assert.That(feet1,Is.Not.EqualTo(feet2));
    }

    /// <summary>
    /// take two same Feet values
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenOneFeetAndOneFeet_WhenCompared_ReturnEqual()
    {
        Feet feet1 = new Feet(1.0);
        Feet feet2 = new Feet(1.0);
        
        Assert.That(feet1,Is.EqualTo(feet2));
    }

    /// <summary>
    /// Take one feet value and another null value
    /// and check for not equal
    /// </summary>

    [Test]
    public void GivenOneFeetAndNull_WhenCompared_ReturnNotEqual()
    {
        Feet feet1 = new Feet(1.0);

        Assert.That(feet1,Is.Not.EqualTo(null));
    }

    /// <summary>
    /// compare the same value to itself 
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenOneComparedToSelf_WhenCompared_ReturnEqual()
    {
        Feet feet = new Feet(1.0);
        Feet feet2 = feet;

        Assert.That(feet,Is.SameAs(feet2));  //check Reference equality
    }

    /// <summary>
    /// Compare two same negative value 
    /// and check for equal
    /// </summary>

    [Test]
    public void GivenTwoSameNegativeValues_WhenCompared_ReturnEqual()
    {
        Feet feet1 = new Feet(-1.0);
        Feet feet2 = new Feet(-1.0);

        Assert.That(feet1,Is.EqualTo(feet2));
    }

    /// <summary>
    /// compare two same Large values
    /// and check for equal
    /// </summary>
    [Test]
    public void GivenTwoSameLargeValues_WhenCompared_ReturnEqual()
    {
        Feet feet1 = new Feet(9999999);
        Feet feet2 = new Feet(9999999);

        Assert.That(feet1,Is.EqualTo(feet2));
    }

    /// <summary>
    /// compare two different Large values
    /// and check for Not equal
    /// </summary>
    [Test]
    public void GivenTwoDifferentLargeValues_WhenCompared_ReturnNotEqual()
    {
        Feet feet1 = new Feet(99999999);
        Feet feet2 = new Feet(11111111);

        Assert.That(feet1,Is.Not.EqualTo(feet2));

    }
    
}
