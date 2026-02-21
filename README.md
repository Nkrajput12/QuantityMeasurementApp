⚖️ Quantity Measurement App
📝 Project Overview
The Quantity Measurement App is a tool designed to compare quantities (such as Length and Weight) and provide accurate comparisons across various units.

The application follows an evolutionary path:

1. Comparison: Comparing two quantities.
2. Conversion: Transforming one unit to another.
3. Arithmetic: Supporting mathematical operations across units.
------------------------------------------------------------------------------------------------------------------------------------------
🚀 Use Case 1: Feet Measurement Equality
The goal of this use case is to ensure that two measurement objects representing Feet are compared based on their values rather than their memory references.

Key Features
1. Encapsulation: Measurement values are stored in private, immutable fields.
2. Value-Based Equality: Overridden Equals() and GetHashCode() methods.
3. Type Safety: Ensures that a Feet object can only be compared to another Feet object.
4. Layered Architecture: Separated into Models, Services, and Tests.

🛠 Project Structure
The project follows a clean separation of concerns:

1. Models: Contains the Feet class with the equality logic.
2. Service: Contains FeetUtil to handle business operations.
3. Tests: NUnit project containing all validation test cases.
4. UI: Console-based menu for manual verification.