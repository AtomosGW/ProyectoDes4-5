using Microsoft.AspNetCore.Mvc;
using System;

namespace ProyectoDes4_5.Models
{
    public class AsignacionesModels
    {
            public int AssignmentId { get; set; }
            public int TicketId { get; set; }
            public int OperatorId { get; set; }
            public DateTime AssignedAt { get; set; }

    }
}
