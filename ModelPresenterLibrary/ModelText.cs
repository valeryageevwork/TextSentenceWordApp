using LanguageClassLibrary;

namespace ModelPresenterLibrary
{
    public class ModelText 
    {
        private Text _text;
        public Text Txt
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        public void ShowSentences(string name, string text)
        {
            _text = new Text(text, name);
        }
        public void SortSentences()
        {
            _text.Sort();
        }
        public void SearchSentences(string sentence, ref bool check)
        {
            check = _text.Search(sentence);
        }
        public void CreateSentences(string text)
        {
            _text.Create(text);
        }
        public void EraseSentences(string text)
        {
            _text.Erase(text);
        }

    }
}