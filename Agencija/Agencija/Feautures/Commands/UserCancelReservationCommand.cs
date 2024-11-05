using MediatR;

namespace Agencija.Feautures.Commands
{
    public class UserCancelReservationCommand : IRequest<bool>
    {
        public int ReservationId { get; set; }
    }
}
