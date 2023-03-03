using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Data.Models;
using ShopApp.DTOs;
using ShopApp.Services;

namespace ShopApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopAppDbContext _shopAppDbContext;
        private readonly ITokenService _tokenService;

        public UserRepository(ShopAppDbContext shopAppDbContext, ITokenService tokenService)
        {
            _shopAppDbContext = shopAppDbContext;
            _tokenService = tokenService;
        }
        public async Task<CustomReturnDTO> PostRegister(RegisterUserDTO registerUserDTO)
        {
                var user = await _shopAppDbContext.Users.Where(u => u.Email == registerUserDTO.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                string passwordHash= BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password);
                user = new User
                    {
                        Username = registerUserDTO.Username,
                        PasswordHash = passwordHash,
                        Email = registerUserDTO.Email

                    };
                    await _shopAppDbContext.Users.AddAsync(user);
                    await _shopAppDbContext.SaveChangesAsync();
                    return new CustomReturnDTO { Type = "OK", Message = "User successfully registered!" };
                }
                return new CustomReturnDTO { Type = "BAD", Message = "This email already exists." };
        }
        public async Task<CustomReturnDTO> PostLogin(LoginUserDTO loginUserDTO)
        {
                var user = await _shopAppDbContext.Users.Where(u => u.Email == loginUserDTO.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new CustomReturnDTO { Type = "BAD", Message = "User with this email could not found!" };
                }
                if (!BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, user.PasswordHash))
                {
                    return new CustomReturnDTO { Type = "BAD", Message = "User with this password could not found!" };
                }
                return new CustomReturnDTO { Type = "OK", Data = _tokenService.CreateToken(user) };
        }

    }
}
