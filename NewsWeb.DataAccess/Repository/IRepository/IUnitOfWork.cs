using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWeb.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPlaceRepository Place { get; }
        INewsRepository News { get; }
        ICategoryRepository Category { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void save();
    }
}
