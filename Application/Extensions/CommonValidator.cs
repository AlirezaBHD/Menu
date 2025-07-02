using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions;

public static class CommonValidator
{
    #region Image Rule

    public static IRuleBuilderOptions<T, IFormFile> ImageRule<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        int maxSize = 5,
        string[]? allowedExtensions = null,
        string errorMessage = " فایل تصویر معتبر نیست.",
        bool blank = false)
    {
        allowedExtensions ??= new[] { ".webp", ".png", ".jpg", ".jpeg" };
        var maxSizeMb = maxSize * 1048576;

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
            }).WithMessage($" تصویر معتبر نیست. پسوند های معتبر: {string.Join(", ", allowedExtensions)}")
            .Must(file =>
            {
                if (file != null)
                {
                    return file.Length <= maxSizeMb;
                }

                return true;
            }).WithMessage($".حجم تصویر نمیتواند بیشتر از {maxSizeMb} مگابایت باشد");
    }

    #endregion
}