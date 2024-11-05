using Agencija.Dto;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetReservationsByUserIdQuery : IRequest<List<CreateReservationDto>>
    {
        public int UserId { get; }

        public GetReservationsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
