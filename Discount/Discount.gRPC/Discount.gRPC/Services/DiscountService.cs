using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.pros;
using Discount.gRPC.Repositories;
using Grpc.Core;

namespace Discount.gRPC.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with ProductName={request.ProductName} not found"));
        _logger.LogInformation("Discount is successfully created. ProductName:{ProductName}", coupon.ProductName);

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        var result = await _discountRepository.CreateDiscount(coupon);
        _logger.LogInformation("Discount is successfully created. ProductName:{ProductName}", coupon.ProductName);
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        await _discountRepository.UpdateDiscount(coupon);
        _logger.LogInformation("Discount is successfully updated. ProductName:{ProductName}", coupon.ProductName);
        var couponModel = _mapper.Map<CouponModel>(request.Coupon);
        Console.WriteLine("Calling");
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };
        var responseString = getDiscount("");
        return response;
    }

    private string getDiscount(string s)
    {
        var req = _discountRepository.GetDiscount(s);
        var response = _mapper.Map<string>(req);
        return response;
    }
}