namespace Agencija.Dto
{
    public class ReservationEventDto
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public int ReservationId { get; set; }
        public string ActionType { get; set; } // "Created", "Canceled", itd.
        public DateTime Timestamp { get; set; }
    }
}
