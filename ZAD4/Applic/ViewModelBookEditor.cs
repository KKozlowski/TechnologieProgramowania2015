using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Applic {
    class ViewModelBookEditor : ViewModelBase {
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

        public ViewModelBookEditor() {
            Title = "Tytuł";
        }

        private bool isSet = false;

        public void SetAsAdder(ViewModelMainWindow mw) {
            Main = mw;
            if (!isSet) {
                ButtonLabel = "Add";
                ButtonClicked = new RelayCommand(ClickMeAdder);
                isSet = true;
                RaisePropertyChanged("ButtonClicked");
            }

        }

        public void SetAsEditor(ViewModelMainWindow mw, Book b) {
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
        }

        private void ClickMeAdder(object o) {
            try {
                integerIssue = Int32.Parse(IssueYear);
            } catch (Exception e) {
                return;
            }
            Main.Baza.Add(new Book(Main.Baza.Books.Count, Title, integerIssue, Author));
            Main.UpdateBooksList();
            ((BookEditor)o).Close();
        }

        private void ClickMeEditor(object o) {
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
            ((BookEditor)o).Close();
        }
    }
}
