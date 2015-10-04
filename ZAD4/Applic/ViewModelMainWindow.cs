using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Biblioteka.Serialization;
using System.Windows;

namespace Applic {
    class ViewModelMainWindow : ViewModelBase {
        public IBase Baza { get; private set; }

        public List<Reader> Readers { get { return Baza.Readers; }  }
        public List<Book> Books { get { return Baza.Books; } }
        public List<Borrow> Borrows { get { return Baza.Borrows; } }

        public Reader ChosenReader { get; set; }
        public Book  ChosenBook { get; set; }
        public Borrow ChosenBorrow { get; set; }

        public RelayCommand AddReaderClicked { get; set; }
        public RelayCommand EditReaderClicked { get; set; }
        public RelayCommand RemoveReaderClicked { get; set; }

        public RelayCommand AddBookClicked { get; set; }
        public RelayCommand EditBookClicked { get; set; }
        public RelayCommand RemoveBookClicked { get; set; }

        public RelayCommand AddBorrowClicked { get; set; }
        public RelayCommand EditBorrowClicked { get; set; }
        public RelayCommand RemoveBorrowClicked { get; set; }

        public RelayCommand ExitClicked { get; set; }
        public RelayCommand SaveLoadClicked { get; set; }

        public void OpenReaderAdder(object o) {
            ReaderEditor re = new ReaderEditor();
            ViewModelReaderEditor redc = re.DataContext as ViewModelReaderEditor;
            redc.SetAsAdder(this);
            re.ShowDialog();
        }

        public void OpenReaderEditor(object o) {
            if (ChosenReader == null) return;
            ReaderEditor re = new ReaderEditor();
            ViewModelReaderEditor redc = re.DataContext as ViewModelReaderEditor;
            redc.SetAsEditor(this, ChosenReader);
            re.ShowDialog();
        }

        public void RemoveReader(object o) {
            if (ChosenReader != null) {
                Baza.Remove(ChosenReader);
                UpdateReadersList();
            }
        }

        public void OpenBookAdder(object o) {
            BookEditor re = new BookEditor();
            ViewModelBookEditor redc = re.DataContext as ViewModelBookEditor;
            redc.SetAsAdder(this);
            re.ShowDialog();
        }

        public void OpenBookEditor(object o) {
            if (ChosenBook == null) return;
            BookEditor re = new BookEditor();
            ViewModelBookEditor redc = re.DataContext as ViewModelBookEditor;
            redc.SetAsEditor(this, ChosenBook);
            re.ShowDialog();
        }

        public void RemoveBook(object o) {
            Console.Write(ChosenBook);
            if (ChosenBook != null) {
                Baza.Remove(ChosenBook);
                UpdateBooksList();
            }
        }

        public void OpenBorrowAdder(object o) {
            BorrowEditor be = new BorrowEditor();
            ViewModelBorrowEditor bedc = be.DataContext as ViewModelBorrowEditor;
            bedc.SetAsAdder(this);
            be.ShowDialog();
        }

        public void RemoveBorrow(object o) {
            if (ChosenBorrow != null) {
                Baza.Remove(ChosenBorrow);
                UpdateBorrowsList();
            }
        }

        public ViewModelMainWindow() {
            Baza = new Baza(new RandomFiller(30), new XmlSerial("XML.xml"));
         

            AddReaderClicked = new RelayCommand(OpenReaderAdder);
            EditReaderClicked = new RelayCommand(OpenReaderEditor);
            RemoveReaderClicked = new RelayCommand(RemoveReader);

            AddBookClicked = new RelayCommand(OpenBookAdder);
            EditBookClicked = new RelayCommand(OpenBookEditor);
            RemoveBookClicked = new RelayCommand(RemoveBook);

            AddBorrowClicked = new RelayCommand(OpenBorrowAdder);
            RemoveBorrowClicked = new RelayCommand(RemoveBorrow);

            ExitClicked = new RelayCommand(Exit);
            SaveLoadClicked = new RelayCommand(OpenSaveLoad);
        }

        public void UpdateReadersList() {
            RaisePropertyChanged("Readers");
        }

        public void UpdateBooksList() {
            RaisePropertyChanged("Books");
        }

        public void UpdateBorrowsList() {
            RaisePropertyChanged("Borrows");
        }

        public void Exit(object o) {
            Console.WriteLine("EXIT.");
            Application.Current.Shutdown();
        }

        public void OpenSaveLoad(object o) {
            SaveLoad sl = new SaveLoad();
            ViewModelSaveLoad sldc = sl.DataContext as ViewModelSaveLoad;
            sldc.Main = this;
            
            sl.ShowDialog();
        }
    }
}
