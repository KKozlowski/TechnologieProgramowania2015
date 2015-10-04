using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using System.Collections.ObjectModel;

namespace Applic {
    class ViewModelBorrowEditor : ViewModelBase {
        private DateTime date;
        public DateTime Date {
            get { return date; }
            set {
                date = value;
                RaisePropertyChanged("Date");
            }
        }

        public Reader ChosenReader { get; set; }
        public Book ChosenBook { get; set; }

        public Collection<Reader> ReadersObjects { get; set; }
        public Collection<Book> BooksObjects { get; set; }

        public Book Book { get; private set; }
        public ViewModelMainWindow Main { get; private set; }

        private string title, author, issueYear;
        private int integerIssue;
        public string Title {
            get { return title; }
            set {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Author {
            get { return author; }
            set {
                author = value;
                RaisePropertyChanged("Author");
            }
        }

        public string IssueYear {
            get { return issueYear; }
            set {
                issueYear = value;
                
                RaisePropertyChanged("IssueYear");
            }
        }

        public RelayCommand ButtonClicked { get; set; }

        private string buttonLabel;
        public string ButtonLabel {
            get { return buttonLabel; }
            private set {
                buttonLabel = value;
                RaisePropertyChanged("ButtonLabel");
            }
        }

        private bool isSet = false;

        public ViewModelBorrowEditor() {
            Date = DateTime.Today;
        }

        public void SetAsAdder(ViewModelMainWindow mw) {
            Main = mw;
            prepareCombos(Main.Baza);
            if (!isSet) {
                ButtonLabel = "Add";
                ButtonClicked = new RelayCommand(ClickMeAdder);
                isSet = true;
                RaisePropertyChanged("ButtonClicked");
            }

        }

        /*public void SetAsEditor(ViewModelMainWindow mw, Book b) {
            Main = mw;
            if (!isSet) {
                Book = b;
                Title = b.Tytul;
                Author = b.Autor;
                IssueYear = b.IssueYear.ToString();
                ButtonLabel = "Save";
                ButtonClicked = new RelayCommand(ClickMeEditor);
                isSet = true;
                RaisePropertyChanged("ButtonClicked");
            }
        }*/

        private void ClickMeAdder(object o) {
            if (ChosenReader != null && ChosenBook != null) {
                Main.Baza.Add(new Borrow(ChosenBook, ChosenReader, Date));
                Main.UpdateBooksList();
                ((BorrowEditor)o).Close();
                Main.UpdateBorrowsList();
            }
        }

        /*private void ClickMeEditor(object o) {
            try {
                integerIssue = Int32.Parse(IssueYear);
            } catch (Exception e) {
                return;
            }
            Console.WriteLine(Book);
            Book.Tytul = Title;
            Book.Autor = Author;
            Book.IssueYear = integerIssue;
            Main.UpdateBooksList();
            ((BorrowEditor)o).Close();
        }*/

        private void prepareCombos(IBase b) {
            ReadersObjects = new Collection<Reader>();
            BooksObjects = new Collection<Book>();

            foreach (Reader r in b.Readers) {
                ReadersObjects.Add(r);
            }
            foreach (Book bo in b.Books) {
                BooksObjects.Add(bo);
            }
            RaisePropertyChanged("ReadersObjects");
            RaisePropertyChanged("BooksObjects");
        }
    }
}
