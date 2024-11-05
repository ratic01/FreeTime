using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {

        private readonly AppDbContext _context;


        public GetUserByIdHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(u => u.UserId == request.Id).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                UserId=user.UserId,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                UserType=user.UserType,
                DateCreated=DateTime.UtcNow          



            };
        }
    }
}
