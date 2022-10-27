namespace Task1.Services;

public interface ICalculateService
{
    public double Calculate(double vat, int amount, double price);
}