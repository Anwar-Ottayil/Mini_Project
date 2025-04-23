using MiniProject.Dto;
using MiniProject.Models;

namespace MiniProject.Services.CartService
{
    public interface ICartService
    {
        Task<ApiResponses<CartItems>> AddToCart(int productId, int Quantity);
        Task<List<CartViewDto>> GetCart(int userid);
        Task<bool> RemoveFromCart(int userId, int productId);
        Task<bool> Decreasequantity(int userid, int productid);
    }
}
