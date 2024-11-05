using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetReservationsByUserIdHandler : IRequestHandler<GetReservationsByUserIdQuery, List<CreateReservationDto>>
    {
        private readonly AppDbContext _context;

        public GetReservationsByUserIdHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CreateReservationDto>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            // Dobij sve rezervacije za korisnika sa UserId
            var reservations = await _context.Reservations
                .Where(r => r.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            // Konvertuj rezultate u DTO
            var reservationDtos = reservations.Select(reservation => new CreateReservationDto
            {
                ReservationId = reservation.ReservationId,
                ReservationDate = reservation.ReservationDate,
                UserId = reservation.UserId,
                TourPackageId = reservation.TourPackageId,
                Status = reservation.Status.Value,
                PackageName = reservation.PackageName,
                NumberOfPackages = reservation.TotalReseervation
            }).ToList();

            return reservationDtos; // Vraća listu rezervacija
        }
    }
    
}
