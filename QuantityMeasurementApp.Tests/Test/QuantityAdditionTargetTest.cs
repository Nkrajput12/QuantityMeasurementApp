using NUnit.Framework;
using NUnit.Framework.Interfaces;
using QuantityMeasurementApp.models;


/// <summary>
/// UC7: Addition with Explicit Target Unit.
/// This suite validates the ability to add two quantities and return
/// the result in a specific target unit chosen by the caller.
/// </summary>
[TestFixture]
public class QuantityAdditionTargetTest
{
    [Test]
    public void testAddition_ExplicitTargetUnit_Feet()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(12.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2,LengthUnit.Feet),Is.EqualTo(new Quantity(2.0,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_Inches()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(12.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2,LengthUnit.Inches),Is.EqualTo(new Quantity(24.0,LengthUnit.Inches)));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_Yards()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(12.0,LengthUnit.Inches);

        Quantity result = q1.Add(q2,LengthUnit.Yards);

        Assert.That(result.value,Is.EqualTo(0.667).Within(0.001));
        Assert.That(result.unit,Is.EqualTo(LengthUnit.Yards));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_Centimeters()
    {
        Quantity q1  = new Quantity(1.0,LengthUnit.Inches);
        Quantity q2 = new Quantity(1.0,LengthUnit.Inches);

        Quantity result = q1.Add(q2,LengthUnit.Centimeters);

        Assert.That(result.value,Is.EqualTo(5.08).Within(0.001));
        Assert.That(result.unit,Is.EqualTo(LengthUnit.Centimeters));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_SameAsFirstOperand()
    {
        Quantity q1 = new Quantity(2.0,LengthUnit.Yards);
        Quantity q2 = new Quantity(3.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2,LengthUnit.Yards),Is.EqualTo(new Quantity(3.0,LengthUnit.Yards)));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_SameAsSecondOperand()
    {
        Quantity q1 = new Quantity(2.0,LengthUnit.Yards);
        Quantity q2 = new Quantity(3.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2,LengthUnit.Feet),Is.EqualTo(new Quantity(9.0,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_Commutativity()
    {
        Quantity q1 = new Quantity(2.0,LengthUnit.Yards);
        Quantity q2 = new Quantity(3.0,LengthUnit.Feet);

        Quantity result1 = q1.Add(q2,LengthUnit.Yards);
        Quantity result2 = q2.Add(q1,LengthUnit.Yards);

        Assert.That(result1.value,Is.EqualTo(result2.value));
        Assert.That(result1.unit,Is.EqualTo(result2.unit));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_WithZero()
    {
        Quantity q1 = new Quantity(5.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(0.0,LengthUnit.Inches);

        Quantity result = q1.Add(q2,LengthUnit.Yards);

        Assert.That(result.value,Is.EqualTo(1.667).Within(0.001));
        Assert.That(result.unit,Is.EqualTo(LengthUnit.Yards));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_NegativeValues()
    {
        Quantity q1 = new Quantity(5.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(-2.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2,LengthUnit.Inches),Is.EqualTo(new Quantity(36.0,LengthUnit.Inches)));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_LargeToSmallScale()
    {
        Quantity q1 = new Quantity(1000.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(500.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2,LengthUnit.Inches),Is.EqualTo(new Quantity(18000.0,LengthUnit.Inches)));
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_SmallToLargeScale()
    {
        Quantity q1 = new Quantity(12.0,LengthUnit.Inches);
        Quantity q2 = new Quantity(12.0,LengthUnit.Inches);

        Quantity result = q1.Add(q2,LengthUnit.Yards);

        Assert.That(result.value,Is.EqualTo(0.667).Within(0.001));
        Assert.That(result.unit,Is.EqualTo(LengthUnit.Yards));
    }
    
    //Index 0:Inches 1:Feet 2:yards 3:Centimeter

    //a: val1, b: unit1, c: val2, d: unit2, e: target unit, f: expected value
    [TestCase(1.0, 1, 12.0, 0, 1, 2.0)]            // Same category
    [TestCase(1.0, 1, 12.0, 0, 0, 24.0)]        // Target is smaller
    [TestCase(1.0, 1, 12.0, 0, 2, 0.667)]        // Target is larger
    [TestCase(1.0, 0, 2.54, 3, 0, 2.0)]  // Cross-system (Metric to Imperial)
    [TestCase(1.0, 0, 1.0, 0, 3, 5.08)]  // Imperial to Metric Target
    [TestCase(1.0, 2, 3.0, 1, 2, 2.0)]            // Large scale units
    public void testAddition_AllUnitCombinations(double val1, int unit1, double val2, int unit2, int target, double expected)
    {
        // Arrange
        LengthUnit u1 = (LengthUnit)(unit1);
        LengthUnit u2 = (LengthUnit)(unit2);
        LengthUnit targetUnit = (LengthUnit)(target);
        
        Quantity q1 = new Quantity(val1, u1);
        Quantity q2 = new Quantity(val2, u2);

        // Act
        Quantity result = q1.Add(q2, targetUnit);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.unit, Is.EqualTo(targetUnit));
            Assert.That(result.value, Is.EqualTo(expected).Within(0.001));
        });
    }

    [Test]
    public void testAddition_ExplicitTargetUnit_PrecisionTolerance()
    {
        Quantity q1 = new Quantity(30.48,LengthUnit.Centimeters);
        Quantity q2 = new Quantity(30.48,LengthUnit.Centimeters);

        Quantity result1 = q1.Add(q2,LengthUnit.Feet);

        Assert.That(result1.value,Is.EqualTo(2.0).Within(0.001));

    }
}