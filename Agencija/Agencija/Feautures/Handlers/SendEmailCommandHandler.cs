using Agencija.Feautures.Commands;
using MediatR;

namespace Agencija.Feautures.Handlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly EmailService _emailService;


        public SendEmailCommandHandler(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Message);
            return true;
        }
    }
}
