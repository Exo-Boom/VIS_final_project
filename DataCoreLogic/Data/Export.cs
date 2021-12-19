using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DataCoreLogic.Data.Logic;


namespace DataCoreLogic.Data
{
    public class Export
    {
        public static bool ExportVyberKnihToCsv(string filename, List<Kniha> seznam)
        {
            try
            {
                using var fileStream = new FileStream(filename, FileMode.Create);
                using var streamWriter = new StreamWriter(fileStream);

                for (int i = 0; i < seznam.Count; i++)
                {
                    streamWriter.WriteLine($"{seznam[i].Id};{seznam[i].Nazev};{seznam[i].Isbn};{seznam[i].Cena};{seznam[i].Pocet};{seznam[i].Popis};");
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }
        
        public static bool ExportVyberKnihToXml(string filename, List<Kniha> seznam)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Kniha>));
                
                using var fileStream = new FileStream(filename, FileMode.Create);

                serializer.Serialize(fileStream, seznam);
                
                fileStream.Flush();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        public static void ExportInvoiceTxt(string f,string invoice)
        {
            
            using StreamWriter file = new(f);
            

            file.Write(invoice);
            
            
        }

    }
    
}