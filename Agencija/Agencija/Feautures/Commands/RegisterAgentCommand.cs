using MediatR;

namespace Agencija.Feautures.Commands
{
    public class RegisterAgentCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}

