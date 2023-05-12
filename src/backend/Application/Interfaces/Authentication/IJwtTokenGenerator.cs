using Domain;

namespace Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates token for user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateToken(User user);
    }
}
