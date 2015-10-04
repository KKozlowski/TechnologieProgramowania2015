using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Biblioteka.Serialization;
using Extensions;
using Applic;

namespace Program
{

    class Program 
    {
        [STAThread]
        static void Main(string[] args){
            MainWindow mw = new MainWindow();
            mw.Show();
            IBase baza = new Baza(new RandomFiller(30), new XmlSerial("XML.xml"));
            baza.Add(new Borrow(baza.GetBookById(3), baza.GetReaderById(3)));
            baza.Add(new Borrow(baza.GetBookById(3), baza.GetReaderById(3)));
            /*var dist = baza.GetDistinctBorrows();
            foreach (var bor in dist) Console.WriteLine("DISTINCT: " + bor);*/
            Inputter inp = new Inputter();
            baza.AnulujWypozyczenieNr(0);
            //baza.Write();
            Console.WriteLine();
            Console.WriteLine("print, stat, +reader, -reader, +book, -book, +borrow, -borrow, stop,");
            Console.WriteLine("bookswithtitle, bookswithyear, readerswithborrows, latestbook, distinctborrows");
            Console.WriteLine("save, load, setXML, setJSON, setBIN, setTERRIBLE");

            string wybor;
            while (true) {
                Console.WriteLine("\nWybierz komende:");
                wybor = Console.ReadLine();
                if (wybor == "STOP" || wybor == "stop") {
                    Console.WriteLine("Stop.");
                    break;
                }
                switch (wybor) {
                    case "print":
                        baza.Write();
                        break;

                    case "stat":
                        Console.WriteLine("Liczba ksiazek: " + baza.LiczbaKsiazek);
                        Console.WriteLine("Liczba czytalnikow: " + baza.LiczbaCzytelnikow);
                        Console.WriteLine("Liczba wypozyczen: " + baza.LiczbaWypozyczen);
                        break;

                    case "+reader":
                        Console.Write("Imie: ");
                        string imie = Console.ReadLine();
                        Console.Write("Nazwisko: ");
                        string nazwisko = Console.ReadLine();
                        int id = -1;
                        if (!inp.GetInt("ID (nonpositive to get receive automatically): ", out id)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }
                        if (id<0)
                            baza.Add(new Reader(imie, nazwisko));
                        else {
                            Console.WriteLine(id);
                            try {
                                Reader nowy = new Reader(imie, nazwisko, id);
                                baza.Add(nowy);
                            } catch (Exception e) {
                                Console.WriteLine(e.Message);
                                break;
                            }
                            
                        }
                        
                        break;

                    case "-reader":
                        int idr;
                        if (!inp.GetInt("ID (nonpositive will cancel): ", out idr)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }
                        if (idr < 0)
                            break;
                        else {
                            try {
                                baza.RemoveReaderWithId(idr);
                            } catch (Exception e) {
                                Console.WriteLine(e.Message);
                                break;
                            }
                        }

                        break;

                    case "+book":
                        Console.Write("Tytul: ");
                        string tutul = Console.ReadLine();
                        int idb;
                        if (!inp.GetInt("ID: ", out idb) || idb < 0) { 
                            Console.WriteLine("Invalid number");
                            break;
                        }
                        try{
                            baza.Add(new Book(idb, tutul));
                        } catch (ArgumentException ae) {
                            Console.WriteLine(ae.Message + " Wcześniej.");
                        }

                        break;

                    case "-book":
                        int idb2;
                        if (!inp.GetInt("ID: ", out idb2)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }

                        baza.RemoveBookWithId(idb2);
                        break;
                    
                    case "+borrow":
                        int ksID, czID;
                        if (!inp.GetInt("Book ID: ", out ksID)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }

                        if (!inp.GetInt("Reader ID: ", out czID)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }
                        try {
                            baza.Add(new Borrow(baza.GetBookById(ksID), baza.GetReaderById(czID)));
                        } catch (KeyNotFoundException ke) {
                            Console.WriteLine(ke.Message);
                        }
                        break;

                    case "-borrow": {
                            int wypID;
                            if (!inp.GetInt("Number of Borrow Receipt on the list: ", out wypID)) {
                                Console.WriteLine("Invalid number");
                                break;
                            }
                            baza.AnulujWypozyczenieNr(wypID);
                        }
                        break;

                    case "bookswithtitle":{
                        Console.Write("Title of the book: ");
                        string title = Console.ReadLine();
                        var ksiazki = baza.GetBooksWithSpecifiedTitle(title);
                        foreach (var k in ksiazki)
                            Console.WriteLine(k);
                        }
                        break;

                    case "bookswithyear": {
                        int minYear, maxYear;
                        if (!inp.GetInt("Minimum issue year: ", out minYear)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }

                        if (!inp.GetInt("Maximum issue year: ", out maxYear)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }
                        var ksiazki = baza.GetBooksWithSpecifiedIssueYear(minYear, maxYear);
                        foreach (var k in ksiazki)
                            Console.WriteLine(k);
                        }
                        break;

                    case "readerswithborrows": {
                        var readers = baza.GetReadersWithBorrows();
                            foreach (var k in readers)
                                Console.WriteLine(k + " (" + k.Borrows.Count + " borrows)");
                        }
                        break;

                    case "latestbook":
                        Console.WriteLine(baza.GetLatestBook());
                        break;

                    case "distinctborrows": {
                        var borrows = baza.GetDistinctBorrows();
                            foreach (var k in borrows)
                                Console.WriteLine(k.Ksiazka + " -> " + k.Czytelnik );
                        }
                        break;
                    case "save":
                        baza.Serialize();
                        Console.WriteLine("Database saved to file.");
                        break;
                    case "load":
                        try {
                            baza.Deserialize();
                            Console.WriteLine("Database loaded from file.");
                        } catch (Exception e) {
                            Console.WriteLine("Problems occured during file loading.");
                        }
                        break;
                        //setXML, setJSON, setBIN, setTERRIBLE
                    case "setXML":
                        baza.Serializer = new XmlSerial("XML.xml");
                        break;
                    case "setJSON":
                        baza.Serializer = new JsonSerial("JSON.json");
                        break;
                    case "setBIN":
                        baza.Serializer = new BinarySerial("BIN.bin");
                        break;
                    case "setTERRIBLE":
                        baza.Serializer = new SerialSerial("SERIAL.terrible");
                        break;
                }
            }
        }
    }
}
