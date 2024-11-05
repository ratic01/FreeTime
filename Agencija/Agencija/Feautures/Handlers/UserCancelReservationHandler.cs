using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class UserCancelReservationHandler : IRequestHandler<UserCancelReservationCommand, bool>
    {

        private readonly AppDbContext _context;


        public UserCancelReservationHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UserCancelReservationCommand request, CancellationToken cancellationToken)
        {
            // Pronađi rezervaciju na osnovu ID-ja
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == request.ReservationId, cancellationToken);

            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            // Proveri da li je rezervacija u statusu "Processing"
            if (reservation.Status != Status.Processing)
            {
                return false;
            }

            // Pronađi povezani paket na osnovu ID-ja rezervacije
            var tourPackage = await _context.TourPackages
                .FirstOrDefaultAsync(p => p.TourPackageId == reservation.TourPackageId, cancellationToken);

            if (tourPackage == null)
            {
                throw new Exception("Tour package not found.");
            }

            reservation.Status=Status.Rejected;

            tourPackage.TotalPackage += reservation.TotalReseervation;


            // Sačuvaj promene u bazi podataka
            await _context.SaveChangesAsync(cancellationToken);


            // Dodaj događaj u EventStore
            var reservationEvent = new ReservationEvent
            {
                ReservationId = reservation.ReservationId,                
                UserId = reservation.UserId.ToString(),
                Email=reservation.Email,
                ActionType = "Canceled",
                Timestamp = DateTime.Now
            };
            _context.ReservationEvents.Add(reservationEvent);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
