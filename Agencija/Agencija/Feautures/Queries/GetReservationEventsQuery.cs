using Agencija.Dto;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetReservationEventsQuery : IRequest<List<ReservationEventDto>>
    {
        public int ReservationId { get; set; }
    }
}
