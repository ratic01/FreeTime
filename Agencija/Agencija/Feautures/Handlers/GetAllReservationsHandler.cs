using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetAllReservationsHandler : IRequestHandler<GetAllReservationsQuery, List<CreateReservationDto>>
    {

        private readonly AppDbContext _context;

        public GetAllReservationsHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CreateReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservations.ToListAsync(cancellationToken);

            var reservationsDtos = reservations.Select(reservation => new CreateReservationDto
            {
                ReservationId = reservation.ReservationId,
                UserId = reservation.UserId,
                TourPackageId = reservation.TourPackageId,
                PackageName=reservation.PackageName,
                NumberOfPackages = reservation.TotalReseervation,
                Status = reservation.Status.Value

            }).ToList();


            return reservationsDtos;


        }
    }
}
