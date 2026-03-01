// using NUnit.Framework;
// using QuantityMeasurementApp.models;


// /// <summary>
// /// These test verify the Conversion Logic
// /// </summary>
// [TestFixture]
// public class QuantityConversionTest
// {

//     //Convert Feet to inches
//     //Logic convert 1.0 ft to 12.0 inches
//     [Test]
//     public void testConversion_feetToInches_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(1.0,LengthUnit.Feet,LengthUnit.Inches);

//         Assert.That(val,Is.EqualTo(12.0));
//     }

//     //convert inches to feet
//     //Logic convert 24.0 in to 2 ft
//     [Test]
//     public void testConversion_inchesToFeet__ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(24.0,LengthUnit.Inches,LengthUnit.Feet);
//         Assert.That(val,Is.EqualTo(2.0));
//     }

//     //convert Yards to inches
//     //Logic convert 1.0 yd  to 36.0 in
//     [Test]
//     public void testConversion_YardsToInches_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(1.0,LengthUnit.Yards,LengthUnit.Inches);
//         Assert.That(val,Is.EqualTo(36.0));

//     }

//     //Convert Inches to Yards
//     //Logic convert 72.0 inches to 2.0yd
//     [Test]
//     public void testConversion_InchesToYards_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(72.0,LengthUnit.Inches,LengthUnit.Yards);

//         Assert.That(val,Is.EqualTo(2.0));
//     }

//     //convert cm to inches
//     //logic convert 2.54 cm to approx 1.0 inches within(0.001) to handle floating point
//     [Test]
//     public void testConversion_CentimeterToInches_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(2.54,LengthUnit.Centimeters,LengthUnit.Inches);
//         Assert.That(val,Is.EqualTo(1.0).Within(0.001));
//     }

//     //Convert Feet to Yards
//     //Logic convert 6.0 ft = 2.0 yd
//     [Test]
//     public void testConversion_FeetToYard_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(6.0,LengthUnit.Feet,LengthUnit.Yards);
//         Assert.That(val,Is.EqualTo(2.0));
//     }

//     //check for zero value
//     //logic convert 0.0 ft to 0.0 in
//     [Test]
//     public void testConversion_ZeroValue_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(0.0,LengthUnit.Feet,LengthUnit.Inches);
//         Assert.That(val,Is.EqualTo(0.0));
//     }
    
//     //convert neg feet to neg inches
//     //logic convert -1.0 ft to -12.0 inches
//     [Test]
//     public void testConversion_NegativeValue_ReturnEqual()
//     {
//         double val = Quantity.ConvertValue(-1.0,LengthUnit.Feet,LengthUnit.Inches);
//         Assert.That(val,Is.EqualTo(-12.0));
//     }

//     //Convert A -> B then B -> A
//     //then verify Original value Of a must we equal to Roundtrip value of A
//     [Test]
//     public void testConversion_RoundTrip_PreserveValue()
//     {
//         double originalValue = 100.0;
//         LengthUnit u1 = LengthUnit.Yards;
//         LengthUnit u2 = LengthUnit.Inches;

//         double convertedValue = Quantity.ConvertValue(originalValue, u1,u2);
//         double RoundTripValue = Quantity.ConvertValue(convertedValue, u2,u1);

//         Assert.That(RoundTripValue,Is.EqualTo(originalValue));
//     }
// }