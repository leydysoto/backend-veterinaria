using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veterinaria.Models;
using veterinaria.Models.viewModel;
using veterinaria.Models.viewModel.request;

namespace veterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly veterinariaContext _context;

        public MascotaController(veterinariaContext context)
        {
            _context = context;
        }

        // GET: api/Mascota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotas()
        {
             var mascotasDTO = await _context.Mascotas
            .Include(m => m.Cliente) // Incluye la información del cliente
            .Select(m => new MascotaDTO
            {
                MascotaId = m.MascotaId,
                Nombre = m.Nombre,
                Especie = m.Especie,
                Raza = m.Raza,
                FechaNacimiento = m.FechaNacimiento,
                ClienteId = m.ClienteId,
                Cliente = new ClienteDTO
                {
                    ClienteId = m.Cliente.ClienteId,
                    Nombre = m.Cliente.Nombre
                }
            })
            .ToListAsync();

                return Ok(mascotasDTO);
        }

        // GET: api/Mascota/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetMascota(int id)
        {
            var mascota = await _context.Mascotas
        .Include(m => m.Cliente)
        .FirstOrDefaultAsync(m => m.MascotaId == id);

            if (mascota == null)
            {
                return NotFound();
            }

            var mascotaDTO = new MascotaDTO
            {
                MascotaId = mascota.MascotaId,
                Nombre = mascota.Nombre,
                Especie = mascota.Especie,
                Raza = mascota.Raza,
                FechaNacimiento = mascota.FechaNacimiento,
                ClienteId = mascota.ClienteId,
                Cliente = new ClienteDTO
                {
                    ClienteId = mascota.Cliente.ClienteId,
                    Nombre = mascota.Cliente.Nombre
                }
            };

            return Ok(mascotaDTO);
        }

        // POST: api/Mascota
        [HttpPost]
        public async Task<ActionResult<Mascota>> PostMascota([FromBody] MascotaRequest mascotaRequest)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                
                var nuevaMascota = new Mascota
                {
                    Nombre = mascotaRequest.Nombre,
                    Especie = mascotaRequest.Especie,
                    Raza = mascotaRequest.Raza,
                    FechaNacimiento = mascotaRequest.FechaNacimiento,
                    ClienteId = mascotaRequest.ClienteId
                };

                
                _context.Mascotas.Add(nuevaMascota);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMascota), new { id = nuevaMascota.MascotaId }, nuevaMascota);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error interno al crear la mascota: {ex.Message}");
            }
        }

        // PUT: api/Mascota/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascota(int id, [FromBody] MascotaRequest mascotaRequest)
        {
            
            var mascotaExistente = await _context.Mascotas
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MascotaId == id);

            if (mascotaExistente == null)
            {
                return NotFound("Mascota no encontrada");
            }

            
            var clienteExistente = await _context.Clientes.FindAsync(mascotaRequest.ClienteId);

            if (clienteExistente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            
            mascotaExistente.Nombre = mascotaRequest.Nombre;
            mascotaExistente.Especie = mascotaRequest.Especie;
            mascotaExistente.Raza = mascotaRequest.Raza;
            mascotaExistente.FechaNacimiento = mascotaRequest.FechaNacimiento;
            mascotaExistente.Cliente = clienteExistente;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Mascota/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascota(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);

            if (mascota == null)
            {
                return NotFound();
            }

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascotas.Any(e => e.MascotaId == id);
        }

    }
}
