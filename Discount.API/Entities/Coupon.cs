namespace E_Commerce.Discount.Entities;
#pragma warning disable

public class Coupon
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}