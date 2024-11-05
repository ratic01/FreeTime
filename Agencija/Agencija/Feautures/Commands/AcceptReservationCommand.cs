using MediatR;

namespace Agencija.Feautures.Commands
{
    public class AcceptReservationCommand : IRequest<bool>
    {
        public int ReservationId { get; set; }

    }
}
