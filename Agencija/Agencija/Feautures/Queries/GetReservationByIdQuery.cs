using Agencija.Dto;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetReservationByIdQuery : IRequest<CreateReservationDto>
    {
         public int ReservationId { get; set; }
        public GetReservationByIdQuery(int reservationId)
        {
            ReservationId = reservationId;
        }

       


        
    }
}
