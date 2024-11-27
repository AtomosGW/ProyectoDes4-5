using ProyectoDes4_5.Interfaz;
using ProyectoDes4_5.Models;
using System.Linq;
using System.Collections.Generic;

namespace ProyectoDes4_5.Repositorio
{
    public class AsignacionesRepository : IAsignacionesRepository
    {
        private readonly WebPedidosContext _context;

        public AsignacionesRepository(WebPedidosContext context)
        {
            _context = context;
        }

        public IEnumerable<Asignaciones> GetAllAsignaciones()
        {
            return _context.Asignaciones.ToList();
        }

        public Asignaciones GetAsignacionById(int id)
        {
            return _context.Asignaciones.FirstOrDefault(a => a.Id == id);
        }

        public void InsertAsignacion(Asignaciones asignacion)
        {
            _context.Asignaciones.Add(asignacion);
            _context.SaveChanges();
        }

        public void UpdateAsignacion(Asignaciones asignacion)
        {
            _context.Asignaciones.Update(asignacion);
            _context.SaveChanges();
        }

        public void DeleteAsignacion(int id)
        {
            var asignacion = GetAsignacionById(id);
            if (asignacion != null)
            {
                _context.Asignaciones.Remove(asignacion);
                _context.SaveChanges();
            }
        }
    }
}
