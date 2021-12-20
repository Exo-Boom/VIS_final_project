using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataCoreLogic.Data.Gateway;
using DataCoreLogic.Data.Logic;
using DataCoreLogic.Data.Mapper;


namespace DataCoreLogic.Data
{
    public class UserDataActions
    {
        public void removeObjednavka(Uzivatel u)
        {
            ObjednavkaMapper objMapper = new ObjednavkaMapper();
            List<Objednavka> k = objMapper.SelectAll();
            

            string s = "";
            while (true)
            {
                Console.Clear();

                Console.WriteLine("ID\t\t" + "Čas\t\t" + "Celková cena");

                for (int i = 0; i < k.Count; i++)
                {
                    Console.WriteLine(k[i].Id+"\t" + k[i].Cas +"\t" + k[i].Celkova_cena+" kč");
                }
                
                Console.WriteLine("\nPro odstranění objednávky napište její ID");
                
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
                            int n = objMapper.Delete(k[i].Id);

                            Interface.deleteSuccess(n);
                            
                            return;
                        }
                    }
                    

                }
                catch (FormatException)
                {
                    
                    continue;
                }
            }
        }
        public void removeKniha()
        {
            KnihaMapper ktg = new KnihaMapper();
            KnihaGateway kniha = new KnihaGateway();
            List<Kniha> k = ktg.SelectAll();
            

            string s = "";
            while (true)
            {
                Interface.printKnihy(k);
                Console.WriteLine("\nPro odstranění knihy napište její ID");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPro opuštění napiš quit");
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
                            
                            kniha.Id = k[i].Id;
                            kniha.Nazev = k[i].Nazev;
                            kniha.ISBN = k[i].Isbn;
                            
                            int n = kniha.Delete();

                            Interface.deleteSuccess(n);
                            
                            return;
                        }
                    }
                    
                }
                catch (FormatException)
                {
                    
                    continue;
                }
            }
        }

        public void removeUser(Uzivatel u)
        {
            UzivatelGateway uzi = new UzivatelGateway();
            UzivatelMapper utg = new UzivatelMapper();
            List<Uzivatel> k = utg.SelectAll();
            string p = "";
            RoleGateway role = new RoleGateway();
            
            
            while (true)
            {          
                Console.Clear();

                Console.WriteLine("ID\t\t" + "Jméno\t\t" + "Email\t" + "Role\t\t" + "Heslo\t");

                for (int i = 0; i < k.Count; i++)
                {
                    role.Id = k[i].role_id;
                    role.SelectById();
                    if (u.Id == k[i].Id)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(k[i].Id + "\t" + k[i].Jmeno + " " + k[i].Prijmeni + "\t" + k[i].Email + "\t" +
                                          role.nazev + "\t" + k[i].Heslo);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine(k[i].Id + "\t" + k[i].Jmeno + " " + k[i].Prijmeni + "\t" + k[i].Email + "\t" +
                                          role.nazev + "\t" + k[i].Heslo);
                    }
                }
                
                Console.WriteLine("\nPro odstranění uživatele napište jeho ID");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPro opuštění napiš quit");
                Console.ForegroundColor = ConsoleColor.White;
                
                string s = "";
                

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
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nOpravdu chcete odstranit uživatele a všechny jeho objednávky? (yes/no)");
                                Console.ForegroundColor = ConsoleColor.White;
                                p = Console.ReadLine();
                                if ("yes" == p)
                                {
                                    break;
                                }

                                if ("no" == p)
                                {
                                    return;
                                }
                                
                            }
                            uzi.Id = k[i].Id;
                            
                            int n = uzi.Delete();

                            Interface.deleteSuccess(n);
                            return;

                        }
                    }
                    

                }
                catch (FormatException)
                {
                    
                    continue;
                }
            }
        }
        
        public void AddKniha()
        {
           string nazev;
           string isbn;
           string Popis;
           string cena;
           string pocet;
           string potvrzeni;
           int pocetInt=0;
           int cenaInt=0;
           
           KnihaGateway uk = new KnihaGateway();
           
           
           bool ok = false;
           
           
           do
           {   Console.Clear();
               Console.WriteLine("Pro ukončení napiše: quit");
               Console.WriteLine("Prosím zadejte nový název: ");
               nazev = Console.ReadLine();
               if (nazev == "quit") return;
               Console.Clear();
               Console.WriteLine("Název: "+ nazev +"\t\t\tPro ukončení napiše: quit"+ "\n");
               Console.WriteLine("Prosím zadejte ISBN knihy: ");
               isbn = Console.ReadLine();
               if (isbn == "quit") return;
               Console.Clear();
               Console.WriteLine("Název: "+nazev+"\t\t\tPro ukončení napiše: quit");
               Console.WriteLine("ISBN: "+isbn);
               Console.WriteLine("Prosím zadejte popis knihy: ");
               Popis = Console.ReadLine();
               if (Popis == "quit") return;
               try
               {
                   while (true)
                   {
                      Console.Clear();
                      Console.WriteLine("Název: "+nazev+"\t\t\tPro ukončení napiše: quit");
                      Console.WriteLine("ISBN: "+isbn);
                      Console.WriteLine("Popis: "+Popis+ "\n");
                      Console.WriteLine("Prosím zadejte cenu nové knihy: ");
                      cena = Console.ReadLine();
                      if (cena == "quit") return;
                      cenaInt = int.Parse(cena);
                      if (cenaInt >= 0)
                      {
                          break;
                      }
                   }
               }
               catch (FormatException)
               {
               }

               try
               {
                   while (true)
                   {
                       Console.Clear();
                       Console.WriteLine("Název: "+nazev+"\t\t\tPro ukončení napiše: quit");
                       Console.WriteLine("ISBN: "+isbn);
                       Console.WriteLine("Popis: "+Popis);
                       Console.WriteLine("Cena: "+cenaInt+ "\n");
                       Console.WriteLine("Prosím zadejte počet kusů na skladě: ");
                       pocet = Console.ReadLine();
                       if (pocet == "quit") return;
                       pocetInt = int.Parse(pocet);
                       if (pocetInt >= 0)
                       {
                           break;
                       }
                   }
               }
               catch (FormatException)
               {
               }

               do
               {                     
                   Console.Clear();
                   Console.WriteLine("Název: "+nazev+"\t\t\tPro ukončení napiše: quit");
                   Console.WriteLine("ISBN: "+isbn);
                   Console.WriteLine("Popis: "+Popis);
                   Console.WriteLine("Cena: "+cenaInt);
                   Console.WriteLine("Kusy na skladě: "+pocetInt+ "\n");
         
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

           uk.cena = cenaInt;
           uk.pocet= pocetInt;
           uk.Nazev= nazev;
           uk.Popis=Popis;
           uk.ISBN=isbn;
           
           uk.Insert();
        }
    

    public bool vytvorObjednavku(ref Uzivatel u)
        {
            ObjednavkaMapper om = new ObjednavkaMapper();
            List<(Kniha,int)> knihy = new List<(Kniha,int)>();
            int idk,pocet;
            string potvrzeni;
            bool Existing=false;
            bool abort = true;

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
                        if (list[i].Id == idk && list[i].Cena != 0)
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
                                        if (pocet < 0)
                                        {
                                            pocet *= -1;
                                        }
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

                                if (list[i].Pocet >= pocet && pocet > 0)
                                {
                                    knihy.Add((list[i], pocet));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\n\n Přidejte do objednávky prosím 1 a více dostupného zboží");
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
                            abort = AnonymTemporaryProfile(ref u);
                            if (abort == false)
                            {
                                return false;
                            }
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
                if (list.Count != 0 && knihy.Count != 0)
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
            KnihaMapper kg = new KnihaMapper();
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
        
        public bool EditUzivatel(ref Uzivatel u,int role = 2)
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
                        Interface.EditUzivatelMenu(dirty,role);
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
                            s = hash(s);
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
                            if (role == 1)
                            {
                                while (true)
                                {
                                    Console.WriteLine("Zadej novou roli: (1 - Admin, 2 - User)");
                                    s = Console.ReadLine();
                                    if (Int32.Parse(s)==1 || Int32.Parse(s) == 2)
                                    {
                                        dirty.role_id = Int32.Parse(s);
                                        break;
                                    }
                                    
                                    
                                }

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
        
        public bool AnonymTemporaryProfile(ref Uzivatel u)
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
                   if (emailExists(email))
                   {
                       Console.ForegroundColor = ConsoleColor.Red;
                       Console.WriteLine("Email: "+email+" Již někdo používá, prosím vyberte jiný, nebo se přihlaste");
                       Console.ForegroundColor = ConsoleColor.White;
                       System.Threading.Thread.Sleep(4500);
                       return false;
                   }
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

           u = new Uzivatel(jmeno,prijmeni,email,psc,mesto,zeme,ulice,hash(""),telefon,3);
           ug.jmeno= jmeno;
           ug.prijmeni= prijmeni;
           ug.email= email;
           ug.Adr.Psc=psc;
           ug.Adr.Mesto=mesto;
           ug.Adr.Zeme=zeme;
           ug.Adr.Ulice=ulice;
           ug.heslo = hash(""); 
           ug.telefon = telefon;
           ug.roleId = 2;
           
           ug.Insert();
           u.Id = ug.Id;
           return true;
        }
        
        
        public static void PrehledObjednavky(List<(Kniha,int)> knihy, int id_obj)
        {
            ObjednavkaMapper o = new ObjednavkaMapper();
            
            Objednavka a = o.SelectById(id_obj);
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Objednavka ID: "+a.Id);
            Console.WriteLine("Celkova cena objednavky: " + a.Celkova_cena + " kč");
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
            KnihaMapper ktg = new KnihaMapper();
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
        
        public static bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

       public bool login(ref Uzivatel a)
       {
           string email, password;

           
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
           
            a = Login(email,password);
            
           if (a != null)
           {
               return true;
           }

           return false;
       }
        
       public bool register(ref Uzivatel a)
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
           DateTime k = new DateTime(2005,2,2,0,0,0);
           string datum;
           int den = 0;
           int rok = 0;
           int mesic = 0;
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
                   if (emailExists(email))
                   {
                       Console.ForegroundColor = ConsoleColor.Red;
                       Console.WriteLine("Email: "+email+" Již někdo používá, prosím vyberte jiný, nebo se přihlaste");
                       Console.ForegroundColor = ConsoleColor.White;
                       System.Threading.Thread.Sleep(4000);
                       
                       return false;
                   }
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

               bool validace = false;

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
                   Console.WriteLine("Prosím zadejte datum narození: ");

                   try
                   {   Console.WriteLine("Prosím zadejte den: ");
                       datum = Console.ReadLine(); 
                       den = int.Parse(datum);
                       if (den < 0)
                       {
                           continue;
                       }
                       Console.WriteLine("Prosím zadejte mesic: ");
                       datum = Console.ReadLine(); 
                       mesic = int.Parse(datum);
                       if (mesic < 0)
                       {
                           continue;
                       }
                       Console.WriteLine("Prosím zadejte rok: ");
                       datum = Console.ReadLine(); 
                       rok = int.Parse(datum);
                       if (rok < 0)
                       {
                           continue;
                       }
                       k = new DateTime(rok, mesic, den, 0, 0, 0);
                       validace = true;
                   }
                   catch (FormatException)
                   {
                        continue;
                   }
                   
               } while (validace != true);
               
               var today = DateTime.Today;
                
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
                   Console.WriteLine("Datum narození: " + k + "(" + (today.Year - k.Year) + "let)");
                   
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
           
           ok = Register(jmeno,prijmeni,email,psc,mesto,zeme,ulice,heslo,telefon);
            
           if (ok == true)
           {
               return true;
           }

           return false;
       }
        
       public Uzivatel Login(string email, string heslo)
       {
           UzivatelMapper um = new UzivatelMapper();
            
           try
           {
               Uzivatel logged = um.SelectByEmail(email);
                
               if (logged.Heslo == hash(heslo))
               {
                   return logged;
               }
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               return null;
           }
           return null;
       }
        
        
       public bool Register(string jmeno, string prijmeni, string email, string psc, string mesto, string zeme, string ulice, string heslo, string telefon, int role = 2)
       {
           UzivatelGateway ug = new UzivatelGateway();

           heslo = hash(heslo);

           try
           {
               ug.jmeno = jmeno;
               ug.prijmeni = prijmeni;
               ug.email = email;
               ug.Adr.Psc = psc;
               ug.Adr.Mesto = mesto;
               ug.Adr.Zeme = zeme;
               ug.Adr.Ulice = ulice;
               ug.heslo = heslo;
               ug.telefon = telefon;
               ug.roleId = role;
               ug.Insert();
           }
           catch (SqlException e)
           {
               Console.WriteLine(e);
               return false;
           }

           return true;
       }
        
       public static String hash(string value)
       {
           StringBuilder Sb = new StringBuilder();

           using (var hash = SHA256.Create())            
           {
               Encoding enc = Encoding.UTF8;
               byte[] result = hash.ComputeHash(enc.GetBytes(value));

               foreach (byte b in result)
                   Sb.Append(b.ToString("x2"));
           }

           return Sb.ToString();
       }
       public static bool emailExists(string value)
       {
           UzivatelMapper u = new UzivatelMapper();

           List<Uzivatel> k = u.SelectAll();

           for (int i = 0; i < k.Count; i++)
           {
               if (k[i].Email == value)
               {
                   return true;
               }
           }

           return false;

       }
       
    }
    
}