using System;
using System.Text;

namespace DataCoreLogic.Data.Logic
{
    public class EmailAPIServiceStub
    {
        public static string Tag => "Email";
        public static string ServiceName => "EmailAPI";
        public static string ServiceVersion => "1.1";
        public static string AuthenticationCode => "0000";

        public static string GenerateCode()
        {
            var builder = new StringBuilder();
            
            var _random = new Random();
            
            char offset = 'a';

            const int lettersOffset = 26;

            for (int i = 0; i < 4; i++)
            {
                var p = (char) _random.Next(offset, offset + lettersOffset);
                builder.Append(p);
            }

            return builder.ToString().ToLower();
        }
        
        public static bool Authentication(string userCode,string emailCode)
        {
            return emailCode == userCode;
        }
        
        
        
    }
}