using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Extensions;

public static class CommonValidationExtensions
{
    #region Image File Rule

    public static IRuleBuilderOptions<T, IFormFile> ImageFileRule<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        int maxSize = 5,
        string[]? allowedExtensions = null,
        string errorMessage = " فایل تصویر معتبر نیست.",
        bool blank = false)
    {
        allowedExtensions ??= new[] { ".webp", ".png", ".jpg", ".jpeg" };
        var maxSizeBytes = maxSize * 1024 * 1024;

        if (!blank)
        {
            ruleBuilder
                .NotNull().WithMessage(".تصویر الزامی است")
                .NotEmpty().WithMessage(".تصویر نمیتواند خالی باشد");
        }

        return ruleBuilder
            .Must(file =>
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName)?.ToLower();
                    return extension != null && allowedExtensions.Contains(extension);
                }

                return true;
            }).WithMessage($"تصویر معتبر نیست. پسوندهای مجاز: {string.Join(", ", allowedExtensions)}")

            .Must(file =>
            {
                if (file != null)
                {
                    return file.Length <= maxSizeBytes;
                }

                return true;
            }).WithMessage($"حجم تصویر نمی‌تواند بیشتر از {maxSize} مگابایت باشد.")

            .Must(file =>
            {
                if (file != null)
                {
                    try
                    {
                        using var stream = file.OpenReadStream();
                        Span<byte> header = stackalloc byte[8];
                        stream.Read(header);

                        // jpg
                        if (header[0] == 0xFF && header[1] == 0xD8)
                            return true;

                        // png
                        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E)
                            return true;

                        // webp/webm
                        if (header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46)
                            return true;

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }

                return true;
            }).WithMessage("محتوای فایل تصویر معتبر نیست.");
    }

    #endregion

    #region Length Validation Rule

    public static IRuleBuilderOptions<T, string> LengthValidationRule<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        Expression<Func<T, string>> expression,
        Type entityType,
        bool blank = false
    )
    {
        var propertyName = GetPropertyName(expression);
        var prop = entityType.GetProperty(propertyName);
        var displayAttr = prop?.GetCustomAttribute<DisplayAttribute>();
        var displayName = displayAttr?.Name ?? propertyName;

        var maxLengthAttr = prop?.GetCustomAttribute<MaxLengthAttribute>();

        var rule = ruleBuilder.NotEmpty().When(x => false); //just a placeholder
        if (!blank)
        {
            rule = rule.NotEmpty()
                .WithMessage($"{displayName} نباید خالی باشد")
                .NotNull().WithMessage($"{displayName} الزامی است");
        }

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

    #endregion
}