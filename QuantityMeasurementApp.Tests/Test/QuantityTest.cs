using NUnit.Framework;
using QuantityMeasurementApp.models;

/// <summary>
/// These tests verify that the code follows the DRY principle and handles
/// cross-unit comparisons
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


    //compare 1 yard with 3 feet
    [Test]
    public void GivenOneYardAndThreeFeet_WhenCompared_ReturnTrue()
    {
        Quantity yard = new Quantity(1.0, LengthUnit.Yards);
        Quantity feet = new Quantity(3.0, LengthUnit.Feet);
        Assert.That(yard,Is.EqualTo(feet));
    }

    //compare 1 yard with 36 inches
    [Test]
    public void GivenOneYardAndThirtySixInches_WhenCompared_ShouldReturnTrue()
    {
        Quantity yards = new Quantity(1.0, LengthUnit.Yards);
        Quantity inches = new Quantity(36.0, LengthUnit.Inches);
        Assert.That(yards, Is.EqualTo(inches));
    }

    //Compare 1 cm with 3937Inches
    [Test]
    public void Given1CmAndPoint3937Inches_WhenCompared_ShouldReturnTrue()
    {
        Quantity cm = new Quantity(1.0, LengthUnit.Centimeters);
        Quantity inches = new Quantity(0.393701, LengthUnit.Inches);
        Assert.That(cm, Is.EqualTo(inches));
    }

    //compare the transitive property like a = b,b=c the a must equal to c;
    [Test]
    public void testEquality_MultiUnit_TransitiveProperty()
    {
        Quantity yard = new Quantity(1.0, LengthUnit.Yards);
        Quantity feet = new Quantity(3.0, LengthUnit.Feet);
        Quantity inches = new Quantity(36.0, LengthUnit.Inches);

        Assert.Multiple(() => {
        Assert.That(yard, Is.EqualTo(feet));
        Assert.That(feet, Is.EqualTo(inches));
        Assert.That(yard, Is.EqualTo(inches));
        });
    }

    [Test]
    public void CompareYardToYard_SameValue_ShouldReturnTrue()
    {
        Quantity yard1 = new Quantity(1.0, LengthUnit.Yards);
        Quantity yard2 = new Quantity(1.0, LengthUnit.Yards);
        Assert.That(yard1, Is.EqualTo(yard2));
    }

    
    [Test]
    public void CompareYardToYard_DifferentValue_ShouldReturnFalse()
    {
        
        Quantity yard1 = new Quantity(1.0, LengthUnit.Yards);
        Quantity yard2 = new Quantity(2.0, LengthUnit.Yards);
        Assert.That(yard1, Is.Not.EqualTo(yard2));
    }

    [Test]
    public void testEquality_AllUnits_ComplexScenario()
    {
        // Complex verification: 2 yards == 6 feet == 72 inches
        Quantity q1 = new Quantity(2.0, LengthUnit.Yards);
        Quantity q2 = new Quantity(6.0, LengthUnit.Feet);
        Quantity q3 = new Quantity(72.0, LengthUnit.Inches);

        Assert.Multiple(() => {
            Assert.That(q1, Is.EqualTo(q2));
            Assert.That(q2, Is.EqualTo(q3));
            Assert.That(q1, Is.EqualTo(q3));
        });
    }

}