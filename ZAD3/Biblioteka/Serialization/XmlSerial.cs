using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Biblioteka.Serialization.Entities;

namespace Biblioteka.Serialization {
    public class XmlSerial : ISerializer {
        private SerialBasics sb = new SerialBasics();
        XmlDocument xmlDoc = new XmlDocument();

        public string Path { get; set; }
        public XmlSerial(string path) {
            Path = path;
        }

        public  void SerializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            SBase bas = new SBase(czytelnicy, ksiazki, wypozyczenia);

            try {
                using (Stream stream = File.Open(Path, FileMode.Create)) {
                    XmlSerializer xml = new XmlSerializer(typeof(SBase));
                    xml.Serialize(stream, bas);
                }
            } catch (IOException io) {
                Console.WriteLine(io.Message);
            }
            
        }

        public void DeserializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            using (StreamReader file = File.OpenText(Path)) {
                XmlSerializer xml = new XmlSerializer(typeof(SBase));
                SBase baza = (SBase)xml.Deserialize(file);
                sb.ResetAll(czytelnicy, ksiazki, wypozyczenia);
                sb.ConvertAll(czytelnicy, ksiazki, wypozyczenia, baza);
            }
            return;
        }
    }
}
