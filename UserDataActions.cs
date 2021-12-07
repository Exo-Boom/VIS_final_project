using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using vis.Gateway;
using vis.Mapper;
using vis.TableGateway;

namespace vis
{
    public class UserDataActions
    {

        public bool vytvorObjednavku(ref Uzivatel u)
        {
            ObjednavkaMapper om = new ObjednavkaMapper();
            List<(Kniha,int)> knihy = new List<(Kniha,int)>();
            int idk,pocet;
            string potvrzeni;
            bool Existing=false;
            

            List<Kniha> list = getKnihy();
            
            
            while(true)
            { 
                bool skip = false;
                Interface.printKnihy(list);
                try
                {
                    Console.WriteLine("Zadej ID knihy kterou chceš přidat do objednávky");
                    idk = int.Parse(Console.ReadLine());

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Id == idk)
                        {
                            Existing = true;
                            break;
                        }
                    }
                    
                    if (Existing == false)
                    {
                        continue;
                    }
                    
                    for (int i = 0; i < knihy.Count; i++)
                    {
                        if (idk == knihy[i].Item1.Id)
                        {
                            while (true){
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Tato kniha se již ve Vaší objednávce nachází!!!");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("0 - Chcete přidat počet kusů?");
                                Console.WriteLine("1 - Chcete odebrat počet kusů?");
                                Console.WriteLine("2 - Chcete odebrat knihu z objednávky?");
                                potvrzeni = Console.ReadLine();
                                
                                if (potvrzeni == "0")
                                {
                                    
                                    Console.WriteLine("\nCelkový dostupný počet knih na skladě: "+ knihy[i].Item1.Pocet);
                                    Console.WriteLine("Vámi objednaný počet v objednávce: "+ knihy[i].Item2);
                                    Console.WriteLine("\nKolik kusů chcete přidat?");
                                    pocet = int.Parse(Console.ReadLine());

                                    if (knihy[i].Item1.Pocet >= (knihy[i].Item2 + pocet))
                                    {
                                        knihy[i] = (knihy[i].Item1,knihy[i].Item2 + pocet);
                                        break;
                                    }
                                    else
                                    { 
                                        Console.WriteLine("\nZadané knihy není na skladě dostatek kusů, prosím zkuste to po doplnění zboží.");
                                        System.Threading.Thread.Sleep(3000);
                                        continue;
                                    }
                                    
                                }
                                if (potvrzeni == "1")
                                {
                                    
                                    Console.WriteLine("\nCelkový dostupný počet knih na skladě: "+ knihy[i].Item1.Pocet);
                                    Console.WriteLine("Vámi objednaný počet v objednávce: "+ knihy[i].Item2);
                                    Console.WriteLine("\nKolik kusů chcete odebrat?");
                                    pocet = int.Parse(Console.ReadLine());

                                    if (0 >= (knihy[i].Item2 - pocet))
                                    {
                                        knihy.RemoveAt(i);

                                        break;
                                    }
                                    else
                                    {
                                        knihy[i] = (knihy[i].Item1,knihy[i].Item2 - pocet);

                                        break;
                                    }
                                    
                                }

                                if (potvrzeni == "2")
                                {
                                    knihy.RemoveAt(i);
                                    break;
                                }
                            }
                            pocet = 0;
                            potvrzeni = "";

                            skip = true;
                            break;
                        }
                    }

                    
                    if (skip == false)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Id == idk)
                            {
                                Console.WriteLine("Zadej pocet objednavanych knih: ");
                                pocet = int.Parse(Console.ReadLine());

                                if (list[i].Pocet >= pocet)
                                {
                                    knihy.Add((list[i], pocet));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nZadané knihy není na skladě dostatek kusů, prosím zkuste to po doplnění zboží.");
                                    continue;
                                }

                            }
                        }

                    }
                }
                catch (FormatException)
                {
                    continue;
                }
                while (true){
                    Console.WriteLine("\n1 - Pokud chcete pridat dalsi knihu");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n2 - Pokud chcete potvrdit objednavku");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n3 - Pokud si chcete prohlédnout stav objednávky");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n0 - Pokud chcete stornovat objednavku\n");
                    Console.ForegroundColor = ConsoleColor.White;
                
                
                
                    potvrzeni = Console.ReadLine();

                    if (potvrzeni == "1")
                    {
                        break;
                    }
                    
                    if (potvrzeni == "2")
                    {
                        if (u.role_id == 3)
                        {   
                            AnonymTemporaryProfile(ref u);
                        }
                        break;
                    }
                    if (potvrzeni == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("ID\tKniha\tISBN\t\tCena\t\tObjednaný Počet");
                        for (int i = 0; i < knihy.Count; i++)
                        {
                            Console.WriteLine(knihy[i].Item1.Id+"\t"+knihy[i].Item1.Nazev+"\t"+knihy[i].Item1.Isbn+"\t"+knihy[i].Item1.Cena+"\t\t"+knihy[i].Item2);
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nPro pokračováni stiskněte ENTER");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
                        continue;

                    }
                    if (potvrzeni == "0")
                    {
                        return false;
                    }
                    
                }
                if (potvrzeni == "2")
                {
                    break;
                }
            }

            int status = -1;


            try
            {
                if (list.Any())
                {
                    status = om.Insert(DateTime.Now, u.Id, knihy);
                }
                else
                { 
                    return false;
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine("Došlo k chybě: \n"+ e);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pokračujte stisknutím ENTER");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
                return false;
            }

            if (status == -1)
            {
                return false;
            }
            
            PrehledObjednavky(knihy,status);
            
            return true;
        }
        
        public List<Kniha> getKnihy()
        {
            KnihaTableGateway kg = new KnihaTableGateway();
            List<Kniha> k = new List<Kniha>();
            try
            {
                k = kg.SelectAll();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return null;
            }

            return k;
        }
        
        public bool EditUzivatel(ref Uzivatel u)
        {
            
            Uzivatel dirty;
            string s = "";
            int i=0;
            bool Break = false;
            
            UzivatelMapper um = new UzivatelMapper();
            try
            {
                dirty = um.SelectById(u.Id);
                while(true){
                    if (Break)
                    {
                        break;
                    }

                    try
                    {
                        Interface.EditUzivatelMenu(dirty);
                        i = int.Parse(Console.ReadLine());
                        
                    switch (i)
                    {
                        case 0:
                        {
                            Break = true;
                            continue;
                        }
                        case 1:
                        {
                            Console.WriteLine("Zadej nové jméno: ");
                            s = Console.ReadLine();
                            dirty.Jmeno = s;
                            break;
                        }
                            
                        case 2:
                        {   
                            Console.WriteLine("Zadej nové příjmení: ");
                            s = Console.ReadLine();
                            dirty.Prijmeni = s;
                            break;
                        }
                        case 3:
                        {
                            Console.WriteLine("Zadej nový email: ");
                            s = Console.ReadLine();
                            dirty.Email = s;
                            break;
                        }
                        case 4:
                        {
                            Console.WriteLine("Zadej nové PSC: ");
                            s = Console.ReadLine();
                            dirty.Adr.Psc = s;
                            break;
                        }
                        case 5:
                        {
                            Console.WriteLine("Zadej nové město: ");
                            s = Console.ReadLine();
                            dirty.Adr.Mesto = s;
                            break;
                        }
                        case 6:
                        {
                            Console.WriteLine("Zadej novou země: ");
                            s = Console.ReadLine();
                            dirty.Adr.Zeme = s;
                            break;
                        }
                        case 7:
                        {
                            Console.WriteLine("Zadej nové heslo: ");
                            s = Console.ReadLine();
                            s = UserActions.hash(s);
                            dirty.Heslo = s; 
                            break;
                        }
                        case 8:
                        {
                            Console.WriteLine("Zadej nový telefon: ");
                            s = Console.ReadLine();
                            dirty.Telefon = s;
                            break;
                        }
                        case 9:
                        {
                            Console.WriteLine("Zadej novou ulici: ");
                            s = Console.ReadLine();
                            dirty.Adr.Ulice = s;
                            break;
                            
                        }
                        case 10:
                        {
                            if (dirty.role_id == 0)
                            {
                                Console.WriteLine("Zadej novou roli: ");
                                s = Console.ReadLine();
                                dirty.role_id = Int32.Parse(s);
                            }

                            break;
                        }
                    }
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }


                um.Update(dirty.Id,dirty.Jmeno,dirty.Prijmeni,dirty.Email,dirty.Adr.Psc,dirty.Adr.Mesto,dirty.Adr.Zeme,dirty.Adr.Ulice,dirty.Heslo,dirty.Telefon,dirty.role_id);
                
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }

            
        }
        
        public void AnonymTemporaryProfile(ref Uzivatel u)
        {
            string jmeno;
           string prijmeni;
           string email;
           string psc;
           string mesto;
           string zeme;
           string ulice;
           string telefon;
           string potvrzeni; 
           UzivatelGateway ug = new UzivatelGateway();
           
           UserActions ua = new UserActions();
           bool ok = false;
           
           
           do
           {   Console.Clear();
               Console.WriteLine("Pro ukončení napiše: quit");
               Console.WriteLine("Prosím zadejte jméno: ");
               jmeno = Console.ReadLine();
               if (jmeno == "quit") return;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno +"\t\t\tPro ukončení napiše: quit"+ "\n");
               Console.WriteLine("Prosím zadejte Prijmeni: ");
               prijmeni = Console.ReadLine();
               if (prijmeni == "quit") return;
               do
               {
                   Console.Clear();
                   Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
                   Console.WriteLine("Prijmeni: "+prijmeni+ "\n");
                   Console.WriteLine("Prosím zadejte email: ");
                   email = Console.ReadLine();
                       
               } while (IsValidEmail(email) != true);
               
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email+ "\n");
               Console.WriteLine("Prosím zadejte PSČ: ");
               psc = Console.ReadLine();
               if (psc == "quit") return;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc+ "\n");
               Console.WriteLine("Prosím zadejte město: ");
               mesto = Console.ReadLine();
               if (mesto == "quit") return;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc);
               Console.WriteLine("Město: "+mesto+ "\n");
               Console.WriteLine("Prosím zadejte zemi: ");
               zeme = Console.ReadLine();
               if (zeme == "quit") return;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc);
               Console.WriteLine("Město: "+mesto);
               Console.WriteLine("Země: "+zeme+ "\n");
               Console.WriteLine("Prosím zadejte ulici: ");
               ulice = Console.ReadLine();
               if (ulice == "quit") return;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc);
               Console.WriteLine("Město: "+mesto);
               Console.WriteLine("Země: "+zeme);
               Console.WriteLine("Ulice: "+ulice+ "\n");
               Console.WriteLine("Prosím zadejte telefon: ");
               telefon = Console.ReadLine();
               if (telefon == "quit") return;
               do
               {                     
                   Console.Clear();
                   Console.WriteLine("Jmeno: " + jmeno);
                   Console.WriteLine("Prijmeni: " + prijmeni);
                   Console.WriteLine("Email: " + email);
                   Console.WriteLine("PSČ: " + psc);
                   Console.WriteLine("Město: " + mesto);
                   Console.WriteLine("Země: " + zeme);
                   Console.WriteLine("Ulice: " + ulice);
                   Console.WriteLine("Telefon: " + telefon + "\n");
         
                   Console.WriteLine("Jsou všechny údaje správně? y/n" );
                   potvrzeni = Console.ReadLine();

                   if (potvrzeni == "y")
                   {
                       ok = true;
                       break;
                   }

                   if (potvrzeni == "n")
                   {
                       break;
                   }
               } while (true);

           } while (ok != true);
           
           u.Jmeno = ug.jmeno= jmeno;
           u.Prijmeni = ug.prijmeni= prijmeni;
           u.Email = ug.email= email;
           u.Adr.Psc = ug.Adr.Psc=psc;
           u.Adr.Mesto = ug.Adr.Mesto=mesto;
           u.Adr.Zeme = ug.Adr.Zeme=zeme;
           u.Adr.Ulice = ug.Adr.Ulice=ulice;
           u.Heslo = ug.heslo = UserActions.hash(""); 
           u.Telefon = ug.telefon = telefon;
            
           ug.Insert();
        }
        
        
        public static void PrehledObjednavky(List<(Kniha,int)> knihy, int id_obj)
        {
            ObjednavkaMapper o = new ObjednavkaMapper();
            
            Objednavka a = o.SelectById(id_obj);
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Objednavka ID: "+a.Id);
            Console.WriteLine("Celkova cena objednavky: " + a.Celkova_cena);
            Console.WriteLine("Objednavka vytvořena : "+a.Cas + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("Seznam položek: ");
            Console.WriteLine("Nazev knihy"+"\t"+"Cena"+"\t"+"Zakoupeny pocet");
            for (int i = 0; i < knihy.Count; i++)
            {
                Console.WriteLine(knihy[i].Item1.Nazev +"\t\t"+knihy[i].Item1.Cena+"\t"+ knihy[i].Item2);
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Pokračujte stisknutím ENTER");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
            
        }
        
        public bool EditKniha(Kniha u)
        {
            
            Kniha dirty;
            string s = "";
            int i=0;
            bool Break = false;
            KnihaGateway kg = new KnihaGateway();
            KnihaMapper km = new KnihaMapper();
            try
            {
                dirty = km.SelectById(u.Id);
                while(true){
                    if (Break)
                    {
                        break;
                    }

                    try
                    {
                        Interface.MenuEditaceKniha(ref dirty);
                        i = int.Parse(Console.ReadLine());
                        
                    switch (i)
                    {
                        case 0:
                        {
                            Break = true;
                            continue;
                        }
                        case 1:
                        {
                            Console.WriteLine("Zadej nový název: ");
                            s = Console.ReadLine();
                            dirty.Nazev = s;
                            break;
                        }
                            
                        case 2:
                        {   
                            Console.WriteLine("Zadej nové ISBN: ");
                            s = Console.ReadLine();
                            dirty.Isbn = s;
                            break;
                        }
                        case 3:
                        {
                            Console.WriteLine("Zadej novou cenu: ");
                            dirty.Cena = int.Parse(Console.ReadLine());
                            break;
                        }
                        case 4:
                        {
                            Console.WriteLine("Zadej nový počet: ");
                            dirty.Pocet = int.Parse(Console.ReadLine());
                            break;
                        }
                        case 5:
                        {
                            Console.WriteLine("Zadej nový popis: ");
                            s = Console.ReadLine();
                            dirty.Popis = s;
                            break;
                        }
                    }
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }

                kg.Nazev = dirty.Nazev;
                kg.cena = dirty.Cena;
                kg.Popis = dirty.Popis;
                kg.pocet = dirty.Pocet;
                kg.Id = dirty.Id;
                kg.ISBN = dirty.Isbn;
                
                kg.Update();
                
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }

            
        }
        
        
        public void adminKnihy(Uzivatel u)
        {
            KnihaTableGateway ktg = new KnihaTableGateway();
            List<Kniha> k = ktg.SelectAll();
            string s = "";
            
            while (true)
            {
                Interface.printKnihy(k);
                Console.WriteLine("\nNapište ID knihy, kterou chcete upravit");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNebo napište quit pro odchod z výběru");
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
                            EditKniha(k[i]);
                        }
                    }
                    

                }
                catch (FormatException)
                {
                    
                    continue;
                }
                
                
            }
            
        }
        
        public bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

       public bool Login(ref Uzivatel a)
       {
           string email, password;

           UserActions ua = new UserActions();
           do
           {
               Console.Clear();
               Console.WriteLine("Prosím zadejte email: ");
               email = Console.ReadLine();
               
           } while (IsValidEmail(email) != true);
            
           Console.Clear();
           Console.WriteLine("Email: "+ email);
           Console.WriteLine("Prosím zadejte heslo: ");
           password = Console.ReadLine();
           
            a = ua.Login(email,password);
            
           if (a != null)
           {
               return true;
           }

           return false;
       }
        
       public bool Register(ref Uzivatel a)
       {
           string jmeno;
           string prijmeni;
           string email;
           string psc;
           string mesto;
           string zeme;
           string ulice;
           string heslo;
           string telefon;
           string potvrzeni; 
           
           UserActions ua = new UserActions();
           bool ok = false;
           
           
           do
           {   Console.Clear();
               Console.WriteLine("Pro ukončení napiše: quit");
               Console.WriteLine("Prosím zadejte jméno: ");
               jmeno = Console.ReadLine();
               if (jmeno == "quit") return false;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno +"\t\t\tPro ukončení napiše: quit"+ "\n");
               Console.WriteLine("Prosím zadejte Prijmeni: ");
               prijmeni = Console.ReadLine();
               if (prijmeni == "quit") return false;
               do
               {
                   Console.Clear();
                   Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
                   Console.WriteLine("Prijmeni: "+prijmeni+ "\n");
                   Console.WriteLine("Prosím zadejte email: ");
                   email = Console.ReadLine();
                       
               } while (IsValidEmail(email) != true);
               
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email+ "\n");
               Console.WriteLine("Prosím zadejte PSČ: ");
               psc = Console.ReadLine();
               if (psc == "quit") return false;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc+ "\n");
               Console.WriteLine("Prosím zadejte město: ");
               mesto = Console.ReadLine();
               if (mesto == "quit") return false;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc);
               Console.WriteLine("Město: "+mesto+ "\n");
               Console.WriteLine("Prosím zadejte zemi: ");
               zeme = Console.ReadLine();
               if (zeme == "quit") return false;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc);
               Console.WriteLine("Město: "+mesto);
               Console.WriteLine("Země: "+zeme+ "\n");
               Console.WriteLine("Prosím zadejte ulici: ");
               ulice = Console.ReadLine();
               if (ulice == "quit") return false;
               Console.Clear();
               Console.WriteLine("Jmeno: "+jmeno+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("Prijmeni: "+prijmeni);
               Console.WriteLine("Email: "+email);
               Console.WriteLine("PSČ: "+psc);
               Console.WriteLine("Město: "+mesto);
               Console.WriteLine("Země: "+zeme);
               Console.WriteLine("Ulice: "+ulice+ "\n");
               Console.WriteLine("Prosím zadejte telefon: ");
               telefon = Console.ReadLine();
               if (telefon == "quit") return false;
               do
               {
                   Console.Clear();
                   Console.WriteLine("Jmeno: " + jmeno);
                   Console.WriteLine("Prijmeni: " + prijmeni);
                   Console.WriteLine("Email: " + email);
                   Console.WriteLine("PSČ: " + psc);
                   Console.WriteLine("Město: " + mesto);
                   Console.WriteLine("Země: " + zeme);
                   Console.WriteLine("Ulice: " + ulice);
                   Console.WriteLine("Telefon: " + telefon + "\n");
                   Console.WriteLine("Prosím zadejte heslo: ");
                   heslo = Console.ReadLine();
                   Console.WriteLine("Prosím zadejte heslo znovu pro potvrzení: ");
                   potvrzeni = Console.ReadLine();
                   if (heslo != potvrzeni)
                   {
                       Console.WriteLine("Hesla se neshodují. Zkuste to znovu...");
                   }
                   System.Threading.Thread.Sleep(3000);
               } while (heslo != potvrzeni);
               do
               {                     
                   Console.Clear();
                   Console.WriteLine("Jmeno: " + jmeno);
                   Console.WriteLine("Prijmeni: " + prijmeni);
                   Console.WriteLine("Email: " + email);
                   Console.WriteLine("PSČ: " + psc);
                   Console.WriteLine("Město: " + mesto);
                   Console.WriteLine("Země: " + zeme);
                   Console.WriteLine("Ulice: " + ulice);
                   Console.WriteLine("Telefon: " + telefon + "\n");
         
                   Console.WriteLine("Jsou všechny údaje správně? y/n" );
                   potvrzeni = Console.ReadLine();

                   if (potvrzeni == "y")
                   {
                       ok = true;
                       break;
                   }

                   if (potvrzeni == "n")
                   {
                       break;
                   }
               } while (true);

           } while (ok != true);
           
           ok = ua.Register(jmeno,prijmeni,email,psc,mesto,zeme,ulice,heslo,telefon);
            
           if (ok == true)
           {
               return true;
           }

           return false;
       }
        
    }
    
}