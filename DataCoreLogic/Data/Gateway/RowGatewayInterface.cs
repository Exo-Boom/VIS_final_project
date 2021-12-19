namespace DataCoreLogic.Data.Gateway
{
    public interface RowGatewayInterface
    {
        int Id{get; set;}
        
        void Insert();
        int Delete();
        void Update();
    }
}