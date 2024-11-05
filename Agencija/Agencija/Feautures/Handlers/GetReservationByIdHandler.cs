using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetReservationByIdHandler : IRequestHandler<GetReservationByIdQuery, CreateReservationDto>
    {

        private readonly AppDbContext _context;

        public GetReservationByIdHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CreateReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == request.ReservationId);

            if (reservation == null)
            {
                return null;
            }

            return new CreateReservationDto
            {
                ReservationId = reservation.ReservationId,
                ReservationDate = reservation.ReservationDate,
                UserId = reservation.UserId,
                TourPackageId = reservation.TourPackageId,
                Status = reservation.Status.Value,
                PackageName=reservation.PackageName,
                NumberOfPackages = reservation.TotalReseervation

            };


        }
    }
}
