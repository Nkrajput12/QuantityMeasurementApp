using NUnit.Framework;
using QuantityMeasurementApp.models;
using QuantityMeasurementApp.Models;
using System;
using System.Reflection;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class TemperatureTest
    {
        private const double Tolerance = 0.0001; // For floating-point precision

        [Test]
        public void TestTemperatureEquality_CelsiusToCelsius_SameValue()
        {
            var q1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            var q2 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            Assert.That(q1.Equals(q2), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_FahrenheitToFahrenheit_SameValue()
        {
            var q1 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            var q2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            Assert.That(q1.Equals(q2), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            var fahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var fahrenheit = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit);
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
        {
            var celsius = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Celsius);
            var fahrenheit = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Fahrenheit);
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_SymmetricProperty()
        {
            var qA = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            var qB = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            
            Assert.That(qA.Equals(qB), Is.True);
            Assert.That(qB.Equals(qA), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_ReflexiveProperty()
        {
            var q = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius);
            Assert.That(q.Equals(q), Is.True);
        }



        [TestCase(50.0, 122.0)]
        [TestCase(-20.0, -4.0)]
        public void TestTemperatureConversion_CelsiusToFahrenheit_VariousValues(double c, double expectedF)
        {
            var celsius = new Quantity<TemperatureUnit>(c, TemperatureUnit.Celsius);
            var result = celsius.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(expectedF).Within(Tolerance));
        }

        [TestCase(122.0, 50.0)]
        [TestCase(-4.0, -20.0)]
        public void TestTemperatureConversion_FahrenheitToCelsius_VariousValues(double f, double expectedC)
        {
            var fahrenheit = new Quantity<TemperatureUnit>(f, TemperatureUnit.Fahrenheit);
            var result = fahrenheit.ConvertTo(TemperatureUnit.Celsius);
            Assert.That(result.Value, Is.EqualTo(expectedC).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_RoundTrip_PreservesValue()
        {
            double originalValue = 75.5;
            var original = new Quantity<TemperatureUnit>(originalValue, TemperatureUnit.Fahrenheit);
            
            var toCelsius = original.ConvertTo(TemperatureUnit.Celsius);
            var backToFahrenheit = toCelsius.ConvertTo(TemperatureUnit.Fahrenheit);
            
            Assert.That(backToFahrenheit.Value, Is.EqualTo(originalValue).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_SameUnit()
        {
            var q = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius);
            var result = q.ConvertTo(TemperatureUnit.Celsius);
            Assert.That(result.Value, Is.EqualTo(25.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_ZeroValue()
        {
            var zeroC = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            var result = zeroC.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(32.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_NegativeValues()
        {
            var negativeC = new Quantity<TemperatureUnit>(-10.0, TemperatureUnit.Celsius);
            var result = negativeC.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(14.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_LargeValues()
        {
            var largeC = new Quantity<TemperatureUnit>(1000.0, TemperatureUnit.Celsius);
            var result = largeC.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(1832.0).Within(Tolerance));
        }


        [Test]
        public void TestTemperatureOperation_Add_ShouldReturnCorrectSum()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            var result = q1.Add(q2);
            Assert.That(result.Value, Is.EqualTo(150.0));
            Assert.That(result.Unit, Is.EqualTo(TemperatureUnit.Celsius));
        }

        [Test]
        public void TestTemperatureUnsupportedOperation_Subtract()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            var result = q1.Subtract(q2);
            Assert.That(result.Value,Is.EqualTo(50.0));
            Assert.That(result.Unit,Is.EqualTo(TemperatureUnit.Celsius));
        }

        [Test]
        public void TestTemperatureUnsupportedOperation_Divide()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            
            Assert.Throws<InvalidOperationException>(() => q1.Divide(q2));
        }

        [Test]
        public void TestTemperatureUnsupportedOperation_ErrorMessage()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            
            var ex = Assert.Throws<InvalidOperationException>(() => q1.Divide(q2));
            Assert.That(ex.Message, Is.EqualTo("Temperature does not support divide operations."));
        }


        [Test]
        public void TestTemperatureVsLengthIncompatibility()
        {
            var temp = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var length = new Quantity<LengthUnit>(100.0, LengthUnit.Feet);
            Assert.That(temp.Equals(length), Is.False);
        }

        [Test]
        public void TestTemperatureVsWeightIncompatibility()
        {
            var temp = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            var weight = new Quantity<WeightUnit>(50.0, WeightUnit.Kilograms);
            Assert.That(temp.Equals(weight), Is.False);
        }

        [Test]
        public void TestTemperatureVsVolumeIncompatibility()
        {
            var temp = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius);
            var volume = new Quantity<VolumeUnit>(25.0, VolumeUnit.Litre);
            Assert.That(temp.Equals(volume), Is.False);
        }



        [Test]
        public void TestOperationSupportMethods_TemperatureUnitAddition()
        {
            var t1 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Celsius);
            var t2 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Celsius);
            
            var result = t1.Add(t2);
            Assert.That(result.Value, Is.EqualTo(20.0));
        }

        [Test]
        public void TestOperationSupportMethods_TemperatureUnitDivision()
        {
            var t1 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Fahrenheit);
            var t2 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Fahrenheit);
        
            Assert.Throws<InvalidOperationException>(() => t1.Divide(t2));
        }

        [Test]
        public void TestOperationSupportMethods_LengthUnitAddition()
        {
            var l1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            var l2 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.DoesNotThrow(() => l1.Add(l2));
        }

        [Test]
        public void TestOperationSupportMethods_WeightUnitDivision()
        {
            var w1 = new Quantity<WeightUnit>(10.0, WeightUnit.Kilograms);
            var w2 = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms);
            
            Assert.DoesNotThrow(() => w1.Divide(w2));
        }


        [Test]
        public void TestIMeasurableInterface_Evolution_BackwardCompatible()
        {
            var in1 = new Quantity<LengthUnit>(2.0, LengthUnit.Inches);
            var in2 = new Quantity<LengthUnit>(2.0, LengthUnit.Inches);
            
            var result = in1.Add(in2);
            Assert.That(result.Value, Is.EqualTo(4.0));
        }

        [Test]
        public void TestTemperatureUnit_NonLinearConversion()
        {

            double fahrenheitFromZero = TemperatureUnit.Fahrenheit.ConvertFromBase(0.0);
            Assert.That(fahrenheitFromZero, Is.EqualTo(32.0));
        }

        [Test]
        public void TestTemperatureUnit_NameMethod()
        {

            Assert.That(TemperatureUnit.Celsius.ToString(), Is.EqualTo("Celsius"));
            Assert.That(TemperatureUnit.Celsius.GetSymbol(), Is.EqualTo("°C"));
            Assert.That(TemperatureUnit.Fahrenheit.GetSymbol(), Is.EqualTo("°F"));
        }

        [Test]
        public void TestTemperatureUnit_ConversionFactor()
        {
            double baseCelsius = TemperatureUnit.Celsius.ConvertToBase(1.0);
            Assert.That(baseCelsius, Is.EqualTo(1.0)); // Celsius is the base
        }
    }
}