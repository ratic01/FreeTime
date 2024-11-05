using Agencija.Data;
using Agencija.Feautures.Commands;
using Agencija.Models;
using MediatR;

namespace Agencija.Feautures.Handlers
{
    public class UpdateTourPackageCommandHandler : IRequestHandler<UpdateTourPackageCommand, string>
    {

        private readonly AppDbContext _context;


        public UpdateTourPackageCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(UpdateTourPackageCommand request, CancellationToken cancellationToken)
        {
            var oldpackage = await _context.TourPackages.FindAsync(request.TourPackageId);

            if (oldpackage == null)
            {
                return "Tour package not found.";
            }

            oldpackage.PackageName = request.PackageName;
            oldpackage.Description = request.Description;
            oldpackage.StartDate = request.StartDate;
            oldpackage.EndDate = request.EndDate;
            oldpackage.PricePerPerson = request.PricePerPerson;            
            oldpackage.NumberOfPeople = request.NumberOfPeople;
            oldpackage.HotelName = request.HotelName;
            oldpackage.TotalPackage = request.TotalPackage;
            oldpackage.CountryName= request.CountryName;
          


            await _context.SaveChangesAsync();


            return "Tour package updated successfully.";




        }
    }
}
