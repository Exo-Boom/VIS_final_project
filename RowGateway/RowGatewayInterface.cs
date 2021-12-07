namespace vis.Gateway
{
    public interface RowGatewayInterface
    {
        int Id{get; set;}
        
        void Insert();
        void Delete();
        void Update();
    }
}