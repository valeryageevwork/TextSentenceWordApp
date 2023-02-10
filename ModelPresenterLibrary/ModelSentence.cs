using LanguageClassLibrary;

namespace ModelPresenterLibrary
{
    public class ModelSentence
    {
        private Text _text;
        private Sentence _stn;

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
        public Sentence Stn
        {
            get
            {
                return _stn;
            }
            set
            {
                _stn = value;
            }
        }

        public ModelSentence(Text txt)
        {
            Txt = txt;
        }
        public void ShowWords(string sentence)
        {
            _stn = Txt.SearchElement(sentence);
        }
        public void SortWords()
        {
            _stn.Sort();
            Txt.AttachedContent();
        }
        public void SearchWord(string word, ref bool check)
        {
            check = Stn.Search(word);
        }
        public void CreateWord(string word)
        {
            _stn.Create(word);
            Txt.AttachedContent();
        }
        public void EraseWord(string word)
        {
            _stn.Erase(word);
            Txt.AttachedContent();
        }
    }
}
