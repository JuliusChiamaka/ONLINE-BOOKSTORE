using OnlineBookstore.Application.Interfaces.Repository.Base;
using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
    }
}
