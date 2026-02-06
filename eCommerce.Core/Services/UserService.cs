using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services
{
    internal class UserService(IUserRepository userRepository,IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AuthenticationResponse?> Login(LoginRequest login)
        {
            ApplicationUser? user = await _userRepository.GetUserBtEmailAndPassword(login.Email, login.Password);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<AuthenticationResponse>(user) with 
                        { Success =true,Token = "token"};
        }

        public async Task<AuthenticationResponse?> Registration(RegisterRequest regReq)
        {


            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(regReq);

            ApplicationUser? registeredUser = await _userRepository.AddUser(applicationUser);

            if (registeredUser == null)
            {
                return null;
            }
            return _mapper.Map<AuthenticationResponse>(registeredUser) with
            { Success = true, Token = "token" };

        }
    }
}
