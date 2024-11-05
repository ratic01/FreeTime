using Agencija.Models;

namespace Agencija.Dto
{
    public class CreateReservationDto
    {
        public int ReservationId { get; set; }
        public int TourPackageId { get; set; }
        public int UserId { get; set; }

      //  public User Email { get; set; }

        public DateTime ReservationDate { get; set; }

        public string PackageName { get; set; }


        public string Email { get; set; }


        public Status Status { get; set; }

        public int NumberOfPackages { get; set; }
    }
}
