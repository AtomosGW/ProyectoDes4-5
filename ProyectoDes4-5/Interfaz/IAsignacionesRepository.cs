using Microsoft.AspNetCore.Mvc;
using System;

namespace ProyectoDes4_5.Interfaz
{
        public interface IAsignacionesRepository
        {
            IEnumerable<Asignaciones> GetAllAsignaciones();
            Asignaciones GetAsignacionById(int id);
            void InsertAsignacion(Asignaciones asignacion);
            void UpdateAsignacion(Asignaciones asignacion);
            void DeleteAsignacion(int id);
        }

}
