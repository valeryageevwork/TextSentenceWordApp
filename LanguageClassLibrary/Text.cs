using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace LanguageClassLibrary;
public class Text : LanguageUnit<Sentence>, ICreating, IErasing, INotifyPropertyChanged
{
    #region Свойства
    public string Name {get; set; }
    public override string Content
    {
        get
        {
            return content;
        }
        set
        {
            content = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Конструктор
    public Text(string content, string name) : base(content)
    {
        Name = name;
        containerUnits = GetParts();
    }
    #endregion

    #region Индексатор
    public override Sentence this[int i]
    {
        get => containerUnits[i];
    }
    #endregion

    #region Методы
    protected override string[] GetPartsStringArray(string str)
    {
        var temp = str.Trim().Split('.', '?', '!');
        temp = temp.Where(t => !string.IsNullOrEmpty(t)).ToArray();
        temp = temp.Select(t => t.Trim()).ToArray();
        temp = temp.Where(t => !string.IsNullOrEmpty(t)).ToArray();

        return temp;
    }
    protected override ObservableCollection<Sentence> GetParts()
    {
        ObservableCollection<Sentence> sentences = new ObservableCollection<Sentence>();
        var temp = GetPartsStringArray(content);
        foreach(var el in temp)
        {
            sentences.Add(new Sentence(el));
        }

        return sentences;
    }
    public override void Sort()
    {
        var marks = new char[] {'.', '?', '!'};
        var allMarks = content.Where(c => marks.Contains(c)).ToArray();

        for (int i = 0; i < containerUnits.Count; i++)
        {
            if (i < allMarks.Length)
            {
                containerUnits[i].Create(allMarks[i].ToString());
            }
            else
            {
                containerUnits[i].Create(".");
            }
        }

        containerUnits.Sort();

        string newContent = string.Empty;

        foreach (var el in containerUnits)
        {
            newContent += el.ToString();
        }

        Content = newContent;

        for (int i = 0; i < containerUnits.Count; i++)
        {
            containerUnits[i].Erase(".");
            containerUnits[i].Erase("?");
            containerUnits[i].Erase("!");
        }
    }
    public override bool Search(string str)
    {
        foreach (var el in containerUnits)
        {
            if (str == el.Content)
            {
                return true;
            }
        }
        return false;
    }
    public void Create(string str)
    {
        var marks = new char[] { '.', '?', '!' };
        var allMarks = content.Where(c => marks.Contains(c)).ToArray();

        if (containerUnits.Count() == allMarks.Count())
        {
            Content += str + ".";
        }
        else
        {
            Content += "." + str + ".";
        }

        var temp = str.Trim().Split('.', '?', '!');
        temp = temp.Where(t => !string.IsNullOrEmpty(t)).ToArray();
        temp = temp.Select(t => t.Trim()).ToArray();

        containerUnits.Add(new Sentence(temp[0]));
    }
    public void Erase(string str)
    {
        var marks = new char[] { '.', '?', '!' };
        var allMarks = str.Where(c => marks.Contains(c)).ToArray();
        if (allMarks.Length >= 1)
        {
            
            Content = content.Replace(str, "");
            Regex regex = new Regex(@"[.?!]+[.?!]?[.?!]?\s?[.?!]+[.?!]?[.?!]?");

            if (regex.IsMatch(content))
            {
                Match match = regex.Match(content);
                Content = content.Replace(match.Value, string.Empty);
            }


            var temp = str.Trim().Split('.', '?', '!');
            temp = temp.Where(t => !string.IsNullOrEmpty(t)).ToArray();
            temp = temp.Select(t => t.Trim()).ToArray();

            containerUnits = new ObservableCollection<Sentence>
                             (containerUnits.Where(x => x.Content != temp[0]).ToList());
        }
    }
        
    public void AttachedContent()
    {
        string newContent = string.Empty;
        var marks = new char[] { '.', '?', '!' };
        var allMarks = content.Where(c => marks.Contains(c)).ToArray();

        for (int i = 0; i < containerUnits.Count(); i++)
        {
            if (i < allMarks.Length)
            {
                newContent += containerUnits[i].ToString() + allMarks[i];
            }
            else
            {
                newContent += containerUnits[i].ToString() + ".";
            }
        }
        Content = newContent;
    }
    public override Sentence SearchElement(string str)
    {
        foreach (var el in containerUnits)
        {
            if (str == el.Content)
            {
                return el;
            }
        }

        return null;
    }
    #endregion
}
