namespace Task1.Services;

public class CalculateService
{
    public double Calculate(double vat, int amount, double price)
    {
        var calculate = ((amount * price) * (1 + vat));

        return calculate;
    }   
    
}