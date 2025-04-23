using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProject.Data;
using MiniProject.Dto;
using MiniProject.Models;

namespace MiniProject.Services.CartService
{
    public class CartService:ICartService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CartService> _logger;
        private readonly IMapper _mapper;
        public CartService(AppDbContext context, ILogger<CartService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResponses<CartItems>> AddToCart(int productId, int userid)
        {
            try
            {
                Console.WriteLine(userid);
                var isuser = await _context.users.Include(ci => ci.Cart).ThenInclude(x => x.cartitems).FirstOrDefaultAsync(x => x.Id == userid);
                if (isuser == null)
                {
                    return new ApiResponses<CartItems>(404, "User not found");
                }
                var isproduct = await _context.product.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (isproduct == null)
                {
                    return new ApiResponses<CartItems>(404, "Product not found");
                }
                if (isuser.Cart == null)
                {
                    isuser.Cart = new Cart()
                    {
                        UserId = userid,
                        cartitems = new List<CartItems>()
                    };
                    await _context.carts.AddAsync(isuser.Cart);
                    await _context.SaveChangesAsync();
                }
                if (isproduct?.stock <= 0)
                {
                    return new ApiResponses<CartItems>(404, "Out of stock");
                }
                var check = isuser.Cart?.cartitems?.FirstOrDefault(p => p.ProductId == productId);
                if (check != null)
                {
                    check.Quantity++;
                    await _context.SaveChangesAsync();

                }
                else
                {

                    var item = new CartItems
                    {
                        CartId = isuser.Cart.Id,
                        ProductId = productId,
                        Quantity = 1
                    };
                    isuser?.Cart?.cartitems?.Add(item);
                }
                await _context.SaveChangesAsync();
                return new ApiResponses<CartItems>(200, "Successfully added to cart");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding to cart: " + ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.LogError("Inner exception: " + ex.InnerException.Message);
                }
                return new ApiResponses<CartItems>(500, "Internal server error", null, ex.Message);
            }
        }
        public async Task<List<CartViewDto>> GetCart(int userid)
        {
            if (userid == 0)
            {
                throw new Exception("Userid is null");
            }
            var cart = await _context.carts.Include(c => c.cartitems).ThenInclude(p => p.Product).FirstOrDefaultAsync(x => x.UserId == userid);
            if (cart != null)
            {
                var cartitem = cart.cartitems.Select(x => new CartViewDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Title,
                    Price = x.Product.Price,
                    Quantity = x.Quantity,
                    TotalAmount = x.Quantity * x.Product.Price,
                    Image = x.Product.Image

                }).ToList();
                return cartitem;
            }
            return new List<CartViewDto>();
        }
        public async Task<bool> RemoveFromCart(int userId, int productId)
        {
            try
            {
                var user = await _context.users.Include(c => c.Cart)
                                    .ThenInclude(ci => ci.cartitems)
                                    .ThenInclude(p => p.Product)
                                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User is not found");
                }

                var deleteItem = user?.Cart?.cartitems?.FirstOrDefault(p => p.ProductId == productId);
                if (deleteItem == null)
                {
                    return false;
                }

                user?.Cart?.cartitems?.Remove(deleteItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Decreasequantity(int userid, int productid)
        {
            try
            {
                var user = await _context.users.Include(c => c.Cart)
                                    .ThenInclude(ci => ci.cartitems)
                                    .ThenInclude(p => p.Product)
                                    .FirstOrDefaultAsync(u => u.Id == userid);
                if (user == null)
                {
                    throw new Exception("user not found");
                }
                var item = user?.Cart?.cartitems?.FirstOrDefault(p => p.ProductId == productid);
                if (item == null)
                {
                    return false;

                }
                if (item.Quantity > 1)
                {
                    item.Quantity--;

                }
                else
                {
                    item.Quantity = 1;
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);



            }
        }
    }
}
