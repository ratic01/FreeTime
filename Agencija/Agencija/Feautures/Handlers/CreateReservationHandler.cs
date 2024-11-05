using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;

namespace Agencija.Feautures.Handlers
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, CreateReservationDto>
    {

        private readonly AppDbContext _context;

        public CreateReservationHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CreateReservationDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new InvalidOperationException("Email is required.");
            }
            var user = await _context.Users.FindAsync(request.UserId);
            var tourPackage = await _context.TourPackages.FindAsync(request.TourPackageId);

            if (user == null || tourPackage == null)
            {
                // Rukovanje slučajem gde korisnik ili paket nisu pronađeni
                throw new KeyNotFoundException("Korisnik ili paket nisu pronađeni.");
            }

            if(tourPackage.TotalPackage<request.NumberOfPackages)
            {
                throw new InvalidOperationException("Not enough packages available for reservation.");
            }

            // Smanji broj dostupnih paketa
            tourPackage.TotalPackage -= request.NumberOfPackages;


            // Kreiraj novu rezervaciju
            var reservation = new Reservation
            {
                UserId = request.UserId,
                Email=request.Email,
                TourPackageId = request.TourPackageId,
                ReservationDate = DateTime.Now, // Postavi trenutni datum
                PackageName = tourPackage.PackageName, // Postavi ime paketa iz TourPackage
                Status = Status.Processing, // Početni status (ili neki drugi status)
                TotalReseervation = request.NumberOfPackages // Preuzmi ukupan broj iz TourPackage
            };

            // Dodaj novu rezervaciju u bazu
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);


            // Dodaj događaj u EventStore
            var reservationEvent = new ReservationEvent
            {
                ReservationId = reservation.ReservationId,
                UserId = reservation.UserId.ToString(),
                Email=reservation.Email,
                ActionType = "Created",
                Timestamp = DateTime.Now
            };
            _context.ReservationEvents.Add(reservationEvent);
            await _context.SaveChangesAsync(cancellationToken);

            // Kreiraj DTO za povratak podataka o rezervaciji
            var reservationDto = new CreateReservationDto
            {
                ReservationId = reservation.ReservationId,
                TourPackageId = reservation.TourPackageId,
                UserId = reservation.UserId,
                Email=reservation.Email,
                ReservationDate = reservation.ReservationDate,
                PackageName = reservation.PackageName,
                Status = reservation.Status.Value, // Pretpostavljam da je Status nullable, pa koristi .Value
                NumberOfPackages = request.NumberOfPackages
            };

            return reservationDto;
        }
    }
}
