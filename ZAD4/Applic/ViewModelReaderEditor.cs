using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Applic {
    class ViewModelReaderEditor : ViewModelBase {
        public string name, surname;
        public string Name {
            get { return name; }
            set {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Surname {
            get { return surname; }
            set {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        public RelayCommand ButtonClicked { get; set; }

        private string buttonLabel;
        public string ButtonLabel { 
            get {return buttonLabel;}
            private set{
                buttonLabel = value;
                RaisePropertyChanged("ButtonLabel");
            }
        }

        public ViewModelMainWindow Main {
            get;
            private set;
        }

        public Reader Reader {
            get;
            private set;
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

        public void SetAsEditor(ViewModelMainWindow mw, Reader r) {
            Main = mw;
            if (!isSet) {
                Reader = r;
                Name = r.Imie;
                Surname = r.Nazwisko;
                ButtonLabel = "Save";
                ButtonClicked = new RelayCommand(ClickMeEditor);
                isSet = true;
                RaisePropertyChanged("ButtonClicked");
            }
        }

        private void ClickMeAdder(object o) {
            Main.Baza.Add(new Reader(Name, Surname));
            Main.UpdateReadersList();
            Console.WriteLine(Surname);
            ((ReaderEditor)o).Close();
            
            
        }

        private void ClickMeEditor(object o) {
            Console.WriteLine(Reader);
            Reader.Imie = Name;
            Reader.Nazwisko = Surname;
            Main.UpdateReadersList();
            Console.WriteLine(Surname);
            ((ReaderEditor)o).Close();
        }

        public ViewModelReaderEditor() {
            
        }
    }
}
