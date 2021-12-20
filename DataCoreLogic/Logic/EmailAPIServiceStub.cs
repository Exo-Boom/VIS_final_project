namespace DataCoreLogic.Data.Logic
{
    public class EmailAPIServiceStub
    {
        public static string Tag => "Email";
        public static string ServiceName => "EmailAPI";
        public static string ServiceVersion => "1.1";
        public static string AuthenticationCode => "0000";

        public static bool Authentication(string userCode)
        {
            return AuthenticationCode == userCode;
        }
        
        
        
    }
}