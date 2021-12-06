using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using vis.Gateway;
using vis.Mapper;
using vis.TableGateway;

namespace vis
{
    public class UserDataActions
    {

        public bool vytvorObjednavku(Uzivatel u)
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
                            dirty.Psc = s;
                            break;
                        }
                        case 5:
                        {
                            Console.WriteLine("Zadej nové město: ");
                            s = Console.ReadLine();
                            dirty.Mesto = s;
                            break;
                        }
                        case 6:
                        {
                            Console.WriteLine("Zadej novou země: ");
                            s = Console.ReadLine();
                            dirty.Zeme = s;
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
                            dirty.Ulice = s;
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


                um.Update(dirty.Id,dirty.Jmeno,dirty.Prijmeni,dirty.Email,dirty.Psc,dirty.Mesto,dirty.Zeme,dirty.Ulice,dirty.Heslo,dirty.Telefon,dirty.role_id);
                
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }

            
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
            
            Console.WriteLine("\nPro Pokracovani stisknete klavesu Enter.");
            Console.ReadLine();
            
        }
        
        
    }
    
}