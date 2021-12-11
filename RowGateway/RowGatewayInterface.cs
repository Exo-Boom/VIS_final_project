namespace vis.Gateway
{
    public interface RowGatewayInterface
    {
        int Id{get; set;}
        
        void Insert();
        int Delete();
        void Update();
    }
}