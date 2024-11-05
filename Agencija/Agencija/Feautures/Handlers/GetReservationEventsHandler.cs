using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetReservationEventsHandler : IRequestHandler<GetReservationEventsQuery, List<ReservationEventDto>>
    {
        private readonly AppDbContext _context;


        public GetReservationEventsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationEventDto>> Handle(GetReservationEventsQuery request, CancellationToken cancellationToken)
        {
            // Pronalazi sve događaje u EventStore koji pripadaju rezervaciji sa zadatim ReservationId
            var events = await _context.ReservationEvents
                .Where(e => e.ReservationId.ToString() == request.ReservationId.ToString())
                .ToListAsync();

            // Mapira događaje na DTO i vraća ih
            return events.Select(e => new ReservationEventDto
            {
                ReservationId = e.ReservationId,
                Email=e.Email,
                UserId = e.UserId,
                ActionType = e.ActionType,
                Timestamp = e.Timestamp
            }).ToList();
        }
    }
}
