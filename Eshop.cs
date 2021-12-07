using System;
using System.Collections.Generic;
using vis.Gateway;
using vis.TableGateway;

namespace vis
{
    public class Eshop
    {
        public static void Run()
        {
            Uzivatel u = new Uzivatel("Anonymous","Anonymous","Anonymous","Anonymous","Anonymous","Anonymous","Anonymous","Anonymous","Anonymous",3);
            Interface I = new Interface();
            UserDataActions uda = new UserDataActions();
            int Option = -1;
            string s;

            
            while (true)
            {
                while (true)
                {
                    I.Welcome();
                    s = Console.ReadLine();
                    try
                    {
                        Option = int.Parse(s);
                    }
                    catch (FormatException)
                    {
                        Console.ReadLine();
                        continue;
                    }


                    if (Option == 0)
                    {
                        if (uda.Login(ref u))
                        {
                            I.LoginSuccess();
                            break;
                        }

                    }

                    if (Option == 1)
                    {
                        if (uda.Register(ref u))
                        {
                            I.RegisterSuccess();
                        }
                    }
                    if (Option == 2)
                    {
                        Console.Clear();
                        return;
                    }
                }

                while (true)
                {
                    I.menu(u);
                    s = Console.ReadLine();
                    try
                    {
                        Option = int.Parse(s);
                    }
                    catch (FormatException)
                    {
                        continue;
                    }

                    if (Option == 1)
                    {
                        while (true){
                            KnihaTableGateway kg = new KnihaTableGateway();
                            List<Kniha> k = kg.SelectAll();
                        
                            Interface.printKnihy(k);

                            Console.WriteLine("\nPro export knih do XML - XML");
                            Console.WriteLine("Pro export knih do CSV - CSV\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nPro ukončení napište quit");
                            Console.ForegroundColor = ConsoleColor.White;
                

                            s = Console.ReadLine();
                            
                            if (s == "XML")
                            {
                                try
                                {
                                    string filename = "ListKnih.xml";
                                Export.ExportVyberKnihToXml(filename,k);
                                Console.ForegroundColor = ConsoleColor.Red;
                                string message = "Váš soubor XML "+filename+ " byl vyexportován.\n";
                                Console.WriteLine(message);
                                Console.ForegroundColor = ConsoleColor.White;
                                System.Threading.Thread.Sleep(3000);
                                break; 
                                }catch (Exception e)
                              {
                                  Console.WriteLine(e);
                                  throw;
                              }
                            }
                            
                            if (s == "CSV")
                            {
                                Export.ExportVyberKnihToCsv("ListKnih.csv",k);
                                break;
                            }
                            if (s == "quit")
                            {
                                break;
                            }

                        }
                    }
                    if (Option == 2)
                    {
                        if (uda.vytvorObjednavku(u))
                        {
                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Objednavka stornovana");
                            Console.ForegroundColor = ConsoleColor.White;
                            System.Threading.Thread.Sleep(3000);
                        }
                        
                    }
                    if (Option == 3)
                    {
                        Interface.printObjednavkyUzivatele(u);
                    }
                    if (Option == 4)
                    {
                        
                        if (uda.EditUzivatel(ref u))
                        {
                            continue;
                        }
                    }
                    if (Option == 0)
                    {
                        I.Logout(ref u);
                        break;
                    }
                    if (Option == 5)
                    {
                        Interface.printAllUsers(u);
                    }
                    if (Option == 6)
                    {
                        Interface.printAllObjednavky(u);
                    }                    
                    if (Option == 7)
                    {
                        uda.adminKnihy(u);
                    }
                }
            }
            
        }
    }
}