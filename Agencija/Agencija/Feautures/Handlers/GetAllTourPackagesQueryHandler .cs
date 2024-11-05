using Agencija.Data;
using Agencija.Feautures.Queries;
using Agencija.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Feautures.Handlers
{
    public class GetAllTourPackagesQueryHandler : IRequestHandler<GetAllTourPackagesQuery, List<TourPackage>>
    {
        private readonly AppDbContext _context;


        public GetAllTourPackagesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TourPackage>> Handle(GetAllTourPackagesQuery request, CancellationToken cancellationToken)
        {
            return await _context.TourPackages.ToListAsync();
            
        }
    }
}
