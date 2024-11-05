using Agencija.Feautures.Commands;
using MediatR;

namespace Agencija.Helpers
{
    public class SeedData
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // Poziva MediatR komandu za kreiranje admina
            await mediator.Send(new CreateAdminCommand());
        }
    }
}
