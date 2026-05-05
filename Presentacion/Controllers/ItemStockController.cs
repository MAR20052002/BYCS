using BYCS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemStocksController : ControllerBase
    {
        private readonly IItemStockRepositorio _repo;

        public ItemStocksController(IItemStockRepositorio repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 📋 Obtener todos los activos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetItemStock();
            return Ok(data);
        }

        /// <summary>
        /// 🔍 Obtener por Respuesta y Informe
        /// </summary>
        [HttpGet("GET/{codigo_item}/{codigo_inventario}")]
        public async Task<IActionResult> Get(string codigo_item, string codigo_inventario)
        {
            if (string.IsNullOrWhiteSpace(codigo_item) || string.IsNullOrWhiteSpace(codigo_inventario))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var data = await _repo.GetItemStock(codigo_item, codigo_inventario);

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
            var data = await _repo.GetItemStockBorrados();
            return Ok(data);
        }
        [HttpGet("GET/Alerta")]
        public async Task<IActionResult> GetItemStockAlerta()
        {
            var data = await _repo.GetItemStockAlerta();
            return Ok(data);
        }
        [HttpGet("GET/AlertaVacios")]
        public async Task<IActionResult> GetItemStockAlertaVacios()
        {
            var data = await _repo.GetItemStockAlertaVacios();
            return Ok(data);
        }

        /// <summary>
        /// ➕ Crear relación Usuario-Teléfono
        /// </summary>
        [HttpPost("POST/{codigo_item}/{codigo_inventario}/{cantidad}")]
        public async Task<IActionResult> Post(string codigo_item, string codigo_inventario, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(codigo_item) || string.IsNullOrWhiteSpace(codigo_inventario))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var creado = await _repo.PostItemStock(codigo_item, codigo_inventario, cantidad);

            if (creado is null)
                return BadRequest("No se pudo crear (usuario/Informe inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { codigo_item, codigo_inventario }, creado);
        }

        /// <summary>
        /// ✏️ Actualizar (fecha_fin)
        /// </summary>
        [HttpPut("PUT/{codigo_item}/{codigo_inventario}/{cantidad}")]
        public async Task<IActionResult> Put(string codigo_item, string codigo_inventario, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(codigo_item) || string.IsNullOrWhiteSpace(codigo_inventario))
                return BadRequest("Datos incompletos.");

            var actualizado = await _repo.PutItemStock(codigo_item, codigo_inventario, cantidad);

            if (actualizado is null)
                return NotFound("Relación no encontrada.");

            return Ok(actualizado);
        }

        /// <summary>
        /// 🗑️ Eliminación lógica
        /// </summary>
        [HttpDelete("DEL/{codigo_item}/{codigo_inventario}")]
        public async Task<IActionResult> Delete(string codigo_item, string codigo_inventario)
        {
            if (string.IsNullOrWhiteSpace(codigo_item) || string.IsNullOrWhiteSpace(codigo_inventario))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var eliminado = await _repo.DeleteItemStock(codigo_item, codigo_inventario);

            if (eliminado is null)
                return NotFound("Relación no encontrada o ya eliminada.");

            return Ok(eliminado);
        }

        /// <summary>
        /// ♻️ Habilitar registro borrado
        /// </summary>
        [HttpPatch("habilitar/{codigo_item}/{codigo_inventario}")]
        public async Task<IActionResult> Habilitar(string codigo_item, string codigo_inventario)
        {
            if (string.IsNullOrWhiteSpace(codigo_item) || string.IsNullOrWhiteSpace(codigo_inventario))
                return BadRequest("Respuesta y Informe son obligatorios.");

            var habilitado = await _repo.HabilitarItemStock(codigo_item, codigo_inventario);

            if (habilitado is null)
                return NotFound("No se encontró el registro borrado.");

            return Ok(habilitado);
        }
    }
}