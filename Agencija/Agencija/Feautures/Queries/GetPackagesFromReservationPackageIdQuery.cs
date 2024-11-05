using Agencija.Dto;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetPackagesFromReservationPackageIdQuery : IRequest<PackageDto>
    {

        public int ReservationId { get; set; }
        public GetPackagesFromReservationPackageIdQuery(int reservationId)
        {
            ReservationId = reservationId;
        }

      
    }
}
