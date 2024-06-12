using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Repositories.Base;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(IDapperContext context) : base(context)
        {
        }
    }
}
