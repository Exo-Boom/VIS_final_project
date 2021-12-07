using System;
using System.Collections.Generic;
using System.IO;
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

                            Console.WriteLine("\nZadejte nazev souboru i s příponou pro export seznamu");
                            Console.WriteLine("Podporované přípony (.xml,.csv)\n");
                            
                            
                            
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nPro ukončení napište quit");
                            Console.ForegroundColor = ConsoleColor.White;
                            
                            
                            s = Console.ReadLine();
                            
                            string ext = Path.GetExtension(s);
                            
                            if (".xml" == ext)
                            {
                                try
                                {
                                    Export.ExportVyberKnihToXml(s,k);
                                    
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    string message = "Váš soubor XML - "+ s + " byl vyexportován.\n";
                                    Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
                                    Console.WriteLine(message);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    System.Threading.Thread.Sleep(3000);
                                    break; 
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Při exportu došlo k chybě");
                                    System.Threading.Thread.Sleep(3000);
                                }
                            }
                            
                            if (".csv" == ext)
                            {
                                try
                                {
                                    Export.ExportVyberKnihToCsv("ListKnih.csv",k);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    string message = "Váš soubor CSV - "+ s + " byl vyexportován.\n";
                                    Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
                                    Console.WriteLine(message);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    System.Threading.Thread.Sleep(3000);
                                    break; 
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Při exportu došlo k chybě");
                                    System.Threading.Thread.Sleep(3000);
                                }
                                
                                
                                
                                
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