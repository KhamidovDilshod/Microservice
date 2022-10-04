namespace Discount.gRPC.Entities;
#pragma warning disable

public class Coupon
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}

public class Test
{
    private static double Calc(double amount, double rate, int term, int unrate)
    {
        double rmm = rate / 12;
        int rterm = term - unrate;
        double mp = Math.Floor(amount * (rmm / 100.0 * Math.Pow((1.0 + rmm / 100.0), rterm))) /
                    (Math.Pow((1.0 + rmm / 100.0), (rterm)) - 1.0);
        return mp;
    }
}