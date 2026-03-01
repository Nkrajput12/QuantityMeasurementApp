using NUnit.Framework;
using QuantityMeasurementApp.models;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class VolumeMeasurementTests
    {
        private const double Epsilon = 1e-6;



        [Test]
        public void testEquality_LitreToLitre_SameValue_ShouldReturnTrue()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.That(q1.Equals(q2), Is.True);
        }

        [Test]
        public void testEquality_LitreToML_EquivalentValue_ShouldReturnTrue()
        {
            // 1 Litre = 1000 ML
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MilliLiter);
            Assert.That(litre.Equals(ml), Is.True);
        }

        [Test]
        public void testEquality_LitreToGallon_EquivalentValue_ShouldReturnTrue()
        {
            // 3.78541 Litres = 1 Gallon
            var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Assert.That(litre.Equals(gallon), Is.True);
        }

        // --- 2. Unit Conversion ---

        [Test]
        public void testConversion_LitreToML_ShouldReturnCorrectValue()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var result = litre.ConvertTo(VolumeUnit.MilliLiter);

            Assert.Multiple(() => {
                Assert.That(result.Value, Is.EqualTo(1000.0).Within(Epsilon));
                Assert.That(result.Unit, Is.EqualTo(VolumeUnit.MilliLiter));
            });
        }

        [Test]
        public void testConversion_GallonToLitre_ShouldReturnCorrectValue()
        {
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            var result = gallon.ConvertTo(VolumeUnit.Litre);
            Assert.That(result.Value, Is.EqualTo(3.78541).Within(Epsilon));
        }

        // --- 3. Addition Operations ---

        [Test]
        public void testAddition_LitreAndML_ShouldReturnSumInLitre()
        {
            // 1.0L + 1000mL = 2.0L
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MilliLiter);
            
            var result = q1.Add(q2); // Implicit target (Litre)
            Assert.That(result.Value, Is.EqualTo(2.0).Within(Epsilon));
        }

        [Test]
        public void testAddition_LitreAndGallon_ExplicitTarget_ShouldReturnSumInGallon()
        {
            // 3.78541L + 1.0 gal = 2.0 gal
            var q1 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            
            var result = q1.Add(q2, VolumeUnit.Gallon);
            Assert.That(result.Value, Is.EqualTo(2.0).Within(Epsilon));
        }


        [Test]
        public void testEquality_VolumeVsLength_ShouldReturnFalse()
        {
            var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            // Verifies that the generic Equals(object) logic correctly returns false
            Assert.That(volume.Equals(length), Is.False);
        }

        [Test]
        public void testEquality_VolumeVsWeight_ShouldReturnFalse()
        {
            var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);
            Assert.That(volume.Equals(weight), Is.False);
        }


        [Test]
        public void testZeroValue_AcrossVolumeUnits_ShouldBeEqual()
        {
            var zeroL = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);
            var zeroGal = new Quantity<VolumeUnit>(0.0, VolumeUnit.Gallon);
            Assert.That(zeroL.Equals(zeroGal), Is.True);
        }

        [Test]
        public void testSymmetricEquality_GallonToLitre_ShouldBeSameBothWays()
        {
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);

            // If A = B, then B = A
            Assert.Multiple(() => {
                Assert.That(gallon.Equals(litre), Is.True, "Gallon to Litre failed");
                Assert.That(litre.Equals(gallon), Is.True, "Litre to Gallon failed");
            });
        }

        [Test]
        public void testTransitiveEquality_GallonToLitreToML()
        {
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            var ml = new Quantity<VolumeUnit>(3785.41, VolumeUnit.MilliLiter);

            Assert.That(gallon.Equals(litre), Is.True);
            Assert.That(litre.Equals(ml), Is.True);
            Assert.That(gallon.Equals(ml), Is.True, "Transitive equality failed across three units");
        }

        
        [Test]
        public void testLargeVolume_ConversionPrecision()
        {
            var largeLitre = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            var result = largeLitre.ConvertTo(VolumeUnit.MilliLiter);

            Assert.That(result.Value, Is.EqualTo(1000000000.0).Within(Epsilon));
        }

        
        [Test]
        public void testSmallVolume_MillLitreToGallon()
        {
            // 1 mL = 0.001 L. 0.001 L / 3.78541 = 0.000264172 Gallons
            var oneMl = new Quantity<VolumeUnit>(1.0, VolumeUnit.MilliLiter);
            var result = oneMl.ConvertTo(VolumeUnit.Gallon);

            Assert.That(result.Value, Is.EqualTo(0.000264172).Within(Epsilon));
        }

        
        [Test]
        public void testNegativeVolume_Addition()
        {
            // 5 Litres + (-2 Litres) = 3 Litres
            var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            var q2 = new Quantity<VolumeUnit>(-2.0, VolumeUnit.Litre);

            var result = q1.Add(q2);
            Assert.That(result.Value, Is.EqualTo(3.0).Within(Epsilon));
        }
    
        [Test]
        public void testAddition_WithZero_ShouldReturnOriginalValue()
        {
            var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.Gallon);
            var zeroMl = new Quantity<VolumeUnit>(0.0, VolumeUnit.MilliLiter);

            var result = q1.Add(zeroMl);
            Assert.That(result.Value, Is.EqualTo(10.0).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Gallon));
        }

    
        [Test]
        public void testRoundTripConversion_LitreToGallonToLitre()
        {
            var original = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);
            var toGallon = original.ConvertTo(VolumeUnit.Gallon);
            var backToLitre = toGallon.ConvertTo(VolumeUnit.Litre);

            Assert.That(backToLitre.Value, Is.EqualTo(10.0).Within(Epsilon));
        }
    }
}