using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public interface IPasswordGeneratorService
    {
        string GenerateRandomPassword(PasswordOptions opts = null);
        string GenerateRandomCode(PasswordOptions opts = null);
        string GenerateRandomCode1(int string_length);
    }
}
