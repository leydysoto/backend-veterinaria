namespace veterinaria.Models.viewModel
{
    public class CitaDTO
    {
        public int CitaId { get; set; }
        public DateTime? Fecha { get; set; }

        public int? ClienteId { get; set; }
        public int? MascotaId { get; set; }


        public ClienteDTO Cliente { get; set; }

        
        public MascotaDTO Mascota { get; set; }
    }
}
