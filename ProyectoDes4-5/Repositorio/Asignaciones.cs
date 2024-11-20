using System;

namespace ProyectoDes4_5.Repositorio
{
    public class Asignaciones
    {
        public int AssignmentId { get; set; }
        public int TicketId { get; set; }
        public int OperatorId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
