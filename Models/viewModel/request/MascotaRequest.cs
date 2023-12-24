namespace veterinaria.Models.viewModel.request
{
    public class MascotaRequest
    {
        public string? Nombre { get; set; }
        public string? Especie { get; set; }
        public string? Raza { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int ClienteId { get; set; }
    }
}
