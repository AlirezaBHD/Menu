using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilder<T, string> ApplyLengthValidation<T>(
        this IRuleBuilderInitial<T, string> ruleBuilder,
        Expression<Func<T, string>> expression,
        Type entityType
    )
    {
        var propertyName = GetPropertyName(expression);
        var prop = entityType.GetProperty(propertyName);
        var displayAttr = prop?.GetCustomAttribute<DisplayAttribute>();
        var displayName = displayAttr?.Name ?? propertyName;

        var maxLengthAttr = prop?.GetCustomAttribute<MaxLengthAttribute>();

        var rule = ruleBuilder.NotEmpty()
            .WithMessage($"{displayName} نباید خالی باشد");

        if (maxLengthAttr != null)
        {
            rule = rule.MaximumLength(maxLengthAttr.Length)
                .WithMessage($"حداکثر طول {displayName} {maxLengthAttr.Length} کاراکتر است.");
        }

        return rule;
    }

    private static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression memberOperand)
            return memberOperand.Member.Name;

        throw new ArgumentException("Expression must be a member expression");
    }
}