using System.Collections.ObjectModel;

namespace LanguageClassLibrary;
public class Word : LanguageUnit<char>, ICreating, IErasing
{
    public override string Content { get => content; set => content = value; }

    #region Конструктор
    public Word(string content) : base(content)
    {
        containerUnits = GetParts();
    }
    #endregion

    #region Индексатор
    public override char this[int i]
    {
        get => containerUnits[i];
    }
    #endregion

    #region Методы
    protected override string[] GetPartsStringArray(string str)
    {
        return new string [] { str };
    }
    protected override ObservableCollection<char> GetParts()
    {
        var temp = GetPartsStringArray(content)[0].ToCharArray();

        ObservableCollection<char> letters = new ObservableCollection<char>();
        foreach (var el in temp)
        {
            letters.Add(el);
        }

        return letters;
    }
    public override void Sort()
    {
        containerUnits.Sort();
        string newContent = string.Empty;

        foreach (var el in containerUnits)
        {
            newContent += el.ToString();
        }

        content = newContent;
    }
    public override bool Search(string str)
    {
        return content.Contains(str, StringComparison.CurrentCultureIgnoreCase);
    }
    public void Create(string str)
    {
        content += str;
        foreach (var el in str)
        {
            containerUnits.Add(el);
        }
    }
    public void Erase(string str)
    {
        content = content.Replace(str.ToCharArray()[0], default);
        containerUnits = new ObservableCollection<char>
                         (containerUnits.Where(x => x != str.ToCharArray()[0]).ToList());
    }
    public override char SearchElement(string str)
    {
        foreach (var el in containerUnits)
        {
            if (str == containerUnits.ToString())
            {
                return el;
            }
        }

        return default;
    }
    public override string ToString()
    {
        return Content;
    }
    #endregion
}
