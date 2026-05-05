using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly IInventarioRepositorio _InventarioRepositorio;

        public InventariosController(IInventarioRepositorio InventarioRepositorio)
        {
            _InventarioRepositorio = InventarioRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Inventarios activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetInventario()
        {
            var Inventarios = await _InventarioRepositorio.GetInventario();
            return Ok(Inventarios);
        }
        /// <summary>
        /// Obtiene un Inventario por su codigo.
        /// </summary>
        [HttpGet("GET/{codigo}")]
        public async Task<IActionResult> GetInventario(string codigo)
        {
            var Inventario = await _InventarioRepositorio.GetInventario(codigo);
            if (Inventario is null)
                return NotFound($"No se encontró un Inventario {codigo}.");

            return Ok(Inventario);
        }

        /// <summary>
        /// Obtiene la lista de Inventarios marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetInventariosBorrados()
        {
            var Inventarios = await _InventarioRepositorio.GetInventarioBorrados();
            return Ok(Inventarios);
        }

        /// <summary>
        /// Crea un nuevo Inventario.
        /// </summary>
        [HttpPost("POST/{codigo}/{titulo}/{descripcion}")]
        public async Task<IActionResult> PostInventario(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var InventarioCreado = await _InventarioRepositorio.PostInventario(codigo, titulo, descripcion);

            return CreatedAtAction(nameof(GetInventario), new { codigo = InventarioCreado.codigo }, InventarioCreado);
        }

        /// <summary>
        /// Actualiza un Inventario existente.
        /// </summary>
        [HttpPut("PUT/{codigo}/{titulo}/{descripcion}")]
        public async Task<IActionResult> PutInventario(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var Inventario = await _InventarioRepositorio.PutInventario(codigo, titulo, descripcion);
            if (Inventario is null)
                return NotFound($"No se encontró el Inventario {codigo}.");

            return Ok(Inventario);
        }

        /// <summary>
        /// Marca un Inventario como borrado (eliminacodigoón lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo}")]
        public async Task<IActionResult> DeleteInventario(string codigo)
        {
            var InventarioEliminado = await _InventarioRepositorio.DeleteInventario(codigo);
            if (InventarioEliminado is null)
                return NotFound($"No se encontró un Inventario {codigo}.");

            return Ok(InventarioEliminado);
        }
        /// <summary>
        /// Habilita un Inventario previamente borrado (reactivacodigoón lógica).
        /// </summary>
        [HttpPut("HAB/{codigo}")]
        public async Task<IActionResult> HabilitarInventario(string codigo)
        {
            var Inventario = await _InventarioRepositorio.HabilitarInventario(codigo);

            if (Inventario is null)
                return NotFound($"No se encontró un Inventario {codigo}.");

            return Ok(Inventario);
        }
    }
}
