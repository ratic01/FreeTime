namespace Agencija.Models
{
    public class ReservationEvent
    {
        public int ReservationEventId { get; set; }
        public int ReservationId { get; set; }
        public string UserId { get; set; }

        public string Email { get; set; }
        public string ActionType { get; set; } // "Created", "Canceled", itd.
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
