using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Biblioteka.Serialization;
using System.Collections.ObjectModel;

namespace Applic {
    class ViewModelSaveLoad : ViewModelBase {
        public Collection<string> SerializatorNames { get; set; }
        private ViewModelMainWindow vmmw;
        private IBase baza;
        public ViewModelMainWindow Main {
            get { return vmmw; }
            set {
                vmmw = value;
                baza = vmmw.Baza;
            }
        }

        public RelayCommand SaveClicked { get; set; }
        public RelayCommand LoadClicked { get; set; }

        public int ChosenIndex { get; set; }

        public string Path { get; set; }

        public ViewModelSaveLoad() {
            SerializatorNames = new Collection<string>();
            SerializatorNames.Add("XML");
            SerializatorNames.Add("JSON");
            SerializatorNames.Add("Binarny");
            SerializatorNames.Add("Paskudny");

            Path = "File.file";

            SaveClicked = new RelayCommand(Save);
            LoadClicked = new RelayCommand(Load);
        }

        public void Save(object o) {
            try {
                setSerializers();
                baza.Serialize();
            } catch (Exception e) {
                return;
            }
            ((SaveLoad)o).Close();
        }

        public void Load(object o) {
            try {
                setSerializers();
                baza.Deserialize();
            } catch (Exception e) {
                return;
            }
            Main.UpdateBooksList();
            Main.UpdateBorrowsList();
            Main.UpdateReadersList();
            ((SaveLoad)o).Close();
        }

        private void setSerializers() {
            switch (ChosenIndex) {
                case 0:
                    baza.Serializer = new XmlSerial(Path);
                    break;
                case 1:
                    baza.Serializer = new JsonSerial(Path);
                    break;
                case 2:
                    baza.Serializer = new BinarySerial(Path);
                    break;
                case 3:
                    baza.Serializer = new SerialSerial(Path);
                    break;
            }
        }
    }
}
