using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.BD;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDes4_5.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly DBContext _context;

        public BaseService(DBContext context)
        {
            _context = context;
        }

        // Método para obtener todos los elementos con relaciones incluidas
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Si se proporcionó un filtro para incluir relaciones, se aplica
            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        // Método para obtener por ID con relaciones incluidas
        public async Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        // Método para crear una nueva entidad
        public async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar una entidad
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        // Método para eliminar una entidad por ID
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
