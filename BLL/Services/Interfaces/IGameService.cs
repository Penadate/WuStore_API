using BLL.Models.Game;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameModel>> GetGamesAsync();
        Task<GameModel> GetGameByIdAsync(int id);
        Task<GameModel> GetGameByNameAsync(string name);
        Task<GameModel> CreateGameAsync(CreateGameModel createGameModel);
        Task UpdateAsync(int id, UpdateGameModel updateGameModel);
        Task DeleteAsync(int id);
    }
}
