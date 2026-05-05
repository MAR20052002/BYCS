using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudProtocolosController : ControllerBase
    {
        private readonly ISolicitudProtocoloRepositorio _repo;

        public SolicitudProtocolosController(ISolicitudProtocoloRepositorio repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 📋 Obtener todos los activos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetSolicitudProtocolo();
            return Ok(data);
        }

        /// <summary>
        /// 🔍 Obtener por Protocolo y solicitud
        /// </summary>
        [HttpGet("{codigo_protocolo}/{codigo_solicitud}")]
        public async Task<IActionResult> Get(string codigo_protocolo, string codigo_solicitud)
        {
            if (string.IsNullOrWhiteSpace(codigo_protocolo) || string.IsNullOrWhiteSpace(codigo_solicitud))
                return BadRequest("Protocolo y solicitud son obligatorios.");

            var data = await _repo.GetSolicitudProtocolo(codigo_protocolo, codigo_solicitud);

            if (data is null)
                return NotFound($"No existe relación para protocolo: {codigo_protocolo} y solicitud: {codigo_solicitud}");

            return Ok(data);
        }

        /// <summary>
        /// 🗑️ Obtener borrados
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetBorrados()
        {
            var data = await _repo.GetSolicitudProtocoloBorrados();
            return Ok(data);
        }

        /// <summary>
        /// ➕ Crear relación protocolo-solicitud
        /// </summary>
        [HttpPost("{codigo_protocolo}/{codigo_solicitud}/{fecha_inicio}/{fecha_fin}")]
        public async Task<IActionResult> Post(string codigo_protocolo, string codigo_solicitud, string fecha_inicio, string fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(codigo_protocolo) || string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(fecha_inicio))
                return BadRequest("Protocolo y solicitud son obligatorios.");

            var creado = await _repo.PostSolicitudProtocolo(codigo_protocolo, codigo_solicitud, fecha_inicio, fecha_fin);

            if (creado is null)
                return BadRequest("No se pudo crear (protocolo/solicitud inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { codigo_protocolo, codigo_solicitud }, creado);
        }

        /// <summary>
        /// ✏️ Actualizar (fecha_fin)
        /// </summary>
        [HttpPut("{codigo_protocolo}/{codigo_solicitud}/{fecha_inicio}/{fecha_fin}")]
        public async Task<IActionResult> Put(string codigo_protocolo, string codigo_solicitud, string fecha_inicio, string fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(codigo_protocolo) || string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(fecha_inicio))
                return BadRequest("Datos incompletos.");

            var actualizado = await _repo.PutSolicitudProtocolo(codigo_protocolo, codigo_solicitud, fecha_inicio, fecha_fin);

            if (actualizado is null)
                return NotFound("Relación no encontrada.");

            return Ok(actualizado);
        }

        /// <summary>
        /// 🗑️ Eliminación lógica
        /// </summary>
        [HttpDelete("{codigo_protocolo}/{codigo_solicitud}")]
        public async Task<IActionResult> Delete(string codigo_protocolo, string codigo_solicitud)
        {
            if (string.IsNullOrWhiteSpace(codigo_protocolo) || string.IsNullOrWhiteSpace(codigo_solicitud))
                return BadRequest("Protocolo y solicitud son obligatorios.");

            var eliminado = await _repo.DeleteSolicitudProtocolo(codigo_protocolo, codigo_solicitud);

            if (eliminado is null)
                return NotFound("Relación no encontrada o ya eliminada.");

            return Ok(eliminado);
        }

        /// <summary>
        /// ♻️ Habilitar registro borrado
        /// </summary>
        [HttpPatch("habilitar/{codigo_protocolo}/{codigo_solicitud}")]
        public async Task<IActionResult> Habilitar(string codigo_protocolo, string codigo_solicitud)
        {
            if (string.IsNullOrWhiteSpace(codigo_protocolo) || string.IsNullOrWhiteSpace(codigo_solicitud))
                return BadRequest("Protocolo y solicitud son obligatorios.");

            var habilitado = await _repo.HabilitarSolicitudProtocolo(codigo_protocolo, codigo_solicitud);

            if (habilitado is null)
                return NotFound("No se encontró el registro borrado.");

            return Ok(habilitado);
        }
    }
}