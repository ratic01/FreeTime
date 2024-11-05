using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, int>
    {
        private readonly AppDbContext _context;


        public CreateAdminCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var adminExist =await  _context.Users.AnyAsync(u => u.UserType == UserType.Administrator);

            if(!adminExist)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("123");
                var admin = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = hashedPassword,
                    UserType= UserType.Administrator,
                    DateCreated= DateTime.UtcNow
                };
                _context.Users.Add(admin);
                await _context.SaveChangesAsync(cancellationToken);

                return admin.UserId;
            }

            return 0;

           
        }
    }
}
