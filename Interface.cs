using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using vis.Mapper;
using vis.TableGateway;

namespace vis
{
    public class Interface
    {

        public void LoginSuccess()
        {
            string message = "Vítejte v Knižním světě.";
            
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            
        }
        
        public void RegisterSuccess()
        {
            string message = "Úspěšně jsme Vás zaregistrovali, nyní se prosím přihlaste";
            
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            
        }
        public void Logout(ref Uzivatel a)
        {
            string message = "Děkujeme, že nakupujete s námi.";
            
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            message = $"Přejeme krásný zbytek dne {a.Jmeno}.";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            a = null;
            System.Threading.Thread.Sleep(3000);
            
        }
        
        public static void EditUzivatelMenu(Uzivatel a)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Editace Uzivatele\n");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Uzivatel: "+ a.Jmeno +" "+ a.Prijmeni);
            Console.WriteLine("Email: "+ a.Email);
            Console.WriteLine("Adresa: "+ a.Ulice + $" , {a.Mesto}, {a.Psc}, {a.Zeme}");
            Console.WriteLine("Telefon: "+ a.Telefon + "\n");
            
            Console.WriteLine("Vyberte možnost co chcete upravit: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1"); Console.WriteLine(" - Pro změnu jména");
            Console.Write("2"); Console.WriteLine(" - Pro změnu příjmení");
            Console.Write("3"); Console.WriteLine(" - Pro změnu emailu");
            Console.Write("4"); Console.WriteLine(" - Pro změnu PSC");
            Console.Write("5"); Console.WriteLine(" - Pro změnu města");
            Console.Write("6"); Console.WriteLine(" - Pro změnu země");
            Console.Write("7"); Console.WriteLine(" - Pro změnu hesla");
            Console.Write("8"); Console.WriteLine(" - Pro změnu telefonu");
            Console.Write("9"); Console.WriteLine(" - Pro změnu ulice");
            if (a.role_id == 0)
            {
               Console.Write("10"); Console.WriteLine(" - Pro změnu role"); 
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0 - Pro opuštění editace");

        }

        public void Welcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            string message = "Vítejte";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            message = "Prosím zvolte zda se chcete přihlásit, nebo registrovat";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message+"\n");
            message = "0 - Pro přihlášení";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
            message = "1 - Pro registraci";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message+"\n");
            message = "2 - Konec";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
        }


        public void WrongInput()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Špatně zadaný input, zkuste to prosím znovu.");
            System.Threading.Thread.Sleep(3000);
        }


        public void menu(Uzivatel a)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Logged as "+a.Jmeno+" "+ a.Prijmeni);
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.Write("1"); Console.WriteLine(" - Pro zobrazení výběru knih");
            Console.Write("2"); Console.WriteLine(" - Pro vytvoření objednávky");
            Console.Write("3"); Console.WriteLine(" - Pro zobrazení objednávek");
            Console.Write("4"); Console.WriteLine(" - Pro editaci profilu");
            Console.Write("5"); Console.WriteLine(" - Pro odhlášení");
            if (a.role_id == 1)
            {
                Console.Write("6"); Console.WriteLine(" - Pro zobrazení všech uživatelů");
                Console.Write("7"); Console.WriteLine(" - Pro zobrazení všech objednávek");
                Console.Write("7"); Console.WriteLine(" - Pro zobrazení všech knih");
            }

        }
        
        public static void printKnihy(List<Kniha> k)
        {
            Console.Clear();
            Console.WriteLine("ID\tKniha\tISBN\t\tCena\t\tDostupny Počet");
            for (int i = 0; i < k.Count; i++)
            {
                Console.WriteLine(k[i].Id+"\t"+k[i].Nazev+"\t"+k[i].Isbn+"\t"+k[i].Cena+"\t\t"+k[i].Pocet);
            }
        }

        public static void printObjednavkyUzivatele(Uzivatel u)
        {
            ObjednavkaMapper og = new ObjednavkaMapper();
            List<Objednavka> k = og.SelectByUser(u.Id);
            
            string s = "";
            
            while(true){
                Console.Clear();
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logged as "+u.Jmeno+" "+ u.Prijmeni+"\n");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine("ID\t\t" + "Čas\t\t" + "Celková cena");

                for (int i = 0; i < k.Count; i++)
                {
                    Console.WriteLine(k[i].Id+"\t" + k[i].Cas +"\t" + k[i].Celkova_cena);
                }
                
                Console.WriteLine("\nPro detail objednavky napište její ID");
                Console.WriteLine("\nPro ukončení napište quit");

                s = Console.ReadLine();

                if (s == "quit")
                {
                    return;
                }

                try
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        if (int.Parse(s) == k[i].Id)
                        {
                            printJedneObjednavkyUzivatele(k[i]);
                        }
                    }
                    

                }
                catch (FormatException)
                {
                    
                    continue;
                }
            }
        }

        public static void printJedneObjednavkyUzivatele(Objednavka o)
        {
            KnihaTableGateway kgt = new KnihaTableGateway();
            List<Kniha> k = kgt.SelectAll();

            PolozkaObjednavkyMapper pom = new PolozkaObjednavkyMapper();
            List<PolozkaObjednavky> l = pom.SelectByObjednavka(o.Id);
            
            Console.Clear();
            Console.WriteLine("ID: "+o.Id);
            Console.WriteLine("Celková cena objednávky: "+o.Celkova_cena);
            Console.WriteLine("Čas pořízení objednávky: "+o.Cas+"\n");
            Console.WriteLine("Položky objednávky: \n");
            Console.WriteLine("Nazev\tISBN\t\tCena\tPočet");
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < k.Count; j++)
                {
                    Console.WriteLine(i+"=="+j);
                    if (l[i].id_o == k[j].Id)
                    {
                        Console.WriteLine(k[j].Nazev + "\t" + k[j].Isbn + "\t" + l[i].cena + "\t" + l[i].pocet);
                    }
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPokračujte stisknutím ENTER");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
            
        }
        
        
    }
}