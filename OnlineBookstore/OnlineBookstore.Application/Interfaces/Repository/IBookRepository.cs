using OnlineBookstore.Application.Interfaces.Repository.Base;
using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces.Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> SearchAsync(string title, string author, int? year, string genre);
    }
}
