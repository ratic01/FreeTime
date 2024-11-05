using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class CreateTourPackageCommandHandler : IRequestHandler<CreateTourPackageCommand, string>
    {
        private readonly AppDbContext _context;

        public CreateTourPackageCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateTourPackageCommand request, CancellationToken cancellationToken)
        {
           var existingPackage=await _context.TourPackages.FirstOrDefaultAsync(p=>p.PackageName==request.PackageName && p.StartDate == request.StartDate
                                   && p.EndDate == request.EndDate);

            if (existingPackage!=null)
            {
                return "A tour package with the same name and dates already exists.";
            }


            var newpackage = new TourPackage
            {
                PackageName = request.PackageName,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerPerson = request.PricePerPerson,
                CountryName=request.CountryName,
                NumberOfPeople = request.NumberOfPeople,
                HotelName = request.HotelName,
                TotalPackage=request.TotalPackage
               
            };

            _context.TourPackages.Add(newpackage);
            await _context.SaveChangesAsync(cancellationToken);


            return newpackage.PackageName;




        }
        
    }
}
