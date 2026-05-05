using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepositorio _ItemRepositorio;

        public ItemsController(IItemRepositorio ItemRepositorio)
        {
            _ItemRepositorio = ItemRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Items activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetItem()
        {
            var Items = await _ItemRepositorio.GetItem();
            return Ok(Items);
        }
        /// <summary>
        /// Obtiene un Item por su codigo.
        /// </summary>
        [HttpGet("GET/{codigo}")]
        public async Task<IActionResult> GetItem(string codigo)
        {
            var Item = await _ItemRepositorio.GetItem(codigo);
            if (Item is null)
                return NotFound($"No se encontró un Item {codigo}.");

            return Ok(Item);
        }

        /// <summary>
        /// Obtiene la lista de Items marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetItemsBorrados()
        {
            var Items = await _ItemRepositorio.GetItemBorrados();
            return Ok(Items);
        }

        /// <summary>
        /// Crea un nuevo Item.
        /// </summary>
        [HttpPost("POST/{codigo}/{nombre}/{descripcion}")]
        public async Task<IActionResult> PostItem(string codigo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var ItemCreado = await _ItemRepositorio.PostItem(codigo, nombre, descripcion);

            return CreatedAtAction(nameof(GetItem), new { codigo = ItemCreado.codigo }, ItemCreado);
        }

        /// <summary>
        /// Actualiza un Item existente.
        /// </summary>
        [HttpPut("PUT/{codigo}/{nombre}/{descripcion}")]
        public async Task<IActionResult> PutItem(string codigo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var Item = await _ItemRepositorio.PutItem(codigo, nombre, descripcion);
            if (Item is null)
                return NotFound($"No se encontró el Item {codigo}.");

            return Ok(Item);
        }

        /// <summary>
        /// Marca un Item como borrado (eliminacodigoón lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo}")]
        public async Task<IActionResult> DeleteItem(string codigo)
        {
            var ItemEliminado = await _ItemRepositorio.DeleteItem(codigo);
            if (ItemEliminado is null)
                return NotFound($"No se encontró un Item {codigo}.");

            return Ok(ItemEliminado);
        }
        /// <summary>
        /// Habilita un Item previamente borrado (reactivacodigoón lógica).
        /// </summary>
        [HttpPut("HAB/{codigo}")]
        public async Task<IActionResult> HabilitarItem(string codigo)
        {
            var Item = await _ItemRepositorio.HabilitarItem(codigo);

            if (Item is null)
                return NotFound($"No se encontró un Item {codigo}.");

            return Ok(Item);
        }
    }
}
