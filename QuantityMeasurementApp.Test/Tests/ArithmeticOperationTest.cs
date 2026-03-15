using System;
using NUnit.Framework;
using QuantityMeasurementModelLayer.Models; 
using QuantityMeasurementModelLayer.Enums;  
using QuantityMeasurementBusinessLayer.Services; 

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class ArithmeticOperationTest
    {
        private const double Epsilon = 1e-6;

        private readonly LengthUnitConverter _lengthConverter = new LengthUnitConverter();
        private readonly VolumeUnitConverter _volumeConverter = new VolumeUnitConverter();
        private readonly WeightUnitConverter _weightConverter = new WeightUnitConverter();

        [Test]
        public void testSubtraction_SameUnit_FeetMinusFeet_ShouldReturnCorrectDifference()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet, _lengthConverter);
            var result = q1.Subtract(q2);
            
            Assert.Multiple(() => {
                Assert.That(result.Value, Is.EqualTo(5.0));
                Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
            });
        }

        [Test]
        public void testSubtraction_CrossUnit_FeetMinusInches_ShouldReturnImplicitFeet()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.Inches, _lengthConverter);
            var result = q1.Subtract(q2);
            
            Assert.That(result.Value, Is.EqualTo(9.5));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_LitreMinusLitre_ToMillilitres()
        {
            var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre, _volumeConverter);
            var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre, _volumeConverter);
            var result = q1.Subtract(q2, VolumeUnit.MilliLiter);
            
            Assert.That(result.Value, Is.EqualTo(3000.0));
            Assert.That(result.Unit, Is.EqualTo(VolumeUnit.MilliLiter));
        }

        [Test]
        public void testSubtraction_ResultingInNegative_ShouldPreserveSign()
        {
            var q1 = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms, _weightConverter);
            var q2 = new Quantity<WeightUnit>(5.0, WeightUnit.Kilograms, _weightConverter);
            var result = q1.Subtract(q2);
            
            Assert.That(result.Value, Is.EqualTo(-3.0));
        }

        [Test]
        public void testSubtraction_NonCommutative_OrderShouldChangeResult()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet, _lengthConverter);
            
            var diff1 = q1.Subtract(q2).Value; 
            var diff2 = q2.Subtract(q1).Value; 
            
            Assert.That(diff1, Is.Not.EqualTo(diff2));
        }

        [Test]
        public void testSubtraction_WithZeroOperand_IdentityProperty()
        {
            var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(0.0, LengthUnit.Inches, _lengthConverter);
            var result = q1.Subtract(q2);
            
            Assert.That(result.Value, Is.EqualTo(5.0));
        }

        [Test]
        public void testDivision_SameUnit_ShouldReturnScalarRatio()
        {
            var q1 = new Quantity<WeightUnit>(10.0, WeightUnit.Kilograms, _weightConverter);
            var q2 = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms, _weightConverter);
            double ratio = q1.Divide(q2);
            
            Assert.That(ratio, Is.EqualTo(5.0));
        }

        [Test]
        public void testDivision_CrossUnit_InchesByFeet_ShouldNormalizeCorrectly()
        {
            var q1 = new Quantity<LengthUnit>(24.0, LengthUnit.Inches, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, _lengthConverter);
            
            Assert.That(q1.Divide(q2), Is.EqualTo(1.0).Within(Epsilon));
        }

        [Test]
        public void testDivision_ByZero_ShouldThrowArithmeticException()
        {
            var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre, _volumeConverter);
            var q2 = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre, _volumeConverter);
            
            Assert.Throws<ArithmeticException>(() => q1.Divide(q2));
        }

        [Test]
        public void testSubtraction_Immutability_OriginalsShouldNotChange()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, _lengthConverter);
            
            q1.Subtract(q2);
            
            Assert.That(q1.Value, Is.EqualTo(10.0)); 
        }

        [Test]
        public void testArithmetic_Integration_A_Add_B_Subtract_B_Equals_A()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, _lengthConverter);
            
            var result = q1.Add(q2).Subtract(q2);
            Assert.That(result.Value, Is.EqualTo(10.0).Within(Epsilon));
        }
    }
}