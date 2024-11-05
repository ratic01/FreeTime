using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class RegisterAgentHandler : IRequestHandler<RegisterAgentCommand, string>
    {

        private readonly AppDbContext _context;


        public RegisterAgentHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await _context.Users.AnyAsync(a => a.Email == request.Email);

            if(agent)
            {
                return "Agent with this email already exists.";

            }

            // Heširanje lozinke pre čuvanja u bazi
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newagent = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash= hashedPassword,
                UserType = UserType.Agent, // Postavljanje role za agenta
                DateCreated = DateTime.Now
            };

            _context.Users.Add(newagent);
            await _context.SaveChangesAsync(cancellationToken);

            return "Agent registered successfully.";
        }
    }
}
