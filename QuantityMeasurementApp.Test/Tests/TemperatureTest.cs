using System;
using NUnit.Framework;
using QuantityMeasurementModelLayer.Models;      // Fixed namespace
using QuantityMeasurementModelLayer.Enums;       // Added for Enums
using QuantityMeasurementBusinessLayer.Services; // Added for Converters

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class TemperatureTest
    {
        private const double Tolerance = 0.0001; // For floating-point precision

        // Reusable converter instances for the tests
        private readonly TemperatureUnitConverter _temperatureConverter = new TemperatureUnitConverter();
        private readonly LengthUnitConverter _lengthConverter = new LengthUnitConverter();
        private readonly WeightUnitConverter _weightConverter = new WeightUnitConverter();
        private readonly VolumeUnitConverter _volumeConverter = new VolumeUnitConverter();

        [Test]
        public void TestTemperatureEquality_CelsiusToCelsius_SameValue()
        {
            var q1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, _temperatureConverter);
            var q2 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, _temperatureConverter);
            Assert.That(q1.Equals(q2), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_FahrenheitToFahrenheit_SameValue()
        {
            var q1 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            var q2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            Assert.That(q1.Equals(q2), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, _temperatureConverter);
            var fahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, _temperatureConverter);
            var fahrenheit = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
        {
            var celsius = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Celsius, _temperatureConverter);
            var fahrenheit = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_SymmetricProperty()
        {
            var qA = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, _temperatureConverter);
            var qB = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            
            Assert.That(qA.Equals(qB), Is.True);
            Assert.That(qB.Equals(qA), Is.True);
        }

        [Test]
        public void TestTemperatureEquality_ReflexiveProperty()
        {
            var q = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius, _temperatureConverter);
            Assert.That(q.Equals(q), Is.True);
        }

        [TestCase(50.0, 122.0)]
        [TestCase(-20.0, -4.0)]
        public void TestTemperatureConversion_CelsiusToFahrenheit_VariousValues(double c, double expectedF)
        {
            var celsius = new Quantity<TemperatureUnit>(c, TemperatureUnit.Celsius, _temperatureConverter);
            var result = celsius.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(expectedF).Within(Tolerance));
        }

        [TestCase(122.0, 50.0)]
        [TestCase(-4.0, -20.0)]
        public void TestTemperatureConversion_FahrenheitToCelsius_VariousValues(double f, double expectedC)
        {
            var fahrenheit = new Quantity<TemperatureUnit>(f, TemperatureUnit.Fahrenheit, _temperatureConverter);
            var result = fahrenheit.ConvertTo(TemperatureUnit.Celsius);
            Assert.That(result.Value, Is.EqualTo(expectedC).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_RoundTrip_PreservesValue()
        {
            double originalValue = 75.5;
            var original = new Quantity<TemperatureUnit>(originalValue, TemperatureUnit.Fahrenheit, _temperatureConverter);
            
            var toCelsius = original.ConvertTo(TemperatureUnit.Celsius);
            var backToFahrenheit = toCelsius.ConvertTo(TemperatureUnit.Fahrenheit);
            
            Assert.That(backToFahrenheit.Value, Is.EqualTo(originalValue).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_SameUnit()
        {
            var q = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius, _temperatureConverter);
            var result = q.ConvertTo(TemperatureUnit.Celsius);
            Assert.That(result.Value, Is.EqualTo(25.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_ZeroValue()
        {
            var zeroC = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, _temperatureConverter);
            var result = zeroC.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(32.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_NegativeValues()
        {
            var negativeC = new Quantity<TemperatureUnit>(-10.0, TemperatureUnit.Celsius, _temperatureConverter);
            var result = negativeC.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(14.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureConversion_LargeValues()
        {
            var largeC = new Quantity<TemperatureUnit>(1000.0, TemperatureUnit.Celsius, _temperatureConverter);
            var result = largeC.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.That(result.Value, Is.EqualTo(1832.0).Within(Tolerance));
        }

        [Test]
        public void TestTemperatureOperation_Add_ShouldReturnCorrectSum()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, _temperatureConverter);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, _temperatureConverter);
            var result = q1.Add(q2);
            Assert.That(result.Value, Is.EqualTo(150.0));
            Assert.That(result.Unit, Is.EqualTo(TemperatureUnit.Celsius));
        }

        [Test]
        public void TestTemperatureUnsupportedOperation_Subtract()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, _temperatureConverter);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, _temperatureConverter);
            var result = q1.Subtract(q2);
            Assert.That(result.Value,Is.EqualTo(50.0));
            Assert.That(result.Unit,Is.EqualTo(TemperatureUnit.Celsius));
        }

        [Test]
        public void TestTemperatureUnsupportedOperation_Divide()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, _temperatureConverter);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, _temperatureConverter);
            
            Assert.Throws<InvalidOperationException>(() => q1.Divide(q2));
        }

        [Test]
        public void TestTemperatureUnsupportedOperation_ErrorMessage()
        {
            var q1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, _temperatureConverter);
            var q2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, _temperatureConverter);
            
            var ex = Assert.Throws<InvalidOperationException>(() => q1.Divide(q2));
            Assert.That(ex.Message, Is.EqualTo("Temperature does not support divide operations."));
        }

        [Test]
        public void TestTemperatureVsLengthIncompatibility()
        {
            var temp = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, _temperatureConverter);
            var length = new Quantity<LengthUnit>(100.0, LengthUnit.Feet, _lengthConverter);
            Assert.That(temp.Equals(length), Is.False);
        }

        [Test]
        public void TestTemperatureVsWeightIncompatibility()
        {
            var temp = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, _temperatureConverter);
            var weight = new Quantity<WeightUnit>(50.0, WeightUnit.Kilograms, _weightConverter);
            Assert.That(temp.Equals(weight), Is.False);
        }

        [Test]
        public void TestTemperatureVsVolumeIncompatibility()
        {
            var temp = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius, _temperatureConverter);
            var volume = new Quantity<VolumeUnit>(25.0, VolumeUnit.Litre, _volumeConverter);
            Assert.That(temp.Equals(volume), Is.False);
        }

        [Test]
        public void TestOperationSupportMethods_TemperatureUnitAddition()
        {
            var t1 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Celsius, _temperatureConverter);
            var t2 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Celsius, _temperatureConverter);
            
            var result = t1.Add(t2);
            Assert.That(result.Value, Is.EqualTo(20.0));
        }

        [Test]
        public void TestOperationSupportMethods_TemperatureUnitDivision()
        {
            var t1 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
            var t2 = new Quantity<TemperatureUnit>(10.0, TemperatureUnit.Fahrenheit, _temperatureConverter);
        
            Assert.Throws<InvalidOperationException>(() => t1.Divide(t2));
        }

        [Test]
        public void TestOperationSupportMethods_LengthUnitAddition()
        {
            var l1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            var l2 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            Assert.DoesNotThrow(() => l1.Add(l2));
        }

        [Test]
        public void TestOperationSupportMethods_WeightUnitDivision()
        {
            var w1 = new Quantity<WeightUnit>(10.0, WeightUnit.Kilograms, _weightConverter);
            var w2 = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms, _weightConverter);
            
            Assert.DoesNotThrow(() => w1.Divide(w2));
        }

        [Test]
        public void TestIMeasurableInterface_Evolution_BackwardCompatible()
        {
            var in1 = new Quantity<LengthUnit>(2.0, LengthUnit.Inches, _lengthConverter);
            var in2 = new Quantity<LengthUnit>(2.0, LengthUnit.Inches, _lengthConverter);
            
            var result = in1.Add(in2);
            Assert.That(result.Value, Is.EqualTo(4.0));
        }

        [Test]
        public void TestTemperatureUnit_NonLinearConversion()
        {
            // Create a base temperature of 0 Celsius
            var baseTemp = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, _temperatureConverter);
            
            // Convert it to Fahrenheit
            var fahrenheit = baseTemp.ConvertTo(TemperatureUnit.Fahrenheit);
            
            // Check the double Value
            Assert.That(fahrenheit.Value, Is.EqualTo(32.0));
        }

        [Test]
        public void TestTemperatureUnit_ConversionFactor()
        {
            // Create 1 Celsius
            var celsius = new Quantity<TemperatureUnit>(1.0, TemperatureUnit.Celsius, _temperatureConverter);
            
            // Convert to base (which is just Celsius)
            var baseCelsius = celsius.ConvertTo(TemperatureUnit.Celsius);
            
            Assert.That(baseCelsius.Value, Is.EqualTo(1.0)); // Celsius is the base
        }
    }
}