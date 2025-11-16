using FluentValidation;
using Muno.Application.Dto.Shared;
using Muno.Application.Localization;

namespace Muno.Application.Validations.Shared;

public class OrderDtoValidator : AbstractValidator<List<OrderDto>>
{
    public OrderDtoValidator()
    {
        var entityType = typeof(Domain.Entities.Sections.Section);
        
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

                if (duplicateOrders.Any())
                {
                    context.AddFailure(Resources.DuplicateSeatNotAllowed);
                }
            });
    }
}