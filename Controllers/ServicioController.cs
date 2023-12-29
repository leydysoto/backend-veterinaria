using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veterinaria.Models;
using veterinaria.Models.viewModel.request;

namespace veterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly veterinariaContext _context;

        public ServicioController(veterinariaContext context)
        {
            _context = context;
        }

        // GET: api/Servicio
        [HttpGet]
        public async Task<IActionResult> GetServicios()
        {
            var servicios = await _context.Servicios.ToListAsync();
            return Ok(servicios);
        }

        // GET: api/Servicio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(servicio);
        }

        // POST: api/Servicio
        [HttpPost]
        public async Task<IActionResult> PostServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServicio), new { id = servicio.ServicioId }, servicio);
        }

        // PUT: api/Servicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, ServicioRequest servicioRequest)
        {
            try
            {

                var existingServicio = await _context.Servicios.FindAsync(id);

                if (existingServicio == null)
                {
                    return NotFound();
                }

                //PARA UN SETEO RAPIDO 

                _context.Entry(existingServicio).CurrentValues.SetValues(servicioRequest);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno al actualizar el servicio: {ex.Message}");
            }
        }

        // DELETE: api/Servicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.ServicioId == id);
        }
    }
}
