using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class RandomFiller : IFiller
    {
        Random radom = new Random();
        public static string[] imiona = {"Jan", "Błażej", "Aleksander", "Hiacynt", "Wiktor", "Monika", "Wiktoria", "Genowefa", "Anna", "Martyna"};
        public static string[] nazwiska = { "Nowak", "Pająk", "Stefanowicz", "Musiał" };
        public static string[] tytuly = { "Wojna i Pokój", "Ogniem i Mieczem", "Duma i Uprzedzenie", "Całki i Różniczki" };

        private int numberOfPos = 10;
        public int NumberOfPositions {
            get { return numberOfPos; }
            set { if (value >= 0) numberOfPos = value; }
        }

        public string GetRandomString(string[] tab) {
            int num = radom.Next(tab.Length);
            return tab[num];
        }

        public RandomFiller() { }
        public RandomFiller(int ile) {
            NumberOfPositions = ile;
        }

        public void Fill(List<Czytelnik> lst, Dictionary<int, Ksiazka> dic, ObservableCollection<Wypozyczenie> oc)
        {
            FillList(lst);
            FillDictionary(dic);

            for (int i = 0; i < NumberOfPositions; i++) {
                oc.Add(new Wypozyczenie(dic[radom.Next(dic.Count)], lst[radom.Next(lst.Count)]));
            }
        }

        public void FillList(List<Czytelnik> lst) {
            for (int i = 0; i < NumberOfPositions; i++) {
                lst.Add(new Czytelnik(GetRandomString(imiona), GetRandomString(nazwiska)));
            }
        }

        public void FillDictionary(Dictionary<int, Ksiazka> dic) {
            for (int i = 0; i < NumberOfPositions; i++) {
                dic.Add(i, new Ksiazka(i, GetRandomString(tytuly)));
            }
        }
    }
}
