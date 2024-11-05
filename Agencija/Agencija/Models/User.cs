namespace Agencija.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public DateTime DateCreated { get; set; }

        // Relacija: Jedan korisnik može imati više rezervacija (1:N)
        public List<Reservation> Reservations { get; set; }
    }
}
