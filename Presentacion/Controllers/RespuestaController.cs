using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly IRespuestaRepositorio _RespuestaRepositorio;

        public RespuestasController(IRespuestaRepositorio RespuestaRepositorio)
        {
            _RespuestaRepositorio = RespuestaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Respuestas activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetRespuesta()
        {
            var Respuestas = await _RespuestaRepositorio.GetRespuesta();
            return Ok(Respuestas);
        }
        /// <summary>
        /// Obtiene un Respuesta por su codigo.
        /// </summary>
        [HttpGet("GET/{codigo_solicitud}/{ci}")]
        public async Task<IActionResult> GetRespuesta(string codigo_solicitud, string ci)
        {
            var Respuesta = await _RespuestaRepositorio.GetRespuesta(codigo_solicitud, ci);
            if (Respuesta is null)
                return NotFound($"No se encontró una Respuesta.");

            return Ok(Respuesta);
        }

        /// <summary>
        /// Obtiene la lista de Respuestas marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetRespuestasBorrados()
        {
            var Respuestas = await _RespuestaRepositorio.GetRespuestaBorrados();
            return Ok(Respuestas);
        }

        /// <summary>
        /// Crea un nuevo Respuesta.
        /// </summary>
        [HttpPost("POST/{codigo_solicitud}/{ci}/{aprobado}")]
        public async Task<IActionResult> PostRespuesta(string codigo_solicitud, string ci, bool aprobado)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci))
                return BadRequest("Faltan campos.");

            var RespuestaCreado = await _RespuestaRepositorio.PostRespuesta(codigo_solicitud, ci, aprobado);

            return CreatedAtAction(nameof(GetRespuesta), new { codigo_solicitud = RespuestaCreado.codigo_solicitud, ci = RespuestaCreado.ci }, RespuestaCreado);
        }

        /// <summary>
        /// Actualiza un Respuesta existente.
        /// </summary>
        [HttpPut("PUT/{codigo_solicitud}/{ci}/{aprobado}")]
        public async Task<IActionResult> PutRespuesta(string codigo_solicitud, string ci, bool aprobado)
{
            if ( string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci))
                return BadRequest("Faltan campos.");

            var Respuesta = await _RespuestaRepositorio.PutRespuesta(codigo_solicitud, ci, aprobado);
            if (Respuesta is null)
                return NotFound($"No se encontró la Respuesta.");

            return Ok(Respuesta);
        }

        /// <summary>
        /// Marca un Respuesta como borrado (eliminacodigoón lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo_solicitud}/{ci}")]
        public async Task<IActionResult> DeleteRespuesta(string codigo_solicitud, string ci)
        {
            var RespuestaEliminado = await _RespuestaRepositorio.DeleteRespuesta(codigo_solicitud, ci);
            if (RespuestaEliminado is null)
                return NotFound($"No se encontró la Respuesta.");

            return Ok(RespuestaEliminado);
        }
        /// <summary>
        /// Habilita un Respuesta previamente borrado (reactivacodigoón lógica).
        /// </summary>
        [HttpPut("HAB/{codigo_solicitud}/{ci}")]
        public async Task<IActionResult> HabilitarRespuesta(string codigo_solicitud, string ci)
        {
            var Respuesta = await _RespuestaRepositorio.HabilitarRespuesta(codigo_solicitud, ci);

            if (Respuesta is null)
                return NotFound($"No se encontró la Respuesta.");

            return Ok(Respuesta);
        }
    }
}
