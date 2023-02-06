namespace FoodApi.Configuration
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; }

        public PathToImages PathToImages { get; set; }

        public string AllowedOriginUrl { get; set; }

        public string AllowedHosts { get; set; }

        public string ConnectionString { get; set; }
    }
}
