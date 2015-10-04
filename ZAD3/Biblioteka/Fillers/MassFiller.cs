using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Biblioteka {
    public class MassFiller : IFiller {
        Random radom = new Random();
        private int numberOfPos = 10000;
        public int NumberOfPositions {
            get { return numberOfPos; }
            set { if (value >= 0) numberOfPos = value; }
        }

        char[] samogloski = { 'a', 'e', 'u', 'o', 'i', 'y' };
        char[] spolgloski = { 'b', 'c', 'd', 'f', 'g', 'h', 'k', 'l', 'm', 'n', 'p', 'r', 'z' };

        public MassFiller() { }
        public MassFiller(int ile) {
            NumberOfPositions = ile;
        }

        public void Fill(List<Reader> lst, Dictionary<int, Book> dic, ObservableCollection<Borrow> oc) {
            FillList(lst);
            FillDictionary(dic);

            for (int i = 0; i < NumberOfPositions; i++) {
                oc.Add(new Borrow(dic[radom.Next(dic.Count)], lst[radom.Next(lst.Count)]));
            }
        }

        public void FillList(List<Reader> lst) {
            for (int i = 0; i < NumberOfPositions; i++) {
                lst.Add(new Reader(randomName(3), randomName(3)));
                //lst.Add(new Czytelnik("Bob","Bob")); //Wzrost wydajnosci o 25%
            }
        }

        public void FillDictionary(Dictionary<int, Book> dic) {
            for (int i = 0; i < NumberOfPositions; i++) {
                dic.Add(i, new Book(i, randomName(8), 1730 + radom.Next(285)));
            }
        }

        private char randomSamogl() {
            return samogloski[radom.Next(samogloski.Length)];
        }

        private char randomSpol() {
            return spolgloski[radom.Next(spolgloski.Length)];
        }

        private string randomName(int length) {
            char[] nazw = new char[length];
            nazw[0] = Char.ToUpper(randomSpol());
            for (int i = 1; i < length; i++) {
                nazw[i] = i % 2 == 1 ? randomSamogl() : randomSpol();
            }

            return new string(nazw);
        }
    }
}
