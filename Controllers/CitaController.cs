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
       .Include(c => c.Cliente) 
       .Include(c => c.Mascota) 
       .Select(c => new CitaDTO
       {
           CitaId = c.CitaId,
           Fecha = c.Fecha,
           ClienteId = c.ClienteId,
           MascotaId = c.MascotaId,
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
                .FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita == null)
            {
                return NotFound();
            }
            var citaDTO = new CitaDTO
            {
                CitaId = cita.CitaId,
                Fecha = cita.Fecha,
                ClienteId = cita.ClienteId,
                MascotaId = cita.MascotaId,
                Cliente = new ClienteDTO
                {
                    ClienteId = cita.Cliente.ClienteId,
                    Nombre = cita.Cliente.Nombre
                },
                Mascota = new MascotaDTO
                {
                    MascotaId = cita.Mascota.MascotaId,
                    Nombre = cita.Mascota.Nombre,
                    Especie = cita.Mascota.Especie,
                    Raza = cita.Mascota.Raza,
                    FechaNacimiento = cita.Mascota.FechaNacimiento,
                    ClienteId = cita.Mascota.ClienteId,
                    Cliente = new ClienteDTO
                    {
                        ClienteId = cita.Cliente.ClienteId,
                        Nombre = cita.Cliente.Nombre
                    },

                }


            };

            return Ok(citaDTO);
        }

        // POST: api/Cita
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita([FromBody] CitaRequest citaRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!citaRequest.ClienteId.HasValue || !citaRequest.MascotaId.HasValue)
                {
                    return BadRequest("ClienteId y MascotaId no pueden ser nulos.");
                }

             
            
                var nuevaCita = new Cita
                {
                    Fecha = citaRequest.Fecha,
                    ClienteId = citaRequest.ClienteId,
                    MascotaId = citaRequest.MascotaId,
                    

                };
                _context.Citas.Add(nuevaCita);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCita), new { id = nuevaCita.CitaId }, nuevaCita);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno al crear la mascota: {ex.Message}");
            }
            
        }

        // PUT: api/Cita/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, [FromBody] CitaRequest  citaRequest)
        {
            var citaExistente=await _context.Citas
                .Include(m=>m.Cliente)
                .Include(m=>m.Mascota)
                .FirstOrDefaultAsync(m=>m.CitaId==id);
            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada");
            }
            var clienteExistente = await _context.Clientes.FindAsync(citaRequest.ClienteId);
            if (clienteExistente == null)
            {
                return NotFound("Cliente no encontrado");
            }
            var mascotaExistente = await _context.Mascotas.FindAsync(citaRequest.MascotaId);
            if(mascotaExistente == null)
            {
                return NotFound("Mascota no encontrado");
            }
            citaExistente.Fecha=citaRequest.Fecha;
            citaExistente.ClienteId = citaRequest.ClienteId;
            citaExistente.MascotaId= citaRequest.MascotaId;
            await _context.SaveChangesAsync();

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
