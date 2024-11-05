using Agencija.Data;
using Agencija.Dto;
using Agencija.Feautures.Queries;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetTourPackagebyIdHandler : IRequestHandler<GetTourPackagebyIdQuery, PackageDto>
    {

        private readonly AppDbContext _context;

        public GetTourPackagebyIdHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PackageDto> Handle(GetTourPackagebyIdQuery request, CancellationToken cancellationToken)
        {
            var package = await _context.TourPackages.Where(p=>p.TourPackageId==request.Id).FirstOrDefaultAsync();

            if(package == null)
            {
                return null;
            }

            return new PackageDto
            {
                TourPackageId = package.TourPackageId,
                PackageName = package.PackageName,
                StartDate= package.StartDate,
                EndDate = package.EndDate,
                Description= package.Description,               
                HotelName=package.HotelName,
                TotalPackage=package.TotalPackage,
                PricePerPerson=package.PricePerPerson,
                CountryName=package.CountryName,
                NumberOfPeople=package.NumberOfPeople

              

            };
        }
    }
}
