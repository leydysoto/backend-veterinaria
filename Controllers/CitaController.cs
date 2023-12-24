using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veterinaria.Models;
using veterinaria.Models.viewModel;

namespace veterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly veterinariaContext _context;

        public CitaController(veterinariaContext context)
        {
            _context = context;
        }

        // GET: api/Cita
        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            var citasDTO = await _context.Citas
       .Include(c => c.Cliente) // Incluye la información del cliente
       .Include(c => c.Mascota) // Incluye la información de la mascota
       .Select(c => new CitaDTO
       {
           CitaId = c.CitaId,
           Fecha = c.Fecha,
           Cliente = new ClienteDTO
           {
               ClienteId = c.Cliente.ClienteId ,
               Nombre = c.Cliente.Nombre
           },
           Mascota = new MascotaDTO
           {
               MascotaId = c.Mascota.MascotaId ,
               Nombre = c.Mascota.Nombre,
               Especie = c.Mascota.Especie,
               Raza = c.Mascota.Raza,
               FechaNacimiento = c.Mascota.FechaNacimiento,
               ClienteId = c.Mascota.ClienteId,
               Cliente = new ClienteDTO
               {
                   ClienteId = c.Cliente.ClienteId,
                   Nombre = c.Cliente.Nombre
               },

           }
           // Puedes incluir más propiedades según sea necesario
       })
       .ToListAsync();

            return Ok(citasDTO);
        }

        // GET: api/Cita/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCita(int id)
        {
            var cita = await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Mascota)
                .Include(c => c.HistorialMedicos)
                .FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }

        // POST: api/Cita
        [HttpPost]
        public async Task<IActionResult> PostCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCita), new { id = cita.CitaId }, cita);
        }

        // PUT: api/Cita/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.CitaId)
            {
                return BadRequest();
            }

            _context.Entry(cita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Cita/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);

            if (cita == null)
            {
                return NotFound();
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.CitaId == id);
        }
    }
}
