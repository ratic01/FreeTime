using MediatR;

namespace Agencija.Feautures.Commands
{
    public class SendEmailCommand : IRequest<bool>
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
