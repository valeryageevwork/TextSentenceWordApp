using System;
using System.Windows;
using System.Windows.Controls;
using LanguageClassLibrary;
using ModelPresenterLibrary;

namespace UserInterfaceWPF
{
    public partial class SentenceWindow : Window
    {
        #region Поля
        private ModelSentence _modelSentence;
        private PresenterSentence _presenterSentence;
        #endregion

        private event Action<bool> OnActivateButtonShow;

        private TextBox textBox;
        #region Конструктор
        public SentenceWindow(Text txt, TextBox textBox)
        {
            InitializeComponent();

            _modelSentence = new ModelSentence(txt);
            _presenterSentence = new PresenterSentence(_modelSentence);

            OnActivateButtonShow += DisEnableButtons;

            this.textBox = textBox;
        }
        #endregion

        #region Методы
        private void buttonShowWords_Click(object sender, RoutedEventArgs e)
        {
            _modelSentence = _presenterSentence.Show(textBoxSentence.Text);

            if (_modelSentence.Stn != null)
            {
                listViewWords.ItemsSource = _modelSentence.Stn.ContainerUnits;
                OnActivateButtonShow(true);
            }
            else
            {
                listViewWords.ItemsSource = null;
                OnActivateButtonShow(false);
            }
            
        }

        private void buttonSortWords_Click(object sender, RoutedEventArgs e)
        {
            _modelSentence.SortWords();
            textBox.Text = _modelSentence.Txt.Content;
        }

        private void buttonSearchWords_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = false;
            _modelSentence.SearchWord(textBoxWordsSearch.Text, ref isChecked);

            checkBoxSearchWords.IsChecked = isChecked;
        }

        private void buttonCreateWord_Click(object sender, RoutedEventArgs e)
        {
            _modelSentence.CreateWord(textBoxCreateWord.Text);
            OnActivateButtonShow(false);

            textBox.Text = _modelSentence.Txt.Content;
        }

        private void buttonEraseWord_Click(object sender, RoutedEventArgs e)
        {
            _modelSentence.EraseWord(textBoxEraseWord.Text);
            OnActivateButtonShow(false);

            textBox.Text = _modelSentence.Txt.Content;
        }
        private void DisEnableButtons(bool disOrEn)
        {
            buttonSortWords.IsEnabled = disOrEn;
            buttonSearchWords.IsEnabled = disOrEn;
            buttonCreateWord.IsEnabled = disOrEn;
            buttonEraseWord.IsEnabled = disOrEn;
        }
        #endregion 
    }
}
