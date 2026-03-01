// using NUnit.Framework;
// using QuantityMeasurementApp.models;

// /// <summary>
// /// These test cases verify that Enum is Standalone
// /// with conversion Responsibility
// /// </summary>
// [TestFixture]
// public class QuantityStandalone
// {
//     [Test]
//     public void testLengthUnitEnum_FeetConstant()
//     {
//         double factor = LengthUnit.Feet.GetConversionFactor();
        
//         Assert.That(factor,Is.EqualTo(12.0));
//     }

//     [Test]
//     public void testLengthUnitEnum_InchesConstant()
//     {
//         double factor = LengthUnit.Inches.GetConversionFactor();

//         Assert.That(factor,Is.EqualTo(1.0));
//     }

//     [Test]
//     public void testLengthUnitEnum_YardsConstant()
//     {
//         double factor = LengthUnit.Yards.GetConversionFactor();
//         Assert.That(factor,Is.EqualTo(36.0));
//     }

//     [Test]
//     public void testLengthUnitEnum_CentimetersConstant()
//     {
//         double factor = LengthUnit.Centimeters.GetConversionFactor();
//         Assert.That(factor,Is.EqualTo(0.393701));
//     }

//     [Test]
//     public void testConvertToBaseUnit_InchesToInches()
//     {
//         double Length = LengthUnit.Inches.ConvertToBase(5.0);
//         Assert.That(Length,Is.EqualTo(5.0));
//     }

//     [Test]
//     public void testConvertToBaseUnit_FeetToInches()
//     {
//         double Length = LengthUnit.Feet.ConvertToBase(1.0);
//         Assert.That(Length,Is.EqualTo(12.0));
//     }

//     [Test]
//     public void testConvertToBaseUnit_YardsToInches()
//     {
//         double Length = LengthUnit.Yards.ConvertToBase(1.0);
//         Assert.That(Length,Is.EqualTo(36.0));
//     }

//     [Test]
//     public void testConvertToBaseUnit_CentimeterToInches()
//     {
//         double Length = LengthUnit.Centimeters.ConvertToBase(2.54);
//         Assert.That(Length,Is.EqualTo(1.0).Within(0.01));
//     }

//     [Test]
//     public void testConvertBaseUnitToBaseUnit_InchesToInches()
//     {
//         double Length = LengthUnit.Inches.ConvertToBase(1.0);
//         Assert.That(Length,Is.EqualTo(1.0));
//     }

//     [Test]
//     public void testConvertFromBaseUnit_InchesToFeet()
//     {
//         double Length = LengthUnit.Feet.ConvertFromBase(12.0);

//         Assert.That(Length,Is.EqualTo(1.0));
//     }

//     [Test]
//     public void testConvertFromBaseUnit_InchesToYards()
//     {
//         double Length = LengthUnit.Yards.ConvertFromBase(36.0);

//         Assert.That(Length,Is.EqualTo(1.0));
//     }

//     [Test]
//     public void testConvertFromBaseUnit_InchesToCentimeter()
//     {
//         double Length = LengthUnit.Centimeters.ConvertFromBase(1.0);

//         Assert.That(Length,Is.EqualTo(2.54).Within(0.001));
//     }

//     [Test]
//     public void testQuantityLengthRefactored_Equality()
//     {
//         Assert.That(new Quantity(1.0, LengthUnit.Feet),Is.EqualTo( new Quantity(12.0,LengthUnit.Inches)));
//     }

//     [Test]
//     public void testQuantityLengthRefactored_ConvertTo()
//     {
//         Quantity q1 = new Quantity(1.0,LengthUnit.Feet);
//         Quantity q2 = q1.ConvertTo(LengthUnit.Inches);

//         Assert.That(q2,Is.EqualTo(new Quantity(12.0,LengthUnit.Inches)));
//     }

// }