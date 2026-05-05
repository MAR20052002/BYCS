using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioTelefonosController : ControllerBase
    {
        private readonly IUsuarioTelefonoRepositorio _repo;

        public UsuarioTelefonosController(IUsuarioTelefonoRepositorio repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 📋 Obtener todos los activos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetUsuarioTelefono();
            return Ok(data);
        }

        /// <summary>
        /// 🔍 Obtener por CI y teléfono
        /// </summary>
        [HttpGet("{ci}/{telf}")]
        public async Task<IActionResult> Get(string ci, string telf)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(telf))
                return BadRequest("CI y teléfono son obligatorios.");

            var data = await _repo.GetUsuarioTelefono(ci, telf);

            if (data is null)
                return NotFound($"No existe relación para CI: {ci} y Teléfono: {telf}");

            return Ok(data);
        }

        /// <summary>
        /// 🗑️ Obtener borrados
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetBorrados()
        {
            var data = await _repo.GetUsuarioTelefonoBorrados();
            return Ok(data);
        }

        /// <summary>
        /// ➕ Crear relación Usuario-Teléfono
        /// </summary>
        [HttpPost("{ci}/{telf}/{fecha_inicio}")]
        public async Task<IActionResult> Post(string ci, string telf, string fecha_inicio, string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(telf) || string.IsNullOrWhiteSpace(fecha_inicio))
                return BadRequest("CI y teléfono son obligatorios.");

            var creado = await _repo.PostUsuarioTelefono(ci, telf, fecha_inicio, fecha_fin);

            if (creado is null)
                return BadRequest("No se pudo crear (usuario/teléfono inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { ci, telf }, creado);
        }

        /// <summary>
        /// ✏️ Actualizar (fecha_fin)
        /// </summary>
        [HttpPut("{ci}/{telf}/{fecha_inicio}")]
        public async Task<IActionResult> Put(string ci, string telf, string fecha_inicio, string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(telf) || string.IsNullOrWhiteSpace(fecha_inicio))
                return BadRequest("Datos incompletos.");

            var actualizado = await _repo.PutUsuarioTelefono(ci, telf, fecha_inicio, fecha_fin);

            if (actualizado is null)
                return NotFound("Relación no encontrada.");

            return Ok(actualizado);
        }

        /// <summary>
        /// 🗑️ Eliminación lógica
        /// </summary>
        [HttpDelete("{ci}/{telf}")]
        public async Task<IActionResult> Delete(string ci, string telf)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(telf))
                return BadRequest("CI y teléfono son obligatorios.");

            var eliminado = await _repo.DeleteUsuarioTelefono(ci, telf);

            if (eliminado is null)
                return NotFound("Relación no encontrada o ya eliminada.");

            return Ok(eliminado);
        }

        /// <summary>
        /// ♻️ Habilitar registro borrado
        /// </summary>
        [HttpPatch("habilitar/{ci}/{telf}")]
        public async Task<IActionResult> Habilitar(string ci, string telf)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(telf))
                return BadRequest("CI y teléfono son obligatorios.");

            var habilitado = await _repo.HabilitarUsuarioTelefono(ci, telf);

            if (habilitado is null)
                return NotFound("No se encontró el registro borrado.");

            return Ok(habilitado);
        }
    }
}