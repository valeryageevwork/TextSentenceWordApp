using System.Collections.ObjectModel;

namespace LanguageClassLibrary
{
    public static class Extensions
    {
        public static void Sort(this ObservableCollection<Sentence> collection)
        {
            Sorting<Sentence>.action(collection);
        }
        public static void Sort(this ObservableCollection<Word> collection)
        {
            Sorting<Word>.action(collection);
        }
        public static void Sort(this ObservableCollection<char> collection)
        {
            Sorting<char>.action(collection);
        }

        private static class Sorting<T>
        {
            public static Action<ObservableCollection<T>> action =
            (ObservableCollection<T> collection) =>
            {
                List<T> sorted = collection.OrderBy(x => x.ToString()).ToList();
                for (int i = 0; i < sorted.Count(); i++)
                    collection.Move(collection.IndexOf(sorted[i]), i);
            };
        }
    }
}
