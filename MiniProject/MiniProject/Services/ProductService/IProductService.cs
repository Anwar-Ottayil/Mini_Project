using MiniProject.Dto;

namespace MiniProject.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ProductViewDto>> GetAllProducts();
        Task<ProductViewDto> GetProductsById(int id);
        Task<List<ProductViewDto>> GetProductsByCategory(string category);
        Task<bool> AddProduct(AddProductDto addProductDto, IFormFile image);
        Task<bool> DeleteProduct(int id);
        Task<bool> EditProduct(int id, AddProductDto editproduct, IFormFile image);
        Task<List<ProductViewDto>> SearchProduct(string search);
        Task<List<ProductViewDto>> PaginatedProduct(int pagenumber, int pagesize);
    }
}
