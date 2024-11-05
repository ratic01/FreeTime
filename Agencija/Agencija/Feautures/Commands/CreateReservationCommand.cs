using Agencija.Dto;
using MediatR;

namespace Agencija.Feautures.Commands
{
    public class CreateReservationCommand : IRequest<CreateReservationDto>
    {
        public int TourPackageId { get; set; }
        public int UserId { get; set; }

        public string? Email {  get; set; }

        // Novi parametar za broj paketa koje korisnik želi da rezerviše
        public int NumberOfPackages { get; set; }
    }
}
