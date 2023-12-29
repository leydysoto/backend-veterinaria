using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veterinaria.Models;
using veterinaria.Models.viewModel;

namespace veterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialMedicoController : ControllerBase
    {
        private readonly veterinariaContext _context;

        public HistorialMedicoController(veterinariaContext context)
        {
            _context = context;
        }

        // GET: api/HistorialMedico
        [HttpGet]
        public async Task<IActionResult> GetHistorialesMedicos()
        {
            var historialesDTO = await _context.HistorialMedicos
          .Include(h => h.Cita) 
          .Select(h => new HistorialMedicoDTO
          {
              HistorialId = h.HistorialId,
              Fecha = h.Fecha,
              Descripcion = h.Descripcion,
              Diagnostico = h.Diagnostico,
              Tratamiento = h.Tratamiento,
              Cita = new CitaDTO
              {
                  CitaId = h.Cita.CitaId  ,
                  Fecha = h.Cita.Fecha,
                  ClienteId= h.Cita.ClienteId ,
                  MascotaId = h.Cita.MascotaId ,
                  Cliente = new ClienteDTO
                  {
                      ClienteId = h.Cita.Cliente.ClienteId ,
                      Nombre = h.Cita.Cliente.Nombre
                  },
                  Mascota = new MascotaDTO
                  {
                      MascotaId = h.Cita.Mascota.MascotaId ,
                      Nombre = h.Cita.Mascota.Nombre,
                      Especie = h.Cita.Mascota.Especie,
                      Raza = h.Cita.Mascota.Raza,
                      FechaNacimiento = h.Cita.Mascota.FechaNacimiento,
                      ClienteId = h.Cita.Mascota.ClienteId
                  }
              }
              
          })
          .ToListAsync();

            return Ok(historialesDTO);
        }

        // GET: api/HistorialMedico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialMedico>> GetHistorialMedico(int id)
        {
            var historialMedico = await _context.HistorialMedicos
                .Include(h => h.Cita)
                .FirstOrDefaultAsync(h => h.HistorialId == id);

            if (historialMedico == null)
            {
                return NotFound();
            }
            var historialMedicoDTO = new HistorialMedicoDTO
            {
                HistorialId = historialMedico.HistorialId,
                Fecha = historialMedico.Fecha,
                Descripcion = historialMedico.Descripcion,
                Diagnostico = historialMedico.Diagnostico,
                Tratamiento = historialMedico.Tratamiento,
                Cita =  new CitaDTO
                {
                    CitaId = historialMedico.Cita.CitaId,
                    Fecha = historialMedico.Cita.Fecha,
                    ClienteId = historialMedico.Cita.ClienteId,
                    MascotaId = historialMedico.Cita.MascotaId,
                    Cliente= new ClienteDTO
                    {
                        ClienteId = historialMedico.Cita.Cliente.ClienteId,
                        Nombre = historialMedico.Cita.Cliente.Nombre
                    },
                    
                    Mascota =  new MascotaDTO
                    {
                        MascotaId = historialMedico.Cita.Mascota.MascotaId,
                        Nombre = historialMedico.Cita.Mascota.Nombre,
                        Especie = historialMedico.Cita.Mascota.Especie,
                        Raza = historialMedico.Cita.Mascota.Raza,
                        FechaNacimiento = historialMedico.Cita.Mascota.FechaNacimiento,
                        ClienteId = historialMedico.Cita.Mascota.ClienteId
                    }
                    
                }
            };

            return Ok(historialMedicoDTO);
        }

        // POST: api/HistorialMedico
        [HttpPost]
        public async Task<IActionResult> PostHistorialMedico(HistorialMedico historialMedico)
        {
            _context.HistorialMedicos.Add(historialMedico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorialMedico), new { id = historialMedico.HistorialId }, historialMedico);
        }

        // PUT: api/HistorialMedico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialMedico(int id, HistorialMedico historialMedico)
        {
            if (id != historialMedico.HistorialId)
            {
                return BadRequest();
            }

            _context.Entry(historialMedico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialMedicoExists(id))
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

        // DELETE: api/HistorialMedico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialMedico(int id)
        {
            var historialMedico = await _context.HistorialMedicos.FindAsync(id);

            if (historialMedico == null)
            {
                return NotFound();
            }

            _context.HistorialMedicos.Remove(historialMedico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialMedicoExists(int id)
        {
            return _context.HistorialMedicos.Any(e => e.HistorialId == id);
        }
    }
}
