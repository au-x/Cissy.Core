namespace Cissy.Swagger
{
    public class BasicAuthScheme : SecurityScheme
    {
        public BasicAuthScheme()
        {
            Type = "basic";
        }
    }
}
