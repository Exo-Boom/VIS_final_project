namespace vis.Gateway
{
    public interface GatewayInterface
    {
        int Id{get; set;}
        
        void Insert();
        void Delete();
        void Update();
    }
}