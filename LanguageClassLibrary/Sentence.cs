using System.Collections.ObjectModel;

namespace LanguageClassLibrary;
public class Sentence : LanguageUnit<Word>, ICreating, IErasing
{
    public override string Content { get => content; set => content = value; }

    #region Конструктор
    public Sentence(string content) : base(content)
    {
        containerUnits = GetParts();
    }
    #endregion

    #region Индексатор
    public override Word this[int i]
    {
        get => containerUnits[i];
    }
    #endregion

    #region Методы
    protected override string[] GetPartsStringArray(string str)
    {
        var temp = str.Trim().Split(' ', '(', ')', ',', ':', ';', '\n');
        temp = temp.Where(t => !string.IsNullOrEmpty(t)).ToArray();

        return temp;
    }
    protected override ObservableCollection<Word> GetParts()
    {
        var temp = GetPartsStringArray(content);

        ObservableCollection<Word> words = new ObservableCollection<Word>();
        foreach (var el in temp)
        {
            words.Add(new Word(el));
        }

        return words;
    }
    public override void Sort()
    {
        containerUnits.Sort();
        string newContent = string.Empty;

        foreach (var el in containerUnits)
        {
            newContent += el.ToString() + " ";
        }
        
        content = newContent;
    }
    public override bool Search(string str)
    {
        return content.Contains(str, StringComparison.CurrentCultureIgnoreCase);
    }
    public void Create(string str)
    {
        if (str == "." || str == "?" || str == "!")
        {
            content += str;
        }
        else
        {
            content += " " + str;
            containerUnits.Add(new Word(str));
        }
        
    }
    public void Erase(string str)
    {
        content = content.Replace(str, string.Empty);
        containerUnits = new ObservableCollection<Word>
                             (containerUnits.Where(x => x.Content != str).ToList());
    }
    public override Word SearchElement(string str)
    {
        foreach (var el in containerUnits)
        {
            if (str == containerUnits.ToString())
            {
                return el;
            }
        }

        return null;
    }
    public override string ToString()
    {
        return Content;
    }
    #endregion
}
