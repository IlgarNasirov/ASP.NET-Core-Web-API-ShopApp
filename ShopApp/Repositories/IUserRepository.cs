using Microsoft.AspNetCore.Mvc;
using ShopApp.DTOs;

namespace ShopApp.Repositories
{
    public interface IUserRepository
    {
        public Task<CustomReturnDTO> PostRegister(RegisterUserDTO registerUserDTO);
        public Task<CustomReturnDTO> PostLogin(LoginUserDTO loginUserDTO);
    }
}
