using Domain;

namespace Application.Interfaces.Persistance
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds new user to the database.
        /// </summary>
        /// <param name="user"></param>
        void Add(User user);

        /// <summary>
        /// Gets user with provided email address from the database.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User? GetUserByEmail(string email);
        User? GetUserById(int userId);
    }
}
