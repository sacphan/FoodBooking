using FoodBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodBooking.Reponsitory.Base
{
    public abstract class BaseReponsitory<T> : IBaseReponsitory<T> where T : class
    {
        protected readonly FoodBookingContext _context;
        private readonly DbSet<T> table;

        protected BaseReponsitory(FoodBookingContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<T?> FindByIdAsync(Guid id)
        {
            return await table.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Create(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Update(obj);
        }

        public void Delete(T obj)
        {
            table.Remove(obj);
        }

    }
}
