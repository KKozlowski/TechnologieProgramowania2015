using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;  //!!!!!

namespace safeprojectname
{
    class Program
    {
        class Klasa
        {
            public String wlasciwosc
            {
                get { return "hello"; }
            }

            private long wlasc2 = 3;
            public long Wlasciwosc2 {
                get { return wlasc2; }
                set { wlasc2 = value; }
            }

            private void metoda1(){
                System.Console.WriteLine("metoda1");
            }
            public void metoda2(String a)
            {
                System.Console.WriteLine("metoda2 " + a);
            }
            public int metoda3(int a, int b)
            {
                return (a + b);
            }
        }

        class Prywatyzacja {
            private int Raz;
            private string Dwa;
            private int Trzy;
            public int MMM;
            public static int I = 0;

            public Prywatyzacja(int i, string s) {
                Raz = i;
                Dwa = s;
                Trzy = I++;
                MMM = 2 * Trzy;
            }

            public int getRaz() {
                return Raz;
            }

            public int getCztery() {
                return Raz;
            }

            public string getHue() {
                return Dwa;
            }

            public int getCosJeszcze() {
                return Trzy;
            }
        }

        static void Main(string[] args)
        {
            // Przykład 1111111111
            int i = 3;
            Type type1 = i.GetType();
            Console.WriteLine("Pierwszy typ: " + type1); //W oknie konsoli zostanie wypisany tekts System.Int32

            // Przykład 2222222222
            Klasa klasa = new Klasa();            
            Type type2 = klasa.GetType();
            PropertyInfo[] properties = type2.GetProperties();
            //pobieramy właściwości typu "type2", brak argumentu w metodzie GetPropierties sprawia, iż będą pobrane domyślne(publiczne)
            //można to zmienić za wstawiając różne wartości enuma BindingFlags (np. BindingFlags.Public, BindingFlags.Instance)
            System.Console.WriteLine("Właściwości drugiego typu (Klasa): ");
            foreach (PropertyInfo pi in properties)
                Console.WriteLine(pi);
            

            // Przykład 333333333

            MethodInfo[] methods = type2.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance); //tylko metody nie publiczne
            MethodInfo[] methodsPub = type2.GetMethods(); //metody publiczne wraz ze statycznymi
            Console.WriteLine("\nMetody prywatne Klasy: " + methods.Length);
            foreach (MethodInfo m in methods)
                Console.WriteLine(m);
            System.Console.WriteLine("Metody publiczne Klasy: " + methodsPub.Length);
            foreach (MethodInfo m in methodsPub)
                Console.WriteLine(m);
            Console.WriteLine();

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //Używanie obiektów na podstawie metadanych, a nie defincji
            var instancja = Activator.CreateInstance(type2); //inicjalizacja obiektu określonego typu
            MethodInfo inf = type2.GetMethod("metoda2");
            inf.Invoke(instancja,new object[]{"123"}); //metoda Invoke przyjmuje obiekt oraz tablice obiektów które są parametrami metody "metoda2"
            //zostanie tu wywołana "metoda2" z parametrem "123" i wypisane zostanie metoda2 123
            Console.WriteLine();
            ////////////////////////////////////////////////////////////////////////////////////////////////
            //Przykład użycia
            przykladUzycia(klasa);
            Console.WriteLine();
            SerializacjaDeserializacja();
            Console.Read();
            
        }
        public static void przykladUzycia(Object o)
        {
            int i = 0;
            Random v = new Random();

            Type type = o.GetType();
            MethodInfo[] methods = type.GetMethods(); //tabela zawierająca wszystkie metody
            while (methods[i].ReturnType != i.GetType()) //znajdujemy metodę zwracającą typ taki jak typ zmiennej i (czyli Int32)
            {
                i++;
            }
            var me = methods[i]; //dodatkowe przechowanie tej metody
            System.Console.WriteLine(me.ToString()); //wypisanie Int32 metoda3(Int32, Int32)
            object[] obj = new object[me.GetParameters().Length]; //tworzymy tabelę objektów potrzebną później do wykonania metody
            for (int j = 0; j < obj.Length; j++)
            {
                obj[j] = v.Next(1, 10);
            }
            System.Console.WriteLine(me.Invoke(o, obj)); //wypisanie wykonanej metody
        }


        public static void SerializacjaDeserializacja() {
            //Nowy obiekt
            Prywatyzacja p1 = new Prywatyzacja(1, "SS");
            Console.WriteLine(p1.getCztery() + " " + p1.getHue() + " " + p1.getCosJeszcze() + " " + p1.MMM);

            //Zapisywanie wartości pól prywatnych
            Type pry = p1.GetType();
            FieldInfo[] fields = pry.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Hashtable values = new Hashtable();

            for (int i = 0; i < fields.Length; i++ )
                values.Add(i, fields[i].GetValue(p1));

            //Zapisywanie wartości pól publicznych i statycznych.
            FieldInfo[] fieldsPub = pry.GetFields();
            Hashtable valuesPub = new Hashtable();

            for (int i = 0; i < fieldsPub.Length; i++)
                valuesPub.Add(i, fieldsPub[i].GetValue(p1));

            //Tworzenie nowego obiektu. Zmiana zmiennej statycznej
            Prywatyzacja.I = 12;
            Prywatyzacja p2 = new Prywatyzacja(2, "SSS");
            Console.WriteLine("\n"+p2.getCztery() + " " + p2.getHue() + " " + p2.getCosJeszcze() + " " + p2.MMM);
            Console.WriteLine("I = " + Prywatyzacja.I);

            //Wczytywanie zapisanych wartości do nowego obiektu.
            for (int i = 0; i < fields.Length; i++) {
                fields[i].SetValue(p2, values[i]);
            }

            for (int i = 0; i < fieldsPub.Length; i++) {
                fieldsPub[i].SetValue(p2, valuesPub[i]);
            }


            Console.WriteLine("\n"+p2.getCztery() + " " + p2.getHue() + " " + p2.getCosJeszcze() + " " + p2.MMM);
            Console.WriteLine("I = " + Prywatyzacja.I);
        }
    }
}
