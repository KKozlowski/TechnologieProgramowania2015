using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Biblioteka.Serialization.Entities;
using System.IO;
using Newtonsoft.Json;

namespace Biblioteka.Serialization {
    public class JsonSerial : ISerializer {
        public string Path { get; set; }
        private SerialBasics sb = new SerialBasics();
        public JsonSerial(string path) {
            Path = path;
        }

        public void SerializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            SBase bas = new SBase(czytelnicy, ksiazki, wypozyczenia);

            using (FileStream fs = File.Open(Path, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw)) {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, bas);
            }
        }

        public void DeserializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            using (StreamReader file = File.OpenText(Path))
            {
                JsonSerializer serializer = new JsonSerializer();
                SBase baza = (SBase)serializer.Deserialize(file, typeof(SBase));
                sb.ResetAll(czytelnicy, ksiazki, wypozyczenia);
                sb.ConvertAll(czytelnicy, ksiazki, wypozyczenia, baza);
                
            }
        }
    }
}
