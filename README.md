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
3. Branch: feature/UC6-AdditionofTwoLengthUnits

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 7: Explicit Target Unit Addition
1. Objective: Provide full control over the output unit, allowing the addition of two different units to be represented in a third, unrelated target unit.
2. Implementation: * Re-scaling Logic: Extends arithmetic by dividing the base-unit sum by the target unit’s conversion factor
   1. .Method Overloading: Added Add(other, targetUnit) to support both implicit (UC6) and explicit (UC7) outputs.
   2. Precision Tolerance: Utilized Epsilon ($0.001$) to maintain mathematical accuracy during floating-point conversions.
3. Key Learning: Decoupling internal calculation (Normalization) from external representation (Target Scaling)
4. .Branch: feature/UC7-TargetUnitAddition

-----------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 8: Architectural Refactoring & Responsibility Delegation
1. Objective: Refactor the design to eliminate circular dependencies and adhere to the Single Responsibility Principle (SRP) by moving conversion logic out of the Quantity class and into a standalone unit model.
2. implementation: * Standalone Enum: Extracted LengthUnit into a top-level enum, removing it from the scope of the Quantity class.
   1. Delegation Pattern: Assigned the responsibility of "Normalization" to the unit itself using extension methods (ConvertToBase and ConvertFromBase).
   2. Simplified Model: Refactored Quantity to act as a coordinator that delegates math to the unit extensions, making the class lean and category-agnostic.
3. Key Learning: Understanding how to separate Domain Logic (addition/equality) from Data Representation (unit conversion factors) to improve system scalability and maintainability.
4. Branch: feature/UC8-StandaloneUnit

-----------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 9: Weight Measurement & Category Isolation
1. Objective: Expand the application beyond length by introducing Weight as a new measurement category. The goal is to demonstrate that the architecture from UC8 is scalable and type-safe across different physical dimensions.
2. Key Implementation:
   1. Independent Category: Created a QuantityWeight class and a WeightUnit enum to ensure weight measurements are isolated from length.
   2. Dimension Protection: Implemented "Category Type Safety" within the .Equals() method to prevent logical errors, such as comparing Kilograms to Feet.
   3. Base Unit Normalization: Established Kilograms (kg) as the internal base unit ($1.0$), with Grams ($0.001$) and Pounds ($0.453592$) as scaled units.
   4. Precision Engineering: Applied a $0.001$ epsilon tolerance to handle the repeating decimals inherent in Metric-to-Imperial (Pounds to Kg) conversions.
3. Engineering Principle: Interface Segregation & Domain Isolation—ensuring that while the logic patterns (addition/conversion) are similar, the data domains remain strictly separated to maintain mathematical integrity.
4. Branch: feature/UC9-Weight-Measurement

------------------------------------------------------------------------------------------------------------------------------------------
