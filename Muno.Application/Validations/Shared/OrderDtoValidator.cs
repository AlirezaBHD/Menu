using FluentValidation;
using Muno.Application.Dto.Shared;
using Muno.Application.Localization;

namespace Muno.Application.Validations.Shared;

public class OrderDtoValidator : AbstractValidator<List<OrderDto>>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x)
            .Custom((list, context) =>
            {
                var invalidOrders = list.Where(d => d.Order < 1).ToList();
                if (invalidOrders.Any())
                {
                    context.AddFailure(Resources.AllSeatsPositive);
                }

                var duplicateOrders = list
                    .GroupBy(d => d.Order)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateOrders.Count != 0)
                {
                    context.AddFailure(Resources.DuplicateSeatNotAllowed);
                }
            });
    }
}