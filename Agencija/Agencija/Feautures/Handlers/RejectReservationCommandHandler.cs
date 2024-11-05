using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class RejectReservationCommandHandler : IRequestHandler<RejectReservationCommand, bool>
    {

        private readonly AppDbContext _context;
        private readonly IMediator _mediator;


        public RejectReservationCommandHandler(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<bool> Handle(RejectReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(request.ReservationId);

            if (reservation == null)
            {
                return false;
            }
            else if (reservation.Status == Status.Processing || reservation.Status==Status.Accepting)
            {

                // Pronađi povezani paket
                var tourPackage = await _context.TourPackages
                    .FirstOrDefaultAsync(p => p.TourPackageId == reservation.TourPackageId, cancellationToken);

                if (tourPackage == null)
                {
                    throw new Exception("Tour package not found.");
                }

                // Odbij rezervaciju
                reservation.Status = Status.Rejected;
                var user = await _context.Users.FindAsync(reservation.UserId);
                if (user != null)
                {

                    string subject = "Reservation Rejected";
                    string body = $"Dear {user.FirstName} {user.LastName},<br/>Your reservation has been rejected.";
                    var emailCommand = new SendEmailCommand
                    {
                        ToEmail = "raticmarko2001@gmail.com",
                        Subject = subject,
                        Message = body
                    };

                    // Pošalji mejl
                    await _mediator.Send(emailCommand, cancellationToken);
                }             

                // Povećaj broj dostupnih paketa
                tourPackage.TotalPackage += reservation.TotalReseervation;

                // Sačuvaj promene u bazi podataka
                await _context.SaveChangesAsync(cancellationToken);

                return true;


            }
            return false;

        }
    }
}
