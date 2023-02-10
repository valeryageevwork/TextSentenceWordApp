using System;
using System.Linq;
using System.Windows;
using LanguageClassLibrary;
using ModelPresenterLibrary;

namespace UserInterfaceWPF
{
    public partial class MainWindow : Window
    {
        #region Поля
        private ModelText _modelText;
        private PresenterText _presenterText;
        SentenceWindow sentenceWindow;
        #endregion

        private event Action<bool> OnActivateButtonShow;

        #region Конструктор
        public MainWindow()
        {
            InitializeComponent();

            _modelText = new ModelText();
            _presenterText = new PresenterText(_modelText);

            OnActivateButtonShow += DisEnableButtons;

            textBoxText.DataContext = _modelText.Txt;
        }
        #endregion

        #region Методы
        private void buttonShowSentence_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxText.Text != string.Empty)
            {
                _modelText = _presenterText.Show(textBoxText.Text, textBoxName.Text);

                listViewSentences.ItemsSource = null;
                listViewSentences.ItemsSource = _modelText.Txt.ContainerUnits;

                OnActivateButtonShow(true);
            }
        }
        private void DisEnableButtons(bool disOrEn)
        {
            buttonSortSentences.IsEnabled = disOrEn;
            buttonSearchSentences.IsEnabled = disOrEn;
            buttonCreateSentence.IsEnabled = disOrEn;
            buttonEraseSentence.IsEnabled = disOrEn;
            buttonShowWindowSentences.IsEnabled = disOrEn;
        }
        private void buttonSortSentences_Click(object sender, RoutedEventArgs e)
        {
            _modelText = _presenterText.Sort();
            textBoxText.Text = _modelText.Txt.Content;
        }

        private void buttonSearchSentences_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = false;
            _modelText = _presenterText.Search(textBoxSentenceSearch.Text, ref isChecked);

            checkBoxSearchSentences.IsChecked = isChecked;
        }

        private void buttonCreateSentence_Click(object sender, RoutedEventArgs e)
        {
            _modelText = _presenterText.Create(textBoxCreateSentence.Text);
            textBoxText.Text = _modelText.Txt.Content;
        }

        private void buttonEraseSentence_Click(object sender, RoutedEventArgs e)
        {
            _modelText = _presenterText.Erase(textBoxEraseSentence.Text);
            textBoxText.Text = _modelText.Txt.Content;
        }

        private void buttonShowWindowSentences_Click(object sender, RoutedEventArgs e)
        {
            sentenceWindow = new SentenceWindow(_modelText.Txt, textBoxText);
            sentenceWindow.Show();
        }
        private void textBoxSentenceSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            checkBoxSearchSentences.IsChecked = false;
        }
        private void textBoxText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OnActivateButtonShow(false);
        }
        #endregion
    }
}
