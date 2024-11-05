using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {

        private readonly AppDbContext _context;


        public RegisterUserHandler(AppDbContext context)
        {
               _context = context;
        }
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AnyAsync(u => u.Email == request.Email);

            if(user==true)
            {
                return false;
            }

            // Heširanje lozinke pre čuvanja u bazi
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var newuser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = hashedPassword, 
                UserType = UserType.Customer, // Postavljamo običan tip korisnika
                DateCreated = DateTime.Now
            };

            _context.Users.Add(newuser);
            await _context.SaveChangesAsync(cancellationToken);

            return true;


        }
    }
}
