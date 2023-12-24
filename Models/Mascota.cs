using System;
using System.Collections.Generic;

namespace veterinaria.Models
{
    public partial class Mascota
    {
        public Mascota()
        {
            Cita = new HashSet<Cita>();
        }

        public int MascotaId { get; set; }
        public string? Nombre { get; set; }
        public string? Especie { get; set; }
        public string? Raza { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? ClienteId { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
    }
}
