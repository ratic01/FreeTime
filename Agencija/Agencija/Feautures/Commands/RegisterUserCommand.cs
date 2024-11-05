using MediatR;

namespace Agencija.Feautures.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
