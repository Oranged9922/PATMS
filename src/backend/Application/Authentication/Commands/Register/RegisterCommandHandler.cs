using Application.Authentication.Common;
using Application.Interfaces.Authentication;
using Application.Interfaces.Persistance;
using Domain;
using Domain.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler :
         IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterCommandHandler"/> class.
        /// </summary>
        /// <param name="jwtTokenGenerator"></param>
        /// <param name="userRepository"></param>
        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Validate that user does not exist
            if (userRepository.GetUserByEmail(command.Email) is not null)
            {
                return new[] { Errors.User.DuplicateEmail };
            }

            // Create a user (generate unique Id) & Persist to DB
            User user = new()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            userRepository.Add(user);

            // Create JWT token
            string token = jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
