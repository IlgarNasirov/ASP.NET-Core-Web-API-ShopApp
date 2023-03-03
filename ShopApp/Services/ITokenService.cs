using ShopApp.Data.Models;

namespace ShopApp.Services
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
