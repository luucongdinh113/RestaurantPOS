using RestaurantPOS.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantPOS.Services
{
    public interface IFoodService
    {
        Task<List<FoodViewModel>> GetAllFoodAsync(string[] listCategory);
        Task<FoodViewModel> GetFoodByIdAsync(int id);
        Task InsertFoodAsync(ClaimsPrincipal user, FoodViewModel food);
        Task<List<FoodViewModel>> GetFoodByFilterAsync(string[] listcategory);
    }
}
