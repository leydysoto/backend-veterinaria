using System;
using System.Collections.Generic;

namespace veterinaria.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cita = new HashSet<Cita>();
            Mascota = new HashSet<Mascota>();
        }

        public int ClienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Mascota> Mascota { get; set; }
    }
}
