namespace Domain.Interfaces.Specifications;

public interface ITranslation<T>
{
    public ICollection<T> Translations { get; set; }
}