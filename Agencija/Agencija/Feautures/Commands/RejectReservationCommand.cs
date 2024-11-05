using MediatR;

namespace Agencija.Feautures.Commands
{
    public class RejectReservationCommand : IRequest<bool>
    {
        public int ReservationId { get; set; }
    }
}
