using NUnit.Framework;
using QuantityMeasurementApp.models;

/// <summary>
/// This test suite validates the Arithmetic Logic for UC6, ensuring cross-unit addition 
/// is physically accurate and result-consistent. It verifies mathematical properties 
/// like commutativity and identity, handles precision across various magnitudes (1e6 to 0.001), 
/// and ensures robust error handling for null operands.
/// </summary>
[TestFixture]
public class QuantityAdditionTest
{
    [Test]
    public void testAddition_SameUnit_FeetPlusFeet()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(2.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(3.0,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_SameUnit_InchPlusInch()
    {
        Quantity q1 = new Quantity(6.0,LengthUnit.Inches);
        Quantity q2 = new Quantity(6.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(12.0,LengthUnit.Inches)));
    }

    [Test]
    public void testAddition_CrossUnit_FeetPlusInches()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(12.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(2.0,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_CrossUnit_InchesPlusFeet()
    {
        Quantity q1 = new Quantity(12.0,LengthUnit.Inches);
        Quantity q2 = new Quantity(1.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(24.0,LengthUnit.Inches)));
    }

    [Test]
    public void testAddition_CrossUnit_YardPlusFeet()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Yards);
        Quantity q2 = new Quantity(3.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(2.0,LengthUnit.Yards)));
    }

    [Test]
    public void testAddition_CrossUnit_CentimeterPlusInches()
    {
        Quantity q1 = new Quantity(2.54,LengthUnit.Centimeters);
        Quantity q2 = new Quantity(1.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(5.08,LengthUnit.Centimeters)));
    }

    [Test]
    public void testAddition_Commutativity()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(12.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2),Is.EqualTo(q2.Add(q1)));
    }

    [Test]
    public void testAddition_WithZero()
    {
        Quantity q1 = new Quantity(5.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(0.0,LengthUnit.Inches);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(5.0,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_NegativeValue()
    {
        Quantity q1 = new Quantity(5.0,LengthUnit.Feet);
        Quantity q2 = new Quantity(-2.0,LengthUnit.Feet);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(3.0,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_NullSecondOperand()
    {
        Quantity q1 = new Quantity(1.0,LengthUnit.Feet);

        Assert.Throws<ArgumentNullException>(()=> q1.Add(null!));
    }

    [Test]
    public void testAddition_LargeValues()
    {
        Quantity q1 = new Quantity(1e6,LengthUnit.Feet);
        Quantity q2 = new Quantity(1e6,LengthUnit.Feet);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(2e6,LengthUnit.Feet)));
    }

    [Test]
    public void testAddition_SmallValues()
    {
        Quantity q1 = new Quantity(0.001,LengthUnit.Feet);
        Quantity q2 = new Quantity(0.002,LengthUnit.Feet);

        Assert.That(q1.Add(q2),Is.EqualTo(new Quantity(0.003,LengthUnit.Feet)));
    }
}