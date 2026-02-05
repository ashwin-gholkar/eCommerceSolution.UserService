using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services
{
    internal class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<AuthenticationResponse?> Login(LoginRequest login)
        {
            ApplicationUser? user = await _userRepository.GetUserBtEmailAndPassword(login.Email, login.Password);

            if (user == null)
            {
                return null;
            }

            return new AuthenticationResponse
                    (user.UserId, user.Email, user.PersonName,
                    user.Gender, "token", true);
        }

        public async Task<AuthenticationResponse?> Registration(RegisterRequest regReq)
        {


            ApplicationUser applicationUser = new ApplicationUser()
            {
                PersonName = regReq.PersonName,
                Email = regReq.Email,
                Password = regReq.Password,
                Gender = regReq.Gender.ToString(),

            };

            ApplicationUser? registeredUser = await _userRepository.AddUser(applicationUser);

            if (registeredUser == null)
            {
                return null;
            }
            return new AuthenticationResponse(registeredUser.UserId,
                registeredUser.Email, registeredUser.PersonName
                , registeredUser.Gender, "token", Success: true);

        }
    }
}
