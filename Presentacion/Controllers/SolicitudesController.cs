using BYCS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudRepositorio _EmergenciaRepositorio;

        public SolicitudesController(ISolicitudRepositorio EmergenciaRepositorio)
        {
            _EmergenciaRepositorio = EmergenciaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Emergencias activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetEmergencia()
        {
            var Emergencias = await _EmergenciaRepositorio.GetEmergencia();
            return Ok(Emergencias);
        }
        /// <summary>
        /// Obtiene un Emergencia por su codigo.
        /// </summary>
        [HttpGet("GET/{codigo}")]
        public async Task<IActionResult> GetEmergencia(string codigo)
        {
            var Emergencia = await _EmergenciaRepositorio.GetEmergencia(codigo);
            if (Emergencia is null)
                return NotFound($"No se encontró un Emergencia con codigo {codigo}.");

            return Ok(Emergencia);
        }

        /// <summary>
        /// Obtiene la lista de Emergencias marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetEmergenciasBorrados()
        {
            var Emergencias = await _EmergenciaRepositorio.GetEmergenciaBorrados();
            return Ok(Emergencias);
        }

        /// <summary>
        /// Crea un nuevo Emergencia.
        /// </summary>
        [HttpPost("POST/{codigo}/{ci}/{descripcion}")]
        public async Task<IActionResult> PostEmergencia(string codigo, string ci, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var EmergenciaCreado = await _EmergenciaRepositorio.PostEmergencia(codigo, ci, descripcion);
            if (EmergenciaCreado == null)
                return BadRequest("Algo salió mal. CI no existe o registro duplicado.");
            return CreatedAtAction(nameof(GetEmergencia), new { codigo = EmergenciaCreado.codigo }, EmergenciaCreado);
        }

        /// <summary>
        /// Actualiza un Emergencia existente.
        /// </summary>
        [HttpPut("PUT/{codigo}/{ci}/{descripcion}")]
        public async Task<IActionResult> PutEmergencia(string codigo, string ci, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var Emergencia = await _EmergenciaRepositorio.PutEmergencia(codigo, ci, descripcion);
            if (Emergencia is null)
                return NotFound($"No se encontró el Emergencia con codigo {codigo}.");

            return Ok(Emergencia);
        }

        /// <summary>
        /// Marca un Emergencia como borrado (eliminacodigoón lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo}")]
        public async Task<IActionResult> DeleteEmergencia(string codigo)
        {
            var EmergenciaEliminado = await _EmergenciaRepositorio.DeleteEmergencia(codigo);
            if (EmergenciaEliminado is null)
                return NotFound($"No se encontró un Emergencia con codigo {codigo}.");

            return Ok(EmergenciaEliminado);
        }
        /// <summary>
        /// Habilita un Emergencia previamente borrado (reactivacodigoón lógica).
        /// </summary>
        [HttpPut("HAB/{codigo}")]
        public async Task<IActionResult> HabilitarEmergencia(string codigo)
        {
            var Emergencia = await _EmergenciaRepositorio.HabilitarEmergencia(codigo);

            if (Emergencia is null)
                return NotFound($"No se encontró un Emergencia con codigo {codigo}.");

            return Ok(Emergencia);
        }
    }
}
