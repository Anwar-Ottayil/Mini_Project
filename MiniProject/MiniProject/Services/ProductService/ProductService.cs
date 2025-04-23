using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProject.Data;
using MiniProject.Dto;
using MiniProject.Models;
using MiniProject.Services.CloudinaryService;

namespace MiniProject.Services.ProductService
{
    public class ProductService: IProductService
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public ProductService(AppDbContext context, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _Context = context;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<List<ProductViewDto>> GetAllProducts()
        {
            try
            {
                var products = await _Context.product.Include(x => x.category).ToListAsync();
                if (products.Count > 0)
                {
                    var productall = products.Select(x => new ProductViewDto
                    {
                        Title = x.Title,
                        Description = x.Description,
                        Price = x.Price,
                        stock = x.stock,
                        Image = x.Image
                    }).ToList();
                    return productall;
                }
                return new List<ProductViewDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProductViewDto> GetProductsById(int id)
        {
            try
            {
                var products = await _Context.product.FirstOrDefaultAsync(x => x.ProductId == id);
                if (products == null)
                {
                    return null;
                }
                return new ProductViewDto()
                {
                    Title = products.Title,
                    Description = products.Description,
                    Price = products.Price,
                    stock = products.stock,
                    Image = products.Image
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<ProductViewDto>> GetProductsByCategory(string categoryname)
        {
            var products = await _Context.product.Include(x => x.category)
                .Where(x => x.category.Name == categoryname)
                .Select(x => new ProductViewDto
                {
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    stock = x.stock,
                    Image = x.Image

                }).ToListAsync();
            if (!products.Any())
            {
                return new List<ProductViewDto>();
            }
            return products;
        }
        public async Task<bool> AddProduct(AddProductDto addProduct, IFormFile image)
        {
            try
            {

                if (addProduct == null)
                {
                    return false;
                }
                var category = await _Context.category.FirstOrDefaultAsync(x => x.CategoryId == addProduct.CategoryId);
                if (category == null)
                {
                    throw new Exception("there is no  category in this id");
                }
                if (image == null)
                {
                    throw new InvalidOperationException("Image is Not Uploaded");
                }

                string imageUrl = await _cloudinaryService.UploadImage(image);
                var product = _mapper.Map<Product>(addProduct);
                product.Image = imageUrl;
                await _Context.product.AddAsync(product);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _Context.product.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product == null)
            {
                return false;
            }
            _Context.product.Remove(product);
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditProduct(int id, AddProductDto editproduct, IFormFile image)
        {
            var exproduct = await _Context.product.FirstOrDefaultAsync(x => x.ProductId == id);
            var catexist = await _Context.category.FirstOrDefaultAsync(x => x.CategoryId == editproduct.CategoryId);
            if (catexist == null)
            {
                throw new Exception("There is no category in this id");
            }
            if (exproduct == null)
            {
                return false;
            }
            try
            {
                exproduct.Title = editproduct.Title;
                exproduct.Description = editproduct.Description;
                exproduct.Price = editproduct.Price;
                exproduct.stock = editproduct.stock;
                exproduct.CategoryId = editproduct.CategoryId;
                if (image != null && image.Length > 0)
                {
                    string imageUrl = await _cloudinaryService.UploadImage(image);
                    exproduct.Image = imageUrl;
                }
                _Context.product.Update(exproduct);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public async Task<List<ProductViewDto>> SearchProduct(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return new List<ProductViewDto>();
            }
            var products = await _Context.product.Include(x => x.category)
                .Where(p => p.Title.ToLower().Contains(search.ToLower()))
                .ToListAsync();
            return products.Select(s => new ProductViewDto
            {
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                Image = s.Image,
            }).ToList();
        }
        public async Task<List<ProductViewDto>> PaginatedProduct(int pagenumber, int pagesize)
        {
            try
            {
                var products = await _Context.product.Include(x => x.category).Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
                return products.Select(p => new ProductViewDto
                {
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
