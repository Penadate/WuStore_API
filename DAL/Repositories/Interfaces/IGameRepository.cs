using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task<Game?> GetGameByNameAsync(string name);
    }
}
