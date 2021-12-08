using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Enumeration;
using System.Text;
using vis.Gateway;
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
            Console.ForegroundColor = ConsoleColor.White;
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
            Console.ForegroundColor = ConsoleColor.White;
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

        public static void LoggedAsAnonym()
        {

                Console.ForegroundColor = ConsoleColor.Red;
                string message = "Pouze pro registrované uživatele!";
                Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
                Console.WriteLine(message);
                message = "Pokud chcete pokračovat přihlašte se, nebo se zaregistrujte!\n";
                Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(5000);
            
        }
        
        public static void EditUzivatelMenu(Uzivatel a,int role = 2)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Editace Uzivatele\n");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Uzivatel: "+ a.Jmeno +" "+ a.Prijmeni);
            Console.WriteLine("Email: "+ a.Email);
            Console.WriteLine("Adresa: "+ a.Adr.Ulice + $" , {a.Adr.Mesto}, {a.Adr.Psc}, {a.Adr.Zeme}");
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
            if (role == 1)
            {
               Console.Write("10"); Console.WriteLine(" - Pro změnu role"); 
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n0 - Pro opuštění editace");
            Console.ForegroundColor = ConsoleColor.White;
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
            Console.WriteLine(message);
            message = "2 - Anonymní režim";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
            message = "3 - Zapomenuté heslo";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message+"\n");
            message = "4 - Konec";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
        }

        public void WrongInput()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Špatně zadaný input, zkuste to prosím znovu.");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(3000);
        }


        public void menu(Uzivatel a)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Logged as "+a.Jmeno+" "+ a.Prijmeni);
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.Write("\n1"); Console.WriteLine(" - Pro zobrazení výběru knih");
            Console.Write("2"); Console.WriteLine(" - Pro vytvoření objednávky");
            Console.Write("3"); Console.WriteLine(" - Pro zobrazení objednávek");
            Console.Write("4"); Console.WriteLine(" - Pro editaci profilu");
            if (a.role_id == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("5"); Console.WriteLine(" - Pro zobrazení všech uživatelů");
                Console.Write("6"); Console.WriteLine(" - Pro zobrazení všech objednávek");
                Console.Write("7"); Console.WriteLine(" - Pro zobrazení všech knih");
                Console.Write("8"); Console.WriteLine(" - Pro přidání nové knihy");
                Console.Write("9"); Console.WriteLine(" - Pro odebrání knihy");
                Console.Write("10"); Console.WriteLine(" - Pro odebrání uživatele");
                Console.Write("11"); Console.WriteLine(" - Pro odebrání objednávky");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n0"); Console.WriteLine(" - Pro odhlášení");
            Console.ForegroundColor = ConsoleColor.White;
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
                    Console.WriteLine(k[i].Id+"\t" + k[i].Cas +"\t" + k[i].Celkova_cena+" kč");
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
                            printJedneObjednavkyUzivatele(k[i],u);
                        }
                    }
                    

                }
                catch (FormatException)
                {
                    
                    continue;
                }
            }
        }
        
        public static void printAllObjednavky(Uzivatel u)
        {
            ObjednavkaTableGateway og = new ObjednavkaTableGateway();
            List<Objednavka> k = og.SelectAll();
            
            string s = "";
            
            while(true){
                Console.Clear();
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logged as "+u.Jmeno+" "+ u.Prijmeni+"\n");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine("ID\t\t" + "Čas\t\t" + "Celková cena");

                for (int i = 0; i < k.Count; i++)
                {
                    Console.WriteLine(k[i].Id+"\t" + k[i].Cas +"\t" + k[i].Celkova_cena+" kč");
                }
                
                Console.WriteLine("\nPro detail objednavky napište její ID");
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPro ukončení napište quit");
                Console.ForegroundColor = ConsoleColor.White;
                

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
                            printJedneObjednavkyUzivatele(k[i],u);
                        }
                    }
                    

                }
                catch (FormatException)
                {
                    
                    continue;
                }
            }
        }
        
        public static void printJedneObjednavkyUzivatele(Objednavka o,Uzivatel u)
        {
            KnihaTableGateway kgt = new KnihaTableGateway();
            List<Kniha> k = kgt.SelectAll();
            Uzivatel kupujici = null;
            string s = "";
            StringBuilder str = new StringBuilder();
            
            if (u.role_id == 1)
            { 
                UzivatelMapper m = new UzivatelMapper();
                kupujici = m.SelectById(o.Uzivatel_id_u);
            }
            
            PolozkaObjednavkyMapper pom = new PolozkaObjednavkyMapper();
            List<PolozkaObjednavky> l = pom.SelectByObjednavka(o.Id);
            
            Console.Clear();
            Console.WriteLine("ID: "+o.Id);
            str.AppendLine("ID: "+o.Id);
            Console.WriteLine("Celková cena objednávky: "+o.Celkova_cena+" kč");
            str.AppendLine("Celková cena objednávky: "+o.Celkova_cena+" kč");
            Console.WriteLine("Čas pořízení objednávky: "+o.Cas);
            str.AppendLine("Čas pořízení objednávky: "+o.Cas);

            if (kupujici != null)
            {
                Console.WriteLine("Kupující: "+kupujici.Jmeno + " " +kupujici.Prijmeni+"\n");
                str.AppendLine("Kupující: "+kupujici.Jmeno + " " +kupujici.Prijmeni);
            }
            else
            {
                str.AppendLine("Kupující: "+u.Jmeno + " " +u.Prijmeni);
            }
            
            
            Console.WriteLine("Položky objednávky: \n");
            str.AppendLine("Položky objednávky: ");
            Console.WriteLine("Nazev\tISBN\t\tCena(kč)\tPočet");
            str.AppendLine("Nazev\t\tISBN\t\tCena(kč)\tPočet");
           
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < k.Count; j++)
                {
                    if (l[i].Id_k == k[j].Id)
                    {
                        str.AppendLine(k[j].Nazev + "\t" + k[j].Isbn + "\t" + l[i].cena + "\t\t\t" + l[i].pocet);
                        Console.WriteLine(k[j].Nazev + "\t" + k[j].Isbn + "\t" + l[i].cena + "\t\t" + l[i].pocet);
                        
                    }
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPro ukončení napište quit");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPro export faktury napište tisk");
            Console.ForegroundColor = ConsoleColor.White;
            
            s = Console.ReadLine();

            if (s == "quit")
            {
                return;
            }
            
            if (s == "tisk")
            {
                Export.ExportInvoiceTxt("Invoice.txt",str.ToString());

                Console.ForegroundColor = ConsoleColor.Red;
                string message = "Váše faktura byla vyexportována.\n";
                Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(3000);

            }
            
        }

        public static void MenuEditaceKniha(ref Kniha k)
        {
            
            Console.WriteLine("Editovaná kniha: \n");
            
            Console.WriteLine("Název: " + k.Nazev);
            Console.WriteLine("ISBN: " + k.Isbn);
            Console.WriteLine("Cena: " + k.Cena + " kč");
            Console.WriteLine("Počet na skladě: " + k.Pocet);
            Console.WriteLine("Popis knihy: " + k.Popis);
            
            Console.WriteLine("Vyberte možnost co chcete upravit: \n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1"); Console.WriteLine(" - Pro změnu názvu");
            Console.Write("2"); Console.WriteLine(" - Pro změnu ISBN");
            Console.Write("3"); Console.WriteLine(" - Pro změnu ceny");
            Console.Write("4"); Console.WriteLine(" - Pro dopnění zboží");
            Console.Write("5"); Console.WriteLine(" - Pro změnu popisu knihy");
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n0 - Pro opuštění editace");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void deleteSuccess()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPoložka byla úspěšně smazána.");
            Console.ForegroundColor = ConsoleColor.White;
            
            System.Threading.Thread.Sleep(4000);
        }
        
        
        public static void printAllUsers(Uzivatel u)
        {
            UzivatelTableGateway utg = new UzivatelTableGateway();
            List<Uzivatel> k = utg.SelectAll();
            UserDataActions uda = new UserDataActions();
            Uzivatel edit = null;
            RoleGateway role = new RoleGateway();
            
            
            string s = "";
            
            while(true){
                Console.Clear();
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logged as "+u.Jmeno+" "+ u.Prijmeni+"\n");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine("ID\t\t" + "Jméno\t\t" + "Email\t" + "Role\t\t" + "Heslo\t");

                for (int i = 0; i < k.Count; i++)
                {
                    role.Id = k[i].role_id;
                    role.SelectById();
                    if (u.Id == k[i].Id)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(k[i].Id+"\t" + k[i].Jmeno+" " + k[i].Prijmeni + "\t" + k[i].Email +"\t" + role.nazev + "\t" + k[i].Heslo);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine(k[i].Id+"\t" + k[i].Jmeno+" " + k[i].Prijmeni + "\t" + k[i].Email +"\t" + role.nazev + "\t" + k[i].Heslo);
                    }
                }
                
                Console.WriteLine("\nPro detail uživatele napište jeho ID");
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPro ukončení napište quit");
                Console.ForegroundColor = ConsoleColor.White;
                

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
                            edit = k[i];
                            uda.EditUzivatel(ref edit,1);
                        }
                    }
                }
                catch (FormatException)
                {
                    continue;
                }
            }
        }

        public void resetPassword()
        {
            string s = "",jmeno = "",prijmeni ="";
            Uzivatel u = null;
            UzivatelMapper map = null;
            while (true)
            {
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t\tPro ukončení napiše: quit");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nZadejte email vašeho účtu: ");
                Console.ForegroundColor = ConsoleColor.White;
                
                s = Console.ReadLine();
                
                
                if (s == "quit" || jmeno == "quit"|| prijmeni == "quit")
                {
                    return;
                }

                if (UserDataActions.IsValidEmail(s))
                {
                    if (UserDataActions.emailExists(s))
                    {
                        map = new UzivatelMapper();
                        u = map.SelectByEmail(s);
                    }
                    else
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nTento email neexistuje.");
                        Console.ForegroundColor = ConsoleColor.White;
            
                        System.Threading.Thread.Sleep(4000);
                        break;
                    }
                }
                else
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEmail je ve spatnem tvaru...");
                    Console.ForegroundColor = ConsoleColor.White;
            
                    System.Threading.Thread.Sleep(4000);
                    continue;
                }

                if (u != null)
                {
                    Console.WriteLine("Zadejte jméno, které jste zadali při registraci (včetně diakritiky)");
                    jmeno = Console.ReadLine();
                    if (jmeno == u.Jmeno)
                    {
                        
                        Console.WriteLine("Zadejte příjmení, které jste zadali při registraci (včetně diakritiky)");
                        prijmeni = Console.ReadLine();
                        
                        if (prijmeni == u.Prijmeni)
                        {
                            Console.WriteLine("Zadejte nové heslo: ");

                            s = Console.ReadLine();
                            
                            map.Update(u.Id,u.Jmeno,u.Prijmeni,u.Email,u.Adr.Psc,u.Adr.Mesto,u.Adr.Zeme,u.Adr.Ulice,UserDataActions.hash(s),u.Telefon,u.role_id);
                            return;
                        }
                        continue;
                        
                    }

                    continue;
                }
                
            
            }
            
            
        }
        
        
    }
}