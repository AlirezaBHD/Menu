namespace Application.Dto.Shared;

public interface IHasTranslationsDto<T>
{
    public ICollection<T> Translations { get; set; }
}