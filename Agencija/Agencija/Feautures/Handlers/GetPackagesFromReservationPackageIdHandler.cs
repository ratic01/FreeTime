using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetPackagesFromReservationPackageIdHandler : IRequestHandler<GetPackagesFromReservationPackageIdQuery, PackageDto>
    {

        private AppDbContext _context;
        private readonly IMediator _mediator;

        public GetPackagesFromReservationPackageIdHandler(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<PackageDto> Handle(GetPackagesFromReservationPackageIdQuery request, CancellationToken cancellationToken)
        {

            var reservation = await _context.Reservations.FindAsync(request.ReservationId);


            var packageId = reservation.TourPackageId; // ID paketa iz rezervacije


            var getPackageQuery = new GetTourPackagebyIdQuery(packageId);
            var packageDto = await _mediator.Send(getPackageQuery, cancellationToken);


            return packageDto;

          

        }
    }
}
