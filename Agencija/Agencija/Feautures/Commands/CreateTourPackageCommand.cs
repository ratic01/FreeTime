using MediatR;

namespace Agencija.Feautures.Commands
{
    public class CreateTourPackageCommand : IRequest<string>
    {
        public string PackageName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerPerson { get; set; }
        public string CountryName {  get; set; }      
        public int NumberOfPeople { get; set; }
        public string HotelName { get; set; }

        public int TotalPackage {  get; set; }
    }
}
