namespace FoodBooking.Reponsitory.Base
{
    public interface IBaseReponsitory<T> where T : class
    {
        Task<T> FindByIdAsync(Guid id);
        void Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        Task<int> SaveChangesAsync();

    }
}
