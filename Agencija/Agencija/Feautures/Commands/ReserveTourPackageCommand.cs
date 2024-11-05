using MediatR;

namespace Agencija.Feautures.Commands
{
    public class ReserveTourPackageCommand : IRequest<bool>
    {
        public int TourPackageId { get; set; }
       
    }
}
