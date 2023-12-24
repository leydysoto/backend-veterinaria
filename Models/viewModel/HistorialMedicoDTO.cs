namespace veterinaria.Models.viewModel
{
    public class HistorialMedicoDTO
    {
        public int HistorialId { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }

       
        public CitaDTO Cita { get; set; }
    }
}
