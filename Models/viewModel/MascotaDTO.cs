namespace veterinaria.Models.viewModel
{
    public class MascotaDTO
    {
        public int MascotaId { get; set; }
        public string? Nombre { get; set; }
        public string? Especie { get; set; }
        public string? Raza { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? ClienteId { get; set; }

        
        public ClienteDTO Cliente { get; set; }
    }
}
