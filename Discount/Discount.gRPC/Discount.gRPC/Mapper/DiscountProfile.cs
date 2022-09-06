using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.pros;

namespace Discount.gRPC.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}