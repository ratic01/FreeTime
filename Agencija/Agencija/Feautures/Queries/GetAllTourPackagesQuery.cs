using Agencija.Models;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetAllTourPackagesQuery : IRequest<List<TourPackage>>
    {
    }
}
