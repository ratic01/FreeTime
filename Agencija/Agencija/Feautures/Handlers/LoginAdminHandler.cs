using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Interfaces;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class LoginAdminHandler : IRequestHandler<LoginAdminCommand, string>
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public LoginAdminHandler(AppDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<string> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await  _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if(user==null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);


            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid credentials");

            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return token;

        }
    }
}
