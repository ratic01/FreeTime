using Agencija.Dto;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetAllReservationsQuery : IRequest<List<CreateReservationDto>>
    {
    }
}
