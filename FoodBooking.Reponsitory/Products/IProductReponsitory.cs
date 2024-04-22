using FoodBooking.Data.Entities;
using FoodBooking.Reponsitory.Base;

namespace FoodBooking.Reponsitory.Products
{
    public interface IProductReponsitory : IBaseReponsitory<Product>
    {
        Task<Product?> FindByNameAndRestaurantId(string name, Guid restaurantId);
    }
}
