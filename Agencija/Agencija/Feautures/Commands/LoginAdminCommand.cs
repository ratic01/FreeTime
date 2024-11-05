using MediatR;

namespace Agencija.Feautures.Commands
{
    public class LoginAdminCommand : IRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
