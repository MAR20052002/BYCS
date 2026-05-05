using BYCS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaInformesController : ControllerBase
    {
        private readonly IRespuestaInformeRepositorio _repo;

        public RespuestaInformesController(IRespuestaInformeRepositorio repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 📋 Obtener todos los activos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetRespuestaInforme();
            return Ok(data);
        }

        /// <summary>
        /// 🔍 Obtener por Respuesta y Informe
        /// </summary>
        [HttpGet("GET/{codigo_solicitud}/{ci}/{codigo_informe}")]
        public async Task<IActionResult> Get(string codigo_solicitud, string ci, string codigo_informe)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(codigo_informe))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var data = await _repo.GetRespuestaInforme(codigo_solicitud, ci, codigo_informe);

            if (data is null)
                return NotFound($"No existe relación.");

            return Ok(data);
        }

        /// <summary>
        /// 🗑️ Obtener borrados
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetBorrados()
        {
            var data = await _repo.GetRespuestaInformeBorrados();
            return Ok(data);
        }

        /// <summary>
        /// ➕ Crear relación Usuario-Teléfono
        /// </summary>
        [HttpPost("POST/{codigo_solicitud}/{ci}/{codigo_informe}")]
        public async Task<IActionResult> Post(string codigo_solicitud, string ci, string codigo_informe)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(codigo_informe))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var creado = await _repo.PostRespuestaInforme(codigo_solicitud, ci, codigo_informe);

            if (creado is null)
                return BadRequest("No se pudo crear (usuario/Informe inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { codigo_solicitud, ci, codigo_informe }, creado);
        }

        /// <summary>
        /// ✏️ Actualizar (fecha_fin)
        /// </summary>
        [HttpPut("PUT/{codigo_solicitud}/{ci}/{codigo_informe}")]
        public async Task<IActionResult> Put(string codigo_solicitud, string ci, string codigo_informe)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(codigo_informe))
                return BadRequest("Datos incompletos.");

            var actualizado = await _repo.PutRespuestaInforme(codigo_solicitud, ci, codigo_informe);

            if (actualizado is null)
                return NotFound("Relación no encontrada.");

            return Ok(actualizado);
        }

        /// <summary>
        /// 🗑️ Eliminación lógica
        /// </summary>
        [HttpDelete("DEL/{codigo_solicitud}/{ci}/{codigo_informe}")]
        public async Task<IActionResult> Delete(string codigo_solicitud, string ci, string codigo_informe)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(codigo_informe))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var eliminado = await _repo.DeleteRespuestaInforme(codigo_solicitud, ci, codigo_informe);

            if (eliminado is null)
                return NotFound("Relación no encontrada o ya eliminada.");

            return Ok(eliminado);
        }

        /// <summary>
        /// ♻️ Habilitar registro borrado
        /// </summary>
        [HttpPatch("habilitar/{codigo_solicitud}/{ci}/{codigo_informe}")]
        public async Task<IActionResult> Habilitar(string codigo_solicitud, string ci, string codigo_informe)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(codigo_informe))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var habilitado = await _repo.HabilitarRespuestaInforme(codigo_solicitud, ci, codigo_informe);

            if (habilitado is null)
                return NotFound("No se encontró el registro borrado.");

            return Ok(habilitado);
        }
    }
}