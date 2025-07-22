using Application.Dto.Shared;
using FluentValidation;

namespace Application.Validations.Shared;

public class OrderDtoValidator : AbstractValidator<List<OrderDto>>
{
    public OrderDtoValidator()
    {
        var entityType = typeof(Domain.Entities.Section);
        
        RuleFor(x => x)
            .Custom((list, context) =>
            {
                var invalidOrders = list.Where(d => d.Order < 1).ToList();
                if (invalidOrders.Any())
                {
                    context.AddFailure("تمام جایگاه‌ها باید عددی مثبت و بزرگتر از صفر باشند.");
                }

                var duplicateOrders = list
                    .GroupBy(d => d.Order)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateOrders.Any())
                {
                    context.AddFailure("عدد تکراری به عنوان جایگاه غیرقابل قبول است");
                }
            });
    }
}