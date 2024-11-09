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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Place = new PlaceRepository(_context);
            News = new NewsRepository(_context);
            Category=new CategoryRepository(_context);
            ApplicationUser=new ApplicationUserRepository(_context);
        }

        public IPlaceRepository Place { private set; get; }
        public INewsRepository News { private set; get; }
        public ICategoryRepository Category { private set; get; }
        public IApplicationUserRepository ApplicationUser { private set; get; } 

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
