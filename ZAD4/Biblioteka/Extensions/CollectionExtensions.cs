using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Extensions {
    public static class CollectionExtensions {
        public static List<Book> GetBooksWithSpecifiedTitle(this Dictionary<int, Book> dic, string title) {
            List<Book> result =
                (from ks in dic
                 where ks.Value.Tytul == title
                 select ks.Value).ToList();

            return result;
        }

        public static List<Book> GetBooksWithSpecifiedTitleLamb(this Dictionary<int, Book> dic, string title) {
            return dic.Values.Where(x => x.Tytul == title).ToList();
        }

        public static List<Book> GetBooksWithSpecifiedIssueYear(this Dictionary<int, Book> dic, int minYear, int maxYear) {
            List<Book> result =
                (from ks in dic
                 where ks.Value.IssueYear >= minYear && ks.Value.IssueYear <= maxYear
                 select ks.Value).ToList();

            return result;
        }

        public static List<Book> GetBooksWithSpecifiedIssueYearLamb(this Dictionary<int, Book> dic, int minYear, int maxYear) {
            return dic.Values.Where(ks => ks.IssueYear >= minYear && ks.IssueYear <= maxYear).ToList();
        }

        public static List<SimpleClass> GetBooksWithSpecifiedIssueYear2(this Dictionary<int, Book> dic, int minYear, int maxYear) {
            List<SimpleClass> result =
                (from ks in dic
                 where ks.Value.IssueYear >= minYear && ks.Value.IssueYear <= maxYear
                 select new SimpleClass 
                    { stringValue = ks.Value.Tytul, intValue = ks.Value.IssueYear }
                 ).ToList();

            return result;
        }

        public static List<SimpleClass> GetBooksWithSpecifiedIssueYear2Lamb(this Dictionary<int, Book> dic, int minYear, int maxYear) {
            return dic.Where(ks => ks.Value.IssueYear >= minYear && ks.Value.IssueYear <= maxYear)
                .Select(ks => new SimpleClass { stringValue = ks.Value.Tytul, intValue = ks.Value.IssueYear })
                .ToList();
        }

        public static List<string> GetAllAuthors(this Dictionary<int, Book> dic) {
            List<string> result =
                (from ks in dic
                 select ks.Value.Autor).Distinct().ToList();

            return result;
        }

        public static List<string> GetAllAuthorsLamb(this Dictionary<int, Book> dic) {
            return dic.Values.Select(x => x.Autor).Distinct().ToList();
        }

        public static Book GetLatestElement(this Dictionary<int, Book> dic) {
            return (from b in dic
                    orderby b.Value.IssueYear descending
                    select b).First().Value;
        }

        public static Book GetLatestElementLamb(this Dictionary<int, Book> dic){
            return dic.OrderByDescending(i => i.Value.IssueYear).First().Value;
        }

        public static Reader[] GetReadersWithBorrows(this List<Reader> list) {
            Reader[] result =
                (from read in list
                 where read.Borrows.Count > 0
                 select read).ToArray();
            return result;
        }

        public static Reader[] GetReadersWithBorrowsLamb(this List<Reader> list) {
            return list.Where(x => x.Borrows.Count > 0).ToArray();
        }

        public static List<Borrow> GetDistinctBorrows (this ObservableCollection<Borrow> oc){
            var result = 
                (from cust in oc
                group cust by new {cust.Czytelnik, cust.Ksiazka}).Select(grp => grp.First()).ToList();
            return result;
        }

        public static List<Borrow> GetDistinctBorrowsLamb(this ObservableCollection<Borrow> oc) {
            return oc.GroupBy(x => new { x.Ksiazka, x.Czytelnik }).Select(grp => grp.First()).ToList();
        }

        /*public static List<Wypozyczenie> Lambda_getDistinctBorrows(List<Wypozyczenie> list) {
            return list.Distinct().Select(w => w).ToList();
        }*/


        //Metody rozszarzajce wystepujace samodzielnie
        public static List<Reader> GetMostActiveReaders(this List<Reader> list, int N) {
            return (from r in list
                 orderby r.Borrows.Count descending
                 select r).Take(N).ToList();
        }

        public static List<Reader>[] Split(this List<Reader> source) {
            return source
                .Select((x, i) => new { Index = i, Value = x }) //nowa klasa anonimowa skladajaca sie z indeksu oraz wartosci w liscie.
                .GroupBy(x => x.Index / 10)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToArray();
        }

        public static List<Book> GetNotBorrowedBooks(this Dictionary<int, Book> dic) {
            List<Book> result =
                (from ks in dic
                 where ks.Value.Borrows.Count == 0
                 select ks.Value).ToList();

            return result;
        }

        public static void CompareLists(this Dictionary<int, Book> dic1, Dictionary<int, Book> dic2) {
            if (dic1.Count != dic2.Count) return;

            List<string> greater = dic1.Select((v, i) => ((v.Value.CompareTo(dic2[i]) < 0)
                ? v.Value + " < " + dic2[i]
                : null
                )).ToList();

            foreach (string str in greater)
                if(str!=null) Console.WriteLine(str);
        }
    }
}
