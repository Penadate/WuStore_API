using AutoMapper;
using BLL.Models.Game;
using BLL.Models.User;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Exceptions;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _dbContext;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public GameService(
            AppDbContext dbContext,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameModel>> GetGamesAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GameModel>>(games);
        }
        
        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException($"Invalid id value: {id}");
            }

            var game = await _gameRepository.GetGameByIdAsync(id)
                ?? throw new NotFoundException($"Game with id {id} was not found");

            return _mapper.Map<GameModel>(game);
        }

        public async Task<GameModel> GetGameByNameAsync(string name)
        {
            var game = await _gameRepository.GetGameByNameAsync(name)
                ?? throw new NotFoundException($"Game with Name {name} was not found");

            return _mapper.Map<GameModel>(game);
        }

        public async Task<GameModel> CreateGameAsync(CreateGameModel createGameModel)
        {
            var check = await _gameRepository.GetGameByNameAsync(createGameModel.Name);

            if (check != null)
            {
                throw new NotFoundException($"Game with name \"{check.Name}\" already exists!");
            }

            var game = _mapper.Map<Game>(createGameModel);
            await _gameRepository.CreateAsync(game);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<GameModel>(game);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException($"Invalid id value: {id}");
            }

            var game = await _gameRepository.GetGameByIdAsync(id);

            if (game == null)
            {
                throw new NotFoundException($"Game with id {id} was not found!");
            }

            _gameRepository.Delete(game);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateGameModel updateGameModel)
        {
            if (id <= 0)
            {
                throw new BadRequestException($"Invalid id value: {id}");
            }

            var game = await _gameRepository.GetGameByIdAsync(id);

            if (game == null)
            {
                throw new NotFoundException($"Game with id {id} was not found!");
            }

            if (!string.IsNullOrWhiteSpace(game.Genre))
            {
                game.Genre = game.Genre;
            }
            if (updateGameModel.Rating != null)
            {
                game.Rating = game.Rating;
            }

            _gameRepository.Update(game);
            await _dbContext.SaveChangesAsync();
        }
    }
}
