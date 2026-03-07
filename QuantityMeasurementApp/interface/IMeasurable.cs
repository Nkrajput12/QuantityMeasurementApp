namespace QuantityMeasurementApp.Interface

{
    public interface IMeasurable
    {
        double GetConversionFactor();
        double ConvertFromBase(double value);
        double ConvertToBase(double baseValue);
        string GetSymbol();
    }
}