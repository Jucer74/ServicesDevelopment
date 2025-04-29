using System;                    
using System.Collections.Generic;  
using System.Linq;                
using System.Threading.Tasks;      
using Pricat.Application.DTOs;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Repositories;

namespace Pricat.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;
            return new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            };
        }
    }
}
