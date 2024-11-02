
namespace InternetShop.Infrastructure.Services
{
    public class JwtOption
    {   
        public string SecretKey { get; set; } = string.Empty;
        public int ExpritesHours { get; set; }
    }
}
