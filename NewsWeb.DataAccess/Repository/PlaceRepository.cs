
using NewsWeb.DataAccess.Data;
using NewsWeb.DataAccess.Repository.IRepository;
using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWeb.DataAccess.Repository
{
    public class PlaceRepository:Repository<Place>,IPlaceRepository
    {
        private readonly ApplicationDbContext _context;
        public PlaceRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
