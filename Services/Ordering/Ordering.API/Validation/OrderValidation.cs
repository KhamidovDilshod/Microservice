using FluentValidation;
using Ordering.Domain.Entities;

namespace Ordering.Validation;

public class OrderValidation : AbstractValidator<Order>
{
    public OrderValidation()
    {
        RuleFor(r => r.UserName)
            .NotEmpty()
            .WithMessage("Username must not be empty!");
    }
}