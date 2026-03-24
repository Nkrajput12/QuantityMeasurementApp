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
4. Branch: [feature/UC1-FeetMeasurementEquality](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC1-FeetMeasurementEquality)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 2: Feet and Inch Measurement Equality
1. Objective: Introduce a second unit, Inches, and ensure 1 inch equals 1 inch.
2. Implementation: Created a parallel Inches class with similar equality logic.
3. Key Learning: Type safety—ensuring Feet cannot be compared to Inches without a conversion layer.
4. Branch: [feature/UC2-FeetAndInchesMeasurementEquality](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC2-FeetAndInchesMeasurementEquality)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 3: Generic Scaling & Cross-Unit Comparison
1. Objective: Refactor the codebase to eliminate duplication (DRY principle) and allow 1 Feet == 12 Inches.
2. Major Refactoring:
    1. Unified Model: Replaced separate unit classes with a single Quantity class.
    2. Base Unit Normalization: All units are converted to a common base (Inches) before comparison.
    3. Precision Handling: Introduced an Epsilon (0.001) to handle floating-point rounding errors.
3. Branch: [feature/UC3-GenericLength](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC3-GenericLength)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 4: Extended Unit Support (Yards & Centimeters)
1. Objective: Demonstrate that the generic design from UC3 can scale effortlessly without changing business logic.
2. New Units Added: * Yards: (1 yd = 36 in) Centimeters: (1 cm = 0.393701 in)
3. Engineering Principle: Open/Closed Principle—The system is open for extension (adding units) but closed for modification (the Quantity class logic remains untouched).
4. Branch: [feature/UC4-ExtendedUnitSupport](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC4-ExtendedUnitSupport)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 5: Unit-to-Unit Conversion
1. Objective: Enable functional conversion between length units (e.g., Yards to Feet) rather than just checking equality.
2. Implementation:
   1. Base Unit Normalization: Converts source to Inches, then scales to target.
   2. Immutability: ConvertTo() returns a new object instead of modifying the existing one.
   3. Method Overloading: Provided both static (raw math) and instance (object-based) conversion methods.
3. Branch: [feature/UC5-UnitConversion](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC5-UnitConversion)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 6: Length Addition

1. Objective: UC6 enables functional arithmetic between different length units. The system converts operands to a base unit, sums them, and returns a new Quantity in the unit of the first operand.
2. Key Implementation
   1. Normalization: Converts both inputs to Inches before summation to ensure accuracy.
   2. Immutability: Returns a new instance; original objects remain unchanged.
   3. Math Logic: Supports Commutativity ($A + B = B + A$) and the Identity Element (adding zero).
   4. Validation: Implements guard clauses to throw ArgumentNullException for null inputs.
3. Branch: [feature/UC6-AdditionofTwoLengthUnits](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC6-AdditionofTwoLengthUnits)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 7: Explicit Target Unit Addition
1. Objective: Provide full control over the output unit, allowing the addition of two different units to be represented in a third, unrelated target unit.
2. Implementation: * Re-scaling Logic: Extends arithmetic by dividing the base-unit sum by the target unit’s conversion factor
   1. .Method Overloading: Added Add(other, targetUnit) to support both implicit (UC6) and explicit (UC7) outputs.
   2. Precision Tolerance: Utilized Epsilon ($0.001$) to maintain mathematical accuracy during floating-point conversions.
3. Key Learning: Decoupling internal calculation (Normalization) from external representation (Target Scaling)
4. .Branch: [feature/UC7-TargetUnitAddition](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC7-TargetUnitAddition)

-----------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 8: Architectural Refactoring & Responsibility Delegation
1. Objective: Refactor the design to eliminate circular dependencies and adhere to the Single Responsibility Principle (SRP) by moving conversion logic out of the Quantity class and into a standalone unit model.
2. implementation: * Standalone Enum: Extracted LengthUnit into a top-level enum, removing it from the scope of the Quantity class.
   1. Delegation Pattern: Assigned the responsibility of "Normalization" to the unit itself using extension methods (ConvertToBase and ConvertFromBase).
   2. Simplified Model: Refactored Quantity to act as a coordinator that delegates math to the unit extensions, making the class lean and category-agnostic.
