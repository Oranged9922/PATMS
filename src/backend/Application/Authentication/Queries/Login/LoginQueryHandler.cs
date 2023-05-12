using Application.Authentication.Common;
using Application.Interfaces.Authentication;
using Application.Interfaces.Persistance;
using Domain;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login
{
    public class LoginQueryHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Make sure user exists
            if (userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return new[] { Domain.Common.Errors.Authentication.InvalidCredentials };
            }

            // Validate the password
            if (user.Password != query.Password)
            {
                return new[] { Domain.Common.Errors.Authentication.InvalidCredentials };
            }

            // Create JWT token
            string token = jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
