using System;
using NUnit.Framework;
using QuantityMeasurementApp.models;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{

    /// <summary>
    /// These test cases use to verify the generic Quantity class
    /// working
    /// </summary>
    [TestFixture]
    public class GenericQuantityTests
    {

        [Test]
        public void testGenericQuantity_LengthEquality_ShouldReturnTrueForEquivalentUnits()
        {

            var oneFoot = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var twelveInches = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            Assert.That(oneFoot.Equals(twelveInches), Is.True);
        }

        [Test]
        public void testGenericQuantity_WeightEquality_ShouldReturnTrueForEquivalentUnits()
        {
            // UC4: 1 Kilogram = 1000 Grams
            var oneKg = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);
            var thousandGrams = new Quantity<WeightUnit>(1000.0, WeightUnit.Grams);

            Assert.That(oneKg.Equals(thousandGrams), Is.True);
        }

        // --- UC5 & UC10: Generic Conversion ---

        [Test]
        public void testGenericQuantity_LengthConversion_FeetToInches()
        {
            var feet = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var result = feet.ConvertTo(LengthUnit.Inches);

            Assert.Multiple(() =>
            {
                Assert.That(result.Value, Is.EqualTo(12.0).Within(1e-6));
                Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inches));
            });
        }

        [Test]
        public void testGenericQuantity_WeightConversion_KgToGrams()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);
            var result = kg.ConvertTo(WeightUnit.Grams);

            Assert.That(result.Value, Is.EqualTo(1000.0).Within(1e-6));
        }

        // --- UC6 & UC10: Generic Addition ---

        [Test]
        public void testGenericQuantity_LengthAddition_FeetAndInches()
        {
            // 1 Feet + 12 Inches = 2 Feet
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = q1.Add(q2, LengthUnit.Feet);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(1e-6));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
        }

        [Test]
        public void testGenericQuantity_WeightAddition_GramsAndKg()
        {
            // 1000 Grams + 1 Kilogram = 2 Kilograms
            var q1 = new Quantity<WeightUnit>(1000.0, WeightUnit.Grams);
            var q2 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);

            var result = q1.Add(q2, WeightUnit.Kilograms);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(1e-6));
        }

        // --- Safety & Validation ---

        [Test]
        public void testCrossCategoryPrevention_LengthVsWeight_ShouldReturnFalse()
        {
            var distance = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);
            Assert.That(distance.Equals(weight), Is.False);
        }

        [Test]
        public void testGenericQuantity_Constructor_ShouldThrowOnInvalidValue()
        {
            Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet));
            Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.PositiveInfinity, LengthUnit.Inches));
        }

        // --- Formatting ---

        [Test]
        public void testToString_ShouldFormatCorrectSymbol()
        {
            var q = new Quantity<LengthUnit>(5.0, LengthUnit.Inches);
            // This test verifies that the dynamic/switch logic in ToString works
            Assert.That(q.ToString(), Does.Contain("5") & Does.Contain("in"));
        }

        [Test]
        public void testGenericQuantity_ZeroValue_ShouldBeEqualAcrossUnits()
        {
            // 0 Feet should equal 0 Inches
            var zeroFeet = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            var zeroInches = new Quantity<LengthUnit>(0.0, LengthUnit.Inches);

            Assert.That(zeroFeet.Equals(zeroInches), Is.True);
        }

        // --- Large Value Handling ---

        [Test]
        public void testGenericQuantity_LargeValue_AdditionAccuracy()
        {
            // Testing with 1 million units to ensure no overflow/precision loss in base conversion
            var largeKm = new Quantity<WeightUnit>(1000000.0, WeightUnit.Kilograms);
            var oneGram = new Quantity<WeightUnit>(1.0, WeightUnit.Grams);

            var result = largeKm.Add(oneGram, WeightUnit.Grams);

            // Expected: 1,000,000,000 + 1 grams
            Assert.That(result.Value, Is.EqualTo(1000000001.0).Within(1e-6));
        }

        // --- Floating Point Precision (Epsilon) ---

        [Test]
        public void testGenericQuantity_Precision_ShouldHandleSmallDifferences()
        {
            // Values differing by less than 1e-6 should be considered equal
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0000001, LengthUnit.Inches);

            Assert.That(q1.Equals(q2), Is.True, "Should be equal within Epsilon tolerance");
        }

        // --- Negative Value Handling ---

        [Test]
        public void testGenericQuantity_NegativeValues_Addition()
        {
            // 10 Inches + (-5 Inches) = 5 Inches
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(-5.0, LengthUnit.Inches);

            var result = q1.Add(q2);

            Assert.That(result.Value, Is.EqualTo(5.0).Within(1e-6));
        }

        // --- Reflexive Equality ---

        [Test]
        public void testGenericQuantity_Equality_ReflexiveProperty()
        {
            var q1 = new Quantity<WeightUnit>(500.0, WeightUnit.Grams);
            
            // An object must always equal itself
            Assert.That(q1.Equals(q1), Is.True);
        }

        // --- Null Comparison ---

        [Test]
        public void testGenericQuantity_Equality_WithNull_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            
            // Comparing with null should never throw an exception, just return false
            Assert.That(q1.Equals(null), Is.False);
        }

        // --- HashCode Consistency ---

        [Test]
        public void testGenericQuantity_GetHashCode_Consistency()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            // Objects that are Equal must have the same HashCode
            Assert.That(q1.GetHashCode(), Is.EqualTo(q2.GetHashCode()));
        }
    }
}