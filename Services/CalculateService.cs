namespace Task1.Services;

public class CalculateService : ICalculateService
{
    public double Calculate(double vat, int amount, double price)
    {
        var calculatedPrice = ((amount * price) * (1 + vat));

        return calculatedPrice;
    }
}