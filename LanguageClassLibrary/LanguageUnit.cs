using System.Collections.ObjectModel;

namespace LanguageClassLibrary;

public abstract class LanguageUnit <T>
{
    #region Поля
    protected string content;
    protected ObservableCollection<T> containerUnits;
    #endregion

    #region Свойства
    public ObservableCollection<T> ContainerUnits { get => containerUnits; }
    public abstract string Content { get; set; }
    #endregion

    #region Конструктор
    public LanguageUnit(string content)
    {
        this.content = content;
    }
    #endregion

    #region Методы
    protected abstract string[] GetPartsStringArray(string str);
    protected abstract ObservableCollection<T> GetParts();
    public abstract void Sort();
    public abstract bool Search(string str);
    public abstract T SearchElement(string str);
    #endregion

    #region Индексатор
    public abstract T this[int i] { get; }
    #endregion
}

