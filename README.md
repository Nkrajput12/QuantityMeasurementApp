⚖️ Quantity Measurement App
📝 Project Overview
The Quantity Measurement App is a tool designed to compare quantities (such as Length and Weight) and provide accurate comparisons across various units.

The application follows an evolutionary path:

1. Comparison: Comparing two quantities.
2. Conversion: Transforming one unit to another.
3. Arithmetic: Supporting mathematical operations across units.
------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 1: Feet Measurement Equality
UC1: Feet Measurement Equality
1. Objective: Compare two Feet objects based on their values rather than their memory addresses (Value-based equality).
2. Implementation: Overriding .Equals() and .GetHashCode() in a dedicated Feet class.
3. Key Learning: Understanding the difference between Reference Equality and Value Equality.
4. Branch: feature/UC1-FeetMeasurementEquality

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 2: Feet and Inch Measurement Equality
1. Objective: Introduce a second unit, Inches, and ensure 1 inch equals 1 inch.
2. Implementation: Created a parallel Inches class with similar equality logic.
3. Key Learning: Type safety—ensuring Feet cannot be compared to Inches without a conversion layer.
4. Branch: feature/UC2-FeetAndInchesMeasurementEquality

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 3: Generic Scaling & Cross-Unit Comparison
1. Objective: Refactor the codebase to eliminate duplication (DRY principle) and allow 1 Feet == 12 Inches.
2. Major Refactoring:
    1. Unified Model: Replaced separate unit classes with a single Quantity class.
    2. Base Unit Normalization: All units are converted to a common base (Inches) before comparison.
    3. Precision Handling: Introduced an Epsilon (0.001) to handle floating-point rounding errors.
3. Branch: feature/UC3-GenericLength

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 4: Extended Unit Support (Yards & Centimeters)
1. Objective: Demonstrate that the generic design from UC3 can scale effortlessly without changing business logic.
2. New Units Added: * Yards: (1 yd = 36 in) Centimeters: (1 cm = 0.393701 in)
3. Engineering Principle: Open/Closed Principle—The system is open for extension (adding units) but closed for modification (the Quantity class logic remains untouched).
4. Branch: feature/UC4-ExtendedUnitSupport

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 5: Unit-to-Unit Conversion
1. Objective: Enable functional conversion between length units (e.g., Yards to Feet) rather than just checking equality.
2. Implementation:
   1. Base Unit Normalization: Converts source to Inches, then scales to target.
   2. Immutability: ConvertTo() returns a new object instead of modifying the existing one.
   3. Method Overloading: Provided both static (raw math) and instance (object-based) conversion methods.
3. Branch: feature/UC5-UnitConversion

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 6: Length Addition

1. Objective: UC6 enables functional arithmetic between different length units. The system converts operands to a base unit, sums them, and returns a new Quantity in the unit of the first operand.
2. Key Implementation
   1. Normalization: Converts both inputs to Inches before summation to ensure accuracy.
   2. Immutability: Returns a new instance; original objects remain unchanged.
   3. Math Logic: Supports Commutativity ($A + B = B + A$) and the Identity Element (adding zero).
   4. Validation: Implements guard clauses to throw ArgumentNullException for null inputs.
3. Branch: feature/UC6-ArithmeticAddition

------------------------------------------------------------------------------------------------------------------------------------------
