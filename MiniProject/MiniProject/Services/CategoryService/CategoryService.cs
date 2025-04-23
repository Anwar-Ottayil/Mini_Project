using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProject.Data;
using MiniProject.Dto;
using MiniProject.Models;

namespace MiniProject.Services.CategoryService
{
    public class CategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> AddCategory(CategoryViewDto categoryViewDto)
        {
            var isExist = await _context.category.AnyAsync(x => x.Name.ToLower() == categoryViewDto.Name.ToLower());
            if (!isExist)
            {
                var d = _mapper.Map<Category>(categoryViewDto);
                await _context.category.AddAsync(d);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<List<CategoryViewDto>> ViewCategory()
        {
            var catagories = await _context.category.ToListAsync();

            if (catagories.Count == 0)
            {
                return new List<CategoryViewDto>();
            }
            return _mapper.Map<List<CategoryViewDto>>(catagories);
        }
        public async Task<bool> RemoveCategory(int id)
        {
            var res = await _context.category.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (res == null)
            {
                return false;
            }
            else
            {
                _context.category.Remove(res);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
