namespace Task1.Services;

public class CalculateService
{
    public double Calculate(double vat, ulong amount, double price)
    {
        var calculate = ((amount * price) * (1 + vat));

        return calculate;
    }   
    
}