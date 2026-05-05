using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreosController : ControllerBase
    {
        private readonly ICorreoRepositorio _CorreoRepositorio;

        public CorreosController(ICorreoRepositorio CorreoRepositorio)
        {
            _CorreoRepositorio = CorreoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Correos activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetCorreo()
        {
            var Correos = await _CorreoRepositorio.GetCorreo();
            return Ok(Correos);
        }
        /// <summary>
        /// Obtiene un Correo por su correo.
        /// </summary>
        [HttpGet("GET/{correo}")]
        public async Task<IActionResult> GetCorreo(string correo)
        {
            var Correo = await _CorreoRepositorio.GetCorreo(correo);
            if (Correo is null)
                return NotFound($"No se encontró un Correo con correo {correo}.");

            return Ok(Correo);
        }

        /// <summary>
        /// Obtiene la lista de Correos marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetCorreosBorrados()
        {
            var Correos = await _CorreoRepositorio.GetCorreoBorrados();
            return Ok(Correos);
        }

        /// <summary>
        /// Crea un nuevo Correo.
        /// </summary>
        [HttpPost("POST/{correo}")]
        public async Task<IActionResult> PostCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return BadRequest("Faltan campos.");

            var CorreoCreado = await _CorreoRepositorio.PostCorreo(correo);

            return CreatedAtAction(nameof(GetCorreo), new { correo = CorreoCreado.correo }, CorreoCreado);
        }

        /// <summary>
        /// Actualiza un Correo existente.
        /// </summary>
        [HttpPut("PUT/{correo}/{correo_nuevo}")]
        public async Task<IActionResult> PutCorreo(string correo, string correo_nuevo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return BadRequest("Faltan campos.");

            var Correo = await _CorreoRepositorio.PutCorreo(correo, correo_nuevo);
            if (Correo is null)
                return NotFound($"No se encontró el Correo {correo}.");

            return Ok(Correo);
        }

        /// <summary>
        /// Marca un Correo como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("DEL/{correo}")]
        public async Task<IActionResult> DeleteCorreo(string correo)
        {
            var CorreoEliminado = await _CorreoRepositorio.DeleteCorreo(correo);
            if (CorreoEliminado is null)
                return NotFound($"No se encontró un Correo {correo}.");

            return Ok(CorreoEliminado);
        }
        /// <summary>
        /// Habilita un Correo previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("HAB/{correo}")]
        public async Task<IActionResult> HabilitarCorreo(string correo)
        {
            var Correo = await _CorreoRepositorio.HabilitarCorreo(correo);

            if (Correo is null)
                return NotFound($"No se encontró un Correo {correo}.");

            return Ok(Correo);
        }
    }
}
