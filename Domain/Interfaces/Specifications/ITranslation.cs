namespace Domain.Interfaces;

public interface ITranslation<T>
{
    public ICollection<T> Translations { get; set; }
}