3. Key Learning: Understanding how to separate Domain Logic (addition/equality) from Data Representation (unit conversion factors) to improve system scalability and maintainability.
4. Branch: [feature/UC8-StandaloneUni](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC8-StandaloneUnit)

-----------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 9: Weight Measurement & Category Isolation
1. Objective: Expand the application beyond length by introducing Weight as a new measurement category. The goal is to demonstrate that the architecture from UC8 is scalable and type-safe across different physical dimensions.
2. Key Implementation:
   1. Independent Category: Created a QuantityWeight class and a WeightUnit enum to ensure weight measurements are isolated from length.
   2. Dimension Protection: Implemented "Category Type Safety" within the .Equals() method to prevent logical errors, such as comparing Kilograms to Feet.
   3. Base Unit Normalization: Established Kilograms (kg) as the internal base unit ($1.0$), with Grams ($0.001$) and Pounds ($0.453592$) as scaled units.
   4. Precision Engineering: Applied a $0.001$ epsilon tolerance to handle the repeating decimals inherent in Metric-to-Imperial (Pounds to Kg) conversions.
3. Engineering Principle: Interface Segregation & Domain Isolation—ensuring that while the logic patterns (addition/conversion) are similar, the data domains remain strictly separated to maintain mathematical integrity.
4. Branch: [feature/UC9-Weight-Measurement](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC9-Weight-Measurement)

