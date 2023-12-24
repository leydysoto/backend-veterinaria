using System;
using System.Collections.Generic;

namespace veterinaria.Models
{
    public partial class Cita
    {
        public Cita()
        {
            HistorialMedicos = new HashSet<HistorialMedico>();
        }

        public int CitaId { get; set; }
        public DateTime? Fecha { get; set; }
        public int? ClienteId { get; set; }
        public int? MascotaId { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Mascota? Mascota { get; set; }
        public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; }
    }
}
