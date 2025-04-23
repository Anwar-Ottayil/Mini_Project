using MiniProject.Dto;

namespace MiniProject.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(CategoryViewDto categoryViewDto);
        Task<List<CategoryViewDto>> ViewCategory();
        Task<bool> RemoveCategory(int id);
    }
}
