using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using System.Security.Claims;

namespace Agencija.Feautures.Handlers
{
    public class AcceptReservationCommandHandler : IRequestHandler<AcceptReservationCommand, bool>
    {

        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        


        public AcceptReservationCommandHandler(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
           
        }
        public async Task<bool> Handle(AcceptReservationCommand request, CancellationToken cancellationToken)
        {
            
            var reservation = await _context.Reservations.FindAsync(request.ReservationId);

            if (reservation == null)
            {
                return false;
            }
            else if (reservation.Status == Status.Processing)
            {

                reservation.Status = Status.Accepting; // Change status to Processing

                // Pronađi korisnika za slanje mejla
                var user = await _context.Users.FindAsync(reservation.UserId);
                if (user != null)
                {
                    
                    string subject = "Reservation Accepted";
                    string body = $"Dear {user.FirstName} {user.LastName},<br/>Your reservation has been accepted.";
                    var emailCommand = new SendEmailCommand
                    {
                        ToEmail = "raticmarko2001@gmail.com",
                        Subject = subject,
                        Message = body
                    };

                    // Pošalji mejl
                    await _mediator.Send(emailCommand, cancellationToken);
                }
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                


            }
            return false;

           



        }
    }
}
