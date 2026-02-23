using NUnit.Framework;
using QuantityMeasurementApp.models;
namespace QuantityMeasurementApp.Test
{

    /// <summary>
    /// These Test cases verify the conversion, comparison
    /// And Addition cases in Weight Quantity
    /// </summary>
    [TestFixture]
    public class QuantityWeightTests
    {
        private const double Epsilon = 0.001;

        // --- EQUALITY & COMPARISON ---

        [Test]
        public void testEquality_KilogramToKilogram_SameValue()
        {
            QuantityWeight w1 = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight w2 = new QuantityWeight(1.0, WeightUnit.Kilograms);
            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void testEquality_KilogramToKilogram_DifferentValue()
        {
            QuantityWeight w1 = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight w2 = new QuantityWeight(2.0, WeightUnit.Kilograms);
            Assert.That(w1.Equals(w2), Is.False);
        }

        [Test]
        public void testEquality_KilogramToGram_EquivalentValue()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight gram = new QuantityWeight(1000.0, WeightUnit.Grams);
            Assert.That(kg.Equals(gram), Is.True);
        }

        [Test]
        public void testEquality_GramToKilogram_EquivalentValue()
        {
            QuantityWeight gram = new QuantityWeight(1000.0, WeightUnit.Grams);
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            Assert.That(gram.Equals(kg), Is.True);
        }

        [Test]
        public void testEquality_WeightVsLength_Incompatible()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            Quantity feet = new Quantity(1.0, LengthUnit.Feet);
            
            // Should return false due to different class types (Category Type Safety)
            Assert.That(kg.Equals(feet), Is.False);
        }

        [Test]
        public void testEquality_NullComparison()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            Assert.That(kg.Equals(null), Is.False);
        }

        [Test]
        public void testEquality_SameReference()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            Assert.That(kg.Equals(kg), Is.True);
        }

        [Test]
        public void testEquality_ZeroValue()
        {
            QuantityWeight z1 = new QuantityWeight(0.0, WeightUnit.Kilograms);
            QuantityWeight z2 = new QuantityWeight(0.0, WeightUnit.Grams);
            Assert.That(z1.Equals(z2), Is.True);
        }

        [Test]
        public void testEquality_NegativeWeight()
        {
            QuantityWeight w1 = new QuantityWeight(-1.0, WeightUnit.Kilograms);
            QuantityWeight w2 = new QuantityWeight(-1000.0, WeightUnit.Grams);
            Assert.That(w1.Equals(w2), Is.True);
        }



        [Test]
        public void testConversion_PoundToKilogram()
        {
            QuantityWeight pound = new QuantityWeight(2.20462, WeightUnit.Pound);
            QuantityWeight result = pound.ConvertTo(WeightUnit.Kilograms);
            Assert.That(result.value, Is.EqualTo(1.0).Within(0.001));
        }

        [Test]
        public void testConversion_KilogramToPound()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight result = kg.ConvertTo(WeightUnit.Pound);
            Assert.That(result.value, Is.EqualTo(2.20462).Within(0.001));
        }

        [Test]
        public void testConversion_SameUnit()
        {
            QuantityWeight kg = new QuantityWeight(5.0, WeightUnit.Kilograms);
            QuantityWeight result = kg.ConvertTo(WeightUnit.Kilograms);
            Assert.That(result.value, Is.EqualTo(5.0));
            Assert.That(result.unit, Is.EqualTo(WeightUnit.Kilograms));
        }

        [Test]
        public void testConversion_RoundTrip()
        {
            QuantityWeight original = new QuantityWeight(1.5, WeightUnit.Kilograms);
            QuantityWeight result = original.ConvertTo(WeightUnit.Grams).ConvertTo(WeightUnit.Kilograms);
            Assert.That(result.value, Is.EqualTo(1.5).Within(0.001));
        }

        // --- ARITHMETIC (ADDITION) ---

        [Test]
        public void testAddition_SameUnit_KilogramPlusKilogram()
        {
            QuantityWeight w1 = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight w2 = new QuantityWeight(2.0, WeightUnit.Kilograms);
            QuantityWeight sum = w1.Add(w2);
            Assert.That(sum.value, Is.EqualTo(3.0));
            Assert.That(sum.unit, Is.EqualTo(WeightUnit.Kilograms));
        }

        [Test]
        public void testAddition_CrossUnit_KilogramPlusGram()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight gram = new QuantityWeight(1000.0, WeightUnit.Grams);
            QuantityWeight sum = kg.Add(gram); // Defaults to kg
            Assert.That(sum.value, Is.EqualTo(2.0));
        }

        [Test]
        public void testAddition_CrossUnit_PoundPlusKilogram()
        {
            QuantityWeight pound = new QuantityWeight(2.20462, WeightUnit.Pound);
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight sum = pound.Add(kg); // Result in Pounds
            Assert.That(sum.value, Is.EqualTo(4.40924).Within(Epsilon));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Gram()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight gram = new QuantityWeight(1000.0, WeightUnit.Grams);
            QuantityWeight sum = kg.Add(gram, WeightUnit.Grams);
            Assert.That(sum.value, Is.EqualTo(2000.0));
            Assert.That(sum.unit, Is.EqualTo(WeightUnit.Grams));
        }

        [Test]
        public void testAddition_Commutativity()
        {
            QuantityWeight kg = new QuantityWeight(1.0, WeightUnit.Kilograms);
            QuantityWeight gram = new QuantityWeight(1000.0, WeightUnit.Grams);
            
            QuantityWeight res1 = kg.Add(gram, WeightUnit.Kilograms);
            QuantityWeight res2 = gram.Add(kg, WeightUnit.Kilograms);
            
            Assert.That(res1.value, Is.EqualTo(res2.value).Within(Epsilon));
        }

        [Test]
        public void testAddition_WithZero()
        {
            QuantityWeight kg = new QuantityWeight(5.0, WeightUnit.Kilograms);
            QuantityWeight zero = new QuantityWeight(0.0, WeightUnit.Grams);
            QuantityWeight result = kg.Add(zero);
            Assert.That(result.value, Is.EqualTo(5.0));
        }

    }
}