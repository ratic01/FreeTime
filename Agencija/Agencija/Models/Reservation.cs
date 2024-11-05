using System.ComponentModel.DataAnnotations.Schema;

namespace Agencija.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        

        // Relacija: Jedna rezervacija je povezana sa jednim korisnikom (N:1)
        public int UserId { get; set; }
        public User User { get; set; }

        // Relacija: Jedna rezervacija je povezana sa jednim paketom (N:1)
        public int TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }

        public string? Email { get; set; }


        public string PackageName { get; set; }


        [Column(TypeName = "nvarchar(20)")]
        public Status? Status { get; set; }

        public int TotalReseervation { get; set; }
    }
}