------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 10: Generic Architecture & Unified Measurement
1. Objective: The final evolutionary step of the application. The goal of UC10 is to achieve the Ultimate DRY (Don't Repeat Yourself) principle by collapsing category-specific classes into a single, highly flexible Generic Quantity Model.  
2. Key Implementation:
   1. Generic Type Constraints: Refactored the Quantity class to use a generic parameter <TUnit>. By applying the constraint where TUnit : struct, Enum, the class       becomes a universal container for any measurement category.
   2. Bridge Logic: Implemented a "Static Bridge" using pattern matching. The generic Quantity<TUnit> class identifies the specific Enum type at runtime to delegate mathematical normalization to the appropriate static extension methods.
   3. Unified Service Layer: Refactored the QuantityMeasurementService to be entirely generic. A single set of Compare<T>, Convert<T>, and Add<T> methods now services Length, Weight, and all future dimensions.
   4. Type Safety: Leverages the .NET runtime's ability to distinguish between Quantity<LengthUnit> and Quantity<WeightUnit>, physically preventing the logical error of adding Kilograms to Feet.
3. Engineering Principle: Parametric Polymorphism—demonstrating that new categories (Volume, Temperature) can be added by simply creating a new Enum. The core Quantity engine requires zero modifications to scale.
4. Branch: [feature/UC10-GenericQuantity](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC10-GenericQuantity)

-----------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 11: Volume Measurement & Multi-Category Scaling
1. Objective: UC11 extends the application to support Volume (Litres, Millilitres, Gallons). This use case is the ultimate validation of the Generic Architecture from UC10, proving the system can scale to a third physical dimension with zero modifications to the core Quantity<T> engine.
2. Key Implementation:
   1. Standalone Volume Enum: Created a VolumeUnit enum implementing the IMeasurable pattern. This ensures Volume remains a separate, non-interoperable category from Length and Weight.
   2. Base Unit Normalization: Established Litre (L) as the internal base unit ($1.0$), with Millilitres ($0.001$) and Gallons ($3.78541$) as scaled units.
   3. Plug-and-Play Integration: Demonstrated that the generic Quantity<TUnit> class automatically handles Volume equality ($1\text{ L} == 1000\text{ mL}$) and addition without needing new logic.
   4. Dimension Protection: Leveraged .NET Generics to ensure that a Quantity<VolumeUnit> cannot be compared or added to a Quantity<LengthUnit> at compile-time, maintaining mathematical integrity.
3. Engineering Principle: Open/Closed Principle—The system is "Open" for new categories (Volume) but "Closed" for modification, as the primary business logic in the Quantity class remains untouched.
4. Branch: [feature/UC11-VolumeMeasurement](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC11-VolumeMeasurement)

--------------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 12: Subtraction, Division & Arithmetic Scaling
1. Objective: Complete the mathematical engine by introducing Subtraction and Division operations. This use case ensures that the generic architecture can handle non-commutative operations while maintaining physical and dimensional integrity.
2. Key Implementation:
   1. Normalization-Based Subtraction: Implements Subtract(other) by converting both quantities to a base unit, calculating the difference, and returning a new Quantity<TUnit>.
   2. Dimensionless Division: Unlike addition or subtraction, the Divide(other) method returns a double (Scalar). This follows the physics principle that dividing two like-dimensions (e.g., Length / Length) results in a dimensionless ratio.
   3. Implicit & Explicit Output: Supports both implicit results (returning the unit of the first operand) and explicit results (re-scaling the difference to a user-specified target unit).
   4. Safety & Validation: Implements strict "Division by Zero" guards and ensures that the system throws an ArgumentException if a user attempts to subtract Weight from Length.
3. Engineering Principle: Mathematical Soundness & Immutability—ensuring that operations do not modify the original objects and that the return types (Quantity vs. Scalar) correctly reflect the dimensional results of the calculation.
4. Branch: [feature/UC12-SubtractionAndDivision](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC12-SubtractionAndDivision)

--------------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 13: Centralized Arithmetic & DRY Refactoring
1. Objective: Refactor the Quantity<TUnit> class to eliminate code duplication across all mathematical operations. The goal is to adhere to the DRY (Don't Repeat Yourself) principle by consolidating validation and normalization into a single internal engine.
2. Key Implementation:
   1. Internal Arithmetic Engine: Introduced a private ArithmeticOperation enum and a central PerformBaseArithmetic method to handle all math logic in one place.
   2. Unified Validation: Consolidated null checks and category compatibility verification into a single "Gatekeeper" logic, ensuring consistent error reporting.
   3. Normalization Consolidation: All operations now share the same internal pipeline for converting operands to base units, reducing the risk of divergent math logic.
   4. Precision & Scaling: Integrated UC12's rounding logic directly into the centralized workflow, ensuring consistent formatting for all returned quantities.
2. Engineering Principle: Abstraction & Maintainability—Demonstrating that as a system grows, the internal code should become more consolidated. By centralizing the "how" (calculation), the "what" (public API) becomes cleaner and easier to test.
4. Branch: [feature/UC13-CentralizedArithmetic](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC13-CentralizedArithmetic)

---------------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 14: Temperature Measurement
1. Objective: Extend the application to support Temperature (Celsius, Fahrenheit, Kelvin). Address a unique domain challenge: while temperatures can be added or subtracted to represent physical differences, dividing them is mathematically and physically meaningless.
2. Key Implementation:
   1. Non-Linear Conversion: Implemented offset-based conversion formulas (e.g., Fahrenheit to Celsius) via the TemperatureUnitExtension, handling the fact that temperature doesn't scale from an absolute zero in the same way Length or Weight does.
   2. Operation Gatekeeper: Modified the central PerformBaseArithmetic engine to act as a gatekeeper. It dynamically intercepts and blocks mathematically invalid operations (like Divide) specifically for TemperatureUnit, throwing a precise InvalidOperationException.
   3. Domain-Specific Rules: Permitted Addition and Subtraction for temperatures to represent valid temperature differences, successfully tailoring the generic math engine to specific physical constraints.
   4. Exception Handling: Ensured that unsupported operations throw clear, descriptive exceptions rather than returning invalid or "dummy" data, maintaining the integrity of the system's output.
3. Engineering Principle: Domain-Driven Design & Exception Safety—ensuring the software model accurately reflects the strict physical laws of the measurement domain, and proving that a generic architecture (Quantity<TUnit>) can still enforce category-specific operational rules.
4. Branch: [feature/UC14-TemperatureMeasurement](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC14-TemperatureMeasurement)

------------------------------------------------------------------------------------------------------------------------------

🚀 Use Case 15: Architectural Refactoring & Cache Memory
1. Objective: Fundamentally restructure the application from a simple console script into a formal, highly decoupled N-Tier Architecture (UI, Business, Repository, Model layers). Alongside this structural upgrade, introduce local state persistence (Cache Memory) to bridge the gap between temporary RAM storage and a full database.
2. Key Implementation:
   1. N-Tier Separation: Extracted data access logic and business rules into distinct layers, ensuring the UI (Console) no longer directly manages state or calculations.
   2. DTO Pattern: Introduced Data Transfer Objects (CacheRecordDto) in the Model Layer to standardize the format of the data traveling securely between the separated application layers.
   3. Repository Pattern: Created an ICacheRepository interface to completely decouple the file system operations from the business logic.
   4. JSON Serialization & Cache Management: Implemented an InMemoryCacheRepository utilizing System.Text.Json to automatically serialize and deserialize the active RAM cache into a neatly formatted .json file, guaranteeing history survives application restarts.
3. Engineering Principle: Separation of Concerns (SoC) & Interface Segregation—Demonstrating that the Business Service doesn't need to know how or where data is saved; it only interacts with an interface contract, making the system modular and highly testable.
4. Branch: [feature/UC15-NTier](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC15-NTier)

-----------------------------------------------------------------------------------------------------------------------------

🚀 Use Case 16: SQL Database Integration
1. Objective: Elevate the application to an enterprise-grade standard by introducing a permanent, relational SQL database. This validates the architectural refactoring from UC15 by proving that a new storage medium can be added without changing the core business logic.
2. Key Implementation:
   1. ADO.NET Integration: Created a QuantityMeasurementDatabaseRepository (IDatabaseRepository) using SqlConnection, SqlCommand, and SqlDataReader to permanently store and retrieve measurement history.
   2. Dependency Injection (DI): Configured the application's entry point (Program.cs) to act as the Composition Root. It instantiates the Configuration, Cache, and Database repositories, and seamlessly injects them into the QuantityMeasurementService.
   3. Security & Resiliency: Utilized Parameterized Queries (@OpType, @Input) to completely prevent SQL Injection attacks. Engineered an EnsureTableExists() method to dynamically generate the SQL schema if the table is missing, preventing system crashes.
   4. Centralized Configuration: Extracted the database connection string into a dedicated DatabaseConfig class, creating a single source of truth for environment settings.
3. Engineering Principle: Open/Closed Principle (OCP) & Dependency Inversion Principle (DIP)—Proving that the Data Access layer is "Open for extension" (adding SQL capabilities) but the Business layer is "Closed for modification" (the Service code remains completely untouched).
4. Branch: [feature/UC16-DatabaseIntegration](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC16-DatabaseIntegration)

------------------------------------------------------------------------------------------------------------------------------------------------------------

🚀 Use Case 17: Web API Transition & ASP.NET Core Integration
1. Objective: Transform the application from a localized Console application into a distributed RESTful Web API. This step exposes the core measurement logic (comparison, conversion, and arithmetic) over HTTP, allowing any external client (web, mobile, or third-party system) to interact with the engine.
2. Key Implementation:
   1. API Controllers & Routing: Created a dedicated MeasurementController to handle incoming HTTP requests (GET, POST), mapping external JSON payloads to internal Data Transfer Objects (DTOs) seamlessly.
   2. Built-in Dependency Injection: Upgraded the manual Composition Root from the console app into ASP.NET Core’s IServiceCollection (Program.cs). Automatically injected the Business Service, Cache Repository, and Database Repository using appropriate lifecycles (e.g., AddScoped, AddSingleton).
   3. Configuration Management: Migrated the database connection string and environment variables from hardcoded classes into appsettings.json, utilizing the framework's IConfiguration for secure, centralized, and environment-agnostic setups.
   4. Swagger UI Integration: Added OpenAPI/Swagger middleware to automatically generate interactive API documentation. This provides a clean web interface to test the measurement endpoints without needing a frontend or Postman.
3. Engineering Principle: Client-Server Architecture & RESTful Design—Demonstrating the ultimate power of N-Tier architecture. Because the Business and Data layers were perfectly decoupled in UC15/16, the entire UI could be ripped out (Console to Web API) without modifying a single line of the underlying domain logic.
4. Branch: [feature/UC17-ASP.NET-Framework-Integration](https://github.com/Nkrajput12/QuantityMeasurementApp/tree/feature/UC17-ASP.NET-Framework-Integration)

