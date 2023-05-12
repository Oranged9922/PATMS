using Application.Interfaces.Persistance;
using Domain;

namespace Infrastructure.Persistance.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly PatmsDbContext dbContext;

        public UserRepository(PatmsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "user is null");
            }

            dbContext.Add(user);
            dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public User? GetUserById(int userId)
        {
            return dbContext.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
