using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;



namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            IBase baza = new Baza(new MassFiller(15));
            //baza.Add(new Czytelnik("a", "k", 333));
            Inputter inp = new Inputter();
            baza.AnulujWypozyczenieNr(0);
            //baza.Write();
            Console.WriteLine();
            Console.WriteLine("print, stat, +reader, -reader, +book, -book, +rent, -rent");
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
                            baza.Add(new Czytelnik(imie, nazwisko));
                        else {
                            Console.WriteLine(id);
                            try {
                                Czytelnik nowy = new Czytelnik(imie, nazwisko, id);
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
                            baza.Add(new Ksiazka(idb, tutul));
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
                    
                    case "+rent":
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
                            baza.Add(new Wypozyczenie(baza.GetBookById(ksID), baza.GetReaderById(czID)));
                        } catch (KeyNotFoundException ke) {
                            Console.WriteLine(ke.Message);
                        }
                        break;

                    case "-rent":
                        int wypID;
                        if (!inp.GetInt("Number of Rent Receipt on the list: ", out wypID)) {
                            Console.WriteLine("Invalid number");
                            break;
                        }
                        baza.AnulujWypozyczenieNr(wypID);
                        break;
                }
            }
        }
    }
}
