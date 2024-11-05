using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencija.Models
{
    public class TourPackage
    {
        public int TourPackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerPerson { get; set; }
        
        public string CountryName { get; set; }
        public int NumberOfPeople { get; set; }
        public string HotelName { get; set; } 
        
        public int TotalPackage { get; set; }

    



        // Relacija: Jedan turistički paket može imati više rezervacija (1:N)
        public List<Reservation> Reservations { get; set; }

      

    
    }
}
