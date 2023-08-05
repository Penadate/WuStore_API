using AutoMapper;
using BLL.Models.Category;
using BLL.Models.Game;
using BLL.Services.Interfaces;
using DAL.Data;
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
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(
            AppDbContext dbContext,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryModel>> GetCategoriesByGameIdAsync(int gameId)
        {
            var categories = await _categoryRepository.GetCategoriesByGameIdAsync(gameId);
            return _mapper.Map<IEnumerable<CategoryModel>>(categories);
        }
    }
}
