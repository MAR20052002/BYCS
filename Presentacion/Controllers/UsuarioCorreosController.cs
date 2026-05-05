using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioCorreosController : ControllerBase
    {
        private readonly IUsuarioCorreoRepositorio _repo;

        public UsuarioCorreosController(IUsuarioCorreoRepositorio repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 📋 Obtener todos los activos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetUsuarioCorreo();
            return Ok(data);
        }

        /// <summary>
        /// 🔍 Obtener por CI y teléfono
        /// </summary>
        [HttpGet("{ci}/{correo}")]
        public async Task<IActionResult> Get(string ci, string correo)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(correo))
                return BadRequest("CI y teléfono son obligatorios.");

            var data = await _repo.GetUsuarioCorreo(ci, correo);

            if (data is null)
                return NotFound($"No existe relación para CI: {ci} y Teléfono: {correo}");

            return Ok(data);
        }

        /// <summary>
        /// 🗑️ Obtener borrados
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetBorrados()
        {
            var data = await _repo.GetUsuarioCorreoBorrados();
            return Ok(data);
        }

        /// <summary>
        /// ➕ Crear relación Usuario-Teléfono
        /// </summary>
        [HttpPost("{ci}/{correo}/{fecha_inicio}")]
        public async Task<IActionResult> Post(string ci, string correo, string fecha_inicio, string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(fecha_inicio))
                return BadRequest("CI y teléfono son obligatorios.");

            var creado = await _repo.PostUsuarioCorreo(ci, correo, fecha_inicio, fecha_fin);

            if (creado is null)
                return BadRequest("No se pudo crear (usuario/teléfono inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { ci, correo }, creado);
        }

        /// <summary>
        /// ✏️ Actualizar (fecha_fin)
        /// </summary>
        [HttpPut("{ci}/{correo}/{fecha_inicio}")]
        public async Task<IActionResult> Put(string ci, string correo, string fecha_inicio, string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(fecha_inicio))
                return BadRequest("Datos incompletos.");

            var actualizado = await _repo.PutUsuarioCorreo(ci, correo, fecha_inicio, fecha_fin);

            if (actualizado is null)
                return NotFound("Relación no encontrada.");

            return Ok(actualizado);
        }

        /// <summary>
        /// 🗑️ Eliminación lógica
        /// </summary>
        [HttpDelete("{ci}/{correo}")]
        public async Task<IActionResult> Delete(string ci, string correo)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(correo))
                return BadRequest("CI y teléfono son obligatorios.");

            var eliminado = await _repo.DeleteUsuarioCorreo(ci, correo);

            if (eliminado is null)
                return NotFound("Relación no encontrada o ya eliminada.");

            return Ok(eliminado);
        }

        /// <summary>
        /// ♻️ Habilitar registro borrado
        /// </summary>
        [HttpPatch("habilitar/{ci}/{correo}")]
        public async Task<IActionResult> Habilitar(string ci, string correo)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(correo))
                return BadRequest("CI y teléfono son obligatorios.");

            var habilitado = await _repo.HabilitarUsuarioCorreo(ci, correo);

            if (habilitado is null)
                return NotFound("No se encontró el registro borrado.");

            return Ok(habilitado);
        }
    }
}