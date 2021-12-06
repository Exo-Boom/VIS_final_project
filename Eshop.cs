using System;
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
                        KnihaTableGateway kg = new KnihaTableGateway();

                        Interface.printKnihy(kg.SelectAll());
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Pokračujte stisknutím ENTER");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
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
                    if (Option == 5)
                    {
                        I.Logout(ref u);
                        break;
                    }
                    if (Option == 6)
                    {
                        
                    }
                    if (Option == 7)
                    {
                        
                        
                    }                    
                    if (Option == 8)
                    {
                        uda.adminKnihy(u);
                    }
                }
            }
            
        }
    }
}