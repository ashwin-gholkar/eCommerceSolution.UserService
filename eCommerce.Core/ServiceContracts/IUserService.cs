

using eCommerce.Core.DTO;

namespace eCommerce.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<AuthenticationResponse?> Login(LoginRequest login);
        Task<AuthenticationResponse?> Registration(RegisterRequest regReq);
    }
}
