using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using vis.Gateway;

namespace vis
{
    public class DataFunctions
    {
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