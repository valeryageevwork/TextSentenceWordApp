using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageClassLibrary;
using System.Text.RegularExpressions;

namespace ModelPresenterLibrary
{
    public class PresenterSentence
    {
        private ModelSentence _modelSentence;

        public PresenterSentence(ModelSentence _modelSentence)
        {
            this._modelSentence = _modelSentence;
        }

        public ModelSentence Show(string text)
        {
            var temp = text.Trim().Split('.', '?', '!');
            temp = temp.Where(t => !string.IsNullOrEmpty(t)).ToArray();
            temp = temp.Select(t => t.Trim()).ToArray();

            if (temp.Length == 1) _modelSentence.ShowWords(temp[0]);

            return _modelSentence;
        }
        public ModelSentence Sort()
        {
            _modelSentence.SortWords();

            return _modelSentence;
        }
        public ModelSentence Search(string word, ref bool check)
        {
            Regex regex = new Regex(@"^\w+$");
            if (regex.IsMatch(word))
            {
                _modelSentence.SearchWord(word, ref check);
            }

            return _modelSentence;
        }
        public ModelSentence Create(string word)
        {
            Regex regex = new Regex(@"^\w+$");
            if (regex.IsMatch(word))
            {
                _modelSentence.CreateWord(word);
            }
            return _modelSentence;
        }
        public ModelSentence Erase(string word)
        {
            Regex regex = new Regex(@"^\w+$");
            if (regex.IsMatch(word))
            {
                _modelSentence.EraseWord(word);
            }
            return _modelSentence;
        }
    }
}
