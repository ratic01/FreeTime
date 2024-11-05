using Agencija.Dto;
using Agencija.Models;
using MediatR;

namespace Agencija.Feautures.Queries
{
    public class GetTourPackagebyIdQuery : IRequest<PackageDto>
    {
        public int Id { get; set; }


        public GetTourPackagebyIdQuery(int id)
        {
            Id = id;
        }

    }
}
