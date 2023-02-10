using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ModelPresenterLibrary
{
    public class PresenterText
    {
        private ModelText _modelText;
        
        public PresenterText(ModelText _modelText)
        {
            this._modelText = _modelText;
        }

        public ModelText Show(string text, string name)
        {
            _modelText.ShowSentences(name, text);

            return _modelText;
        }
        public ModelText Sort()
        {
            _modelText.SortSentences();

            return _modelText;
        }
        public ModelText Search(string text, ref bool check)
        {
            var temp = text.Split('.', '?', '!'); // Обработка полученной информации
            temp = temp.Where(t => t != string.Empty).ToArray();

            if (temp.Length == 1) _modelText.SearchSentences(temp[0].Trim(), ref check); // Обновление модели

            return _modelText; // Передача обновленной модели
        }
        public ModelText Create(string text)
        {
            if(text.Split('.', '?', '!').Where(t => t != string.Empty).ToArray().Length != 0) _modelText.CreateSentences(text);

            return _modelText;
        }
        public ModelText Erase(string text)
        {
            var temp = text.Split('.', '?', '!');
            temp = temp.Where(t => t != string.Empty).ToArray();

            if (temp.Length == 1) _modelText.EraseSentences(text);

            return _modelText;
        }
    }
}
