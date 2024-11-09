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
    public class NewsRepository:Repository<News>,INewsRepository
    {
        private readonly ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
