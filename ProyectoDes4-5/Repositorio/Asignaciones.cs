using System;

namespace ProyectoDes4_5.Repositorio
{
    public class Asignaciones
    {
        public int AssignmentId { get; set; }  // Esta propiedad será la clave primaria
        public int TicketId { get; set; }
        public int OperatorId { get; set; }
        public DateTime AssignedAt { get; set; }
    }

}
