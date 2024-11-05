using MediatR;

namespace Agencija.Feautures.Commands
{
    public class UpdateTourPackageCommand : IRequest<string>
    {
        public int TourPackageId { get; set; } // Ovaj ID će biti prosleđen u telu zahteva
        public string PackageName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerPerson { get; set; }
        public string CountryName { get; set; }
        public int NumberOfPeople { get; set; }
        public string HotelName { get; set; }

        public int TotalPackage { get; set; }

    }
}
