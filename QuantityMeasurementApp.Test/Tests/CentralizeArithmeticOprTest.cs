using System;
using NUnit.Framework;
using QuantityMeasurementModelLayer.Models; 
using QuantityMeasurementModelLayer.Enums;  
using QuantityMeasurementBusinessLayer.Services; 

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class CentralizeArithmeticOprTest
    {
        
        private readonly LengthUnitConverter _lengthConverter = new LengthUnitConverter();
        private readonly VolumeUnitConverter _volumeConverter = new VolumeUnitConverter();
        private readonly WeightUnitConverter _weightConverter = new WeightUnitConverter();

        [Test]
        public void TestRefactoring_Add_DelegatesViaHelper()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);   
            var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.Inches, _lengthConverter); // 6 inches
            var result = q1.Add(q2, LengthUnit.Inches);

            Assert.That(result.Value, Is.EqualTo(18.0).Within(1e-6));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inches));
        }

        [Test]
        public void TestRefactoring_Subtract_DelegatesViaHelper()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);   // 12 inches
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Inches, _lengthConverter); // 2 inches

            var result = q1.Subtract(q2, LengthUnit.Inches);

            Assert.That(result.Value, Is.EqualTo(10.0).Within(1e-6));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inches));
        }

        [Test]
        public void TestRefactoring_Divide_DelegatesViaHelper()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);   // 12 inches
            var q2 = new Quantity<LengthUnit>(4.0, LengthUnit.Inches, _lengthConverter); // 4 inches

            double ratio = q1.Divide(q2);

            Assert.That(ratio, Is.EqualTo(3.0).Within(1e-6));
        }

        [Test]
        public void TestArithmeticOperation_Add_EnumComputation()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Inches, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Inches, _lengthConverter);

            var result = q1.Add(q2);

            Assert.That(result.Value, Is.EqualTo(15.0).Within(1e-6));
        }

        [Test]
        public void TestArithmeticOperation_Subtract_EnumComputation()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Inches, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Inches, _lengthConverter);

            var result = q1.Subtract(q2);

            Assert.That(result.Value, Is.EqualTo(5.0).Within(1e-6));
        }

        [Test]
        public void TestArithmeticOperation_Divide_EnumComputation()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Inches, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Inches, _lengthConverter);

            double result = q1.Divide(q2);

            Assert.That(result, Is.EqualTo(2.0).Within(1e-6));
        }

        [Test]
        public void TestArithmeticOperation_DivideByZero_EnumThrows()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Inches, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(0, LengthUnit.Inches, _lengthConverter);

            Assert.That(() => q1.Divide(q2), Throws.TypeOf<ArithmeticException>()
                .With.Message.EqualTo("Cannot divide by zero."));
        }

        [Test]
        public void TestImmutability_AfterDivide_ViaCentralizedHelper()
        {
            var q1 = new Quantity<WeightUnit>(20, WeightUnit.Kilograms, _weightConverter);
            var q2 = new Quantity<WeightUnit>(2, WeightUnit.Kilograms, _weightConverter);

            q1.Divide(q2);

            Assert.That(q1.Value, Is.EqualTo(20.0));
        }

        [Test]
        public void TestPerformBaseArithmetic_ConversionAndOperation()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches, _lengthConverter);

            var result = q1.Add(q2, LengthUnit.Inches);

            Assert.That(result.Value, Is.EqualTo(13.0).Within(1e-6));
        }
        
        [Test]
        public void TestAdd_UC12_BehaviorPreserved()
        {
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon, _volumeConverter);
            var litre = new Quantity<VolumeUnit>(3.78, VolumeUnit.Litre, _volumeConverter);

            var result = gallon.Add(litre, VolumeUnit.Litre);

            Assert.That(result.Value, Is.EqualTo(7.56).Within(0.01));
        }

        [Test]
        public void TestSubtract_UC12_BehaviorPreserved()
        {
            var q1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms, _weightConverter);
            var q2 = new Quantity<WeightUnit>(500, WeightUnit.Grams, _weightConverter);

            var result = q1.Subtract(q2, WeightUnit.Kilograms);

            Assert.That(result.Value, Is.EqualTo(0.5).Within(1e-6));
        }

        [Test]
        public void TestDivide_UC12_BehaviorPreserved()
        {
            var q1 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);

            double ratio = q1.Divide(q2);

            Assert.That(ratio, Is.EqualTo(2.0).Within(1e-6));
        }

        [Test]
        public void TestRounding_Divide_NoRounding()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Inches, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(3.0, LengthUnit.Inches, _lengthConverter);

            double result = q1.Divide(q2);

            Assert.That(result, Is.Not.EqualTo(3.33));
            Assert.That(result, Is.EqualTo(3.333333).Within(1e-6));
        }

        [Test]
        public void TestImplicitTargetUnit_AddSubtract()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches, _lengthConverter);

            var resultAdd = q1.Add(q2);
            var resultSub = q1.Subtract(q2);

            Assert.That(resultAdd.Unit, Is.EqualTo(LengthUnit.Feet));
            Assert.That(resultAdd.Value, Is.EqualTo(2.0).Within(1e-6));

            Assert.That(resultSub.Unit, Is.EqualTo(LengthUnit.Feet));
            Assert.That(resultSub.Value, Is.EqualTo(0.0).Within(1e-6));
        }

        [Test]
        public void TestExplicitTargetUnit_AddSubtract_Overrides()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, _lengthConverter);

            var result = q1.Add(q2, LengthUnit.Inches);

            Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inches));
            Assert.That(result.Value, Is.EqualTo(24.0).Within(1e-6));
        }

        [Test]
        public void TestImmutability_AfterAdd_ViaCentralizedHelper()
        {
            var q1 = new Quantity<WeightUnit>(10.0, WeightUnit.Kilograms, _weightConverter);
            var q2 = new Quantity<WeightUnit>(5.0, WeightUnit.Kilograms, _weightConverter);

            q1.Add(q2);

            Assert.That(q1.Value, Is.EqualTo(10.0), "Original quantity was modified!");
            Assert.That(q1.Unit, Is.EqualTo(WeightUnit.Kilograms));
            
            Assert.That(q2.Value, Is.EqualTo(5.0), "Operand quantity was modified!");
        }
    }
}