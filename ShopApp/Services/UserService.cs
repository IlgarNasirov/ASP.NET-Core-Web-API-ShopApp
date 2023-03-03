using System.Security.Claims;

namespace ShopApp.Services
{
    public class UserService : IUserService
    {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UserService(IHttpContextAccessor httpContextAccessor)
            {   
            _httpContextAccessor = httpContextAccessor;
            }

        public int GetUserId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return Convert.ToInt32(result)!;
        }
    }
}
