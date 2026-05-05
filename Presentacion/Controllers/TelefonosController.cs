using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly ITelefonoRepositorio _TelefonoRepositorio;

        public TelefonosController(ITelefonoRepositorio TelefonoRepositorio)
        {
            _TelefonoRepositorio = TelefonoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Telefonos activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetTelefono()
        {
            var Telefonos = await _TelefonoRepositorio.GetTelefono();
            return Ok(Telefonos);
        }
        /// <summary>
        /// Obtiene un Telefono por su telf.
        /// </summary>
        [HttpGet("GET/{telf}")]
        public async Task<IActionResult> GetTelefono(string telf)
        {
            var Telefono = await _TelefonoRepositorio.GetTelefono(telf);
            if (Telefono is null)
                return NotFound($"No se encontró un Telefono {telf}.");

            return Ok(Telefono);
        }

        /// <summary>
        /// Obtiene la lista de Telefonos marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetTelefonosBorrados()
        {
            var Telefonos = await _TelefonoRepositorio.GetTelefonoBorrados();
            return Ok(Telefonos);
        }

        /// <summary>
        /// Crea un nuevo Telefono.
        /// </summary>
        [HttpPost("POST/{telf}")]
        public async Task<IActionResult> PostTelefono(string telf)
        {
            if (string.IsNullOrWhiteSpace(telf))
                return BadRequest("Faltan campos.");

            var TelefonoCreado = await _TelefonoRepositorio.PostTelefono(telf);

            return CreatedAtAction(nameof(GetTelefono), new { telf = TelefonoCreado.telf }, TelefonoCreado);
        }

        /// <summary>
        /// Actualiza un Telefono existente.
        /// </summary>
        [HttpPut("PUT/{telf}/{telf_nuevo}")]
        public async Task<IActionResult> PutTelefono(string telf, string telf_nuevo)
        {
            if (string.IsNullOrWhiteSpace(telf))
                return BadRequest("Faltan campos.");

            var Telefono = await _TelefonoRepositorio.PutTelefono(telf, telf_nuevo);
            if (Telefono is null)
                return NotFound($"No se encontró el Telefono {telf}.");

            return Ok(Telefono);
        }

        /// <summary>
        /// Marca un Telefono como borrado (eliminatelfón lógica).
        /// </summary>
        [HttpDelete("DEL/{telf}")]
        public async Task<IActionResult> DeleteTelefono(string telf)
        {
            var TelefonoEliminado = await _TelefonoRepositorio.DeleteTelefono(telf);
            if (TelefonoEliminado is null)
                return NotFound($"No se encontró un Telefono {telf}.");

            return Ok(TelefonoEliminado);
        }
        /// <summary>
        /// Habilita un Telefono previamente borrado (reactivatelfón lógica).
        /// </summary>
        [HttpPut("HAB/{telf}")]
        public async Task<IActionResult> HabilitarTelefono(string telf)
        {
            var Telefono = await _TelefonoRepositorio.HabilitarTelefono(telf);

            if (Telefono is null)
                return NotFound($"No se encontró un Telefono {telf}.");

            return Ok(Telefono);
        }
    }
}
