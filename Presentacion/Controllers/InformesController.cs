using BYCS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformesController : ControllerBase
    {
        private readonly IInformeRepositorio _InformeRepositorio;

        public InformesController(IInformeRepositorio InformeRepositorio)
        {
            _InformeRepositorio = InformeRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Informes activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetInforme()
        {
            var Informes = await _InformeRepositorio.GetInforme();
            return Ok(Informes);
        }
        /// <summary>
        /// Obtiene un Informe por su codigo_informe.
        /// </summary>
        [HttpGet("GET/{codigo_informe}")]
        public async Task<IActionResult> GetInforme(string codigo_informe)
        {
            var Informe = await _InformeRepositorio.GetInforme(codigo_informe);
            if (Informe is null)
                return NotFound($"No se encontró un Informe {codigo_informe}.");

            return Ok(Informe);
        }

        /// <summary>
        /// Obtiene la lista de Informes marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetInformesBorrados()
        {
            var Informes = await _InformeRepositorio.GetInformeBorrados();
            return Ok(Informes);
        }

        /// <summary>
        /// Crea un nuevo Informe.
        /// </summary>
        [HttpPost("POST/{codigo_informe}/{titulo}/{descripcion}/{url}")]
        public async Task<IActionResult> PostInforme(string codigo_informe, string titulo, string descripcion, string url)
        {
            if (string.IsNullOrWhiteSpace(codigo_informe) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(url))
                return BadRequest("Faltan campos.");

            var InformeCreado = await _InformeRepositorio.PostInforme(codigo_informe, titulo, descripcion, url);

            return CreatedAtAction(nameof(GetInforme), new { codigo_informe = InformeCreado.codigo_informe }, InformeCreado);
        }

        /// <summary>
        /// Actualiza un Informe existente.
        /// </summary>
        [HttpPut("PUT/{codigo_informe}/{titulo}/{descripcion}/{url}")]
        public async Task<IActionResult> PutInforme(string codigo_informe, string titulo, string descripcion, string url)
        {
            if (string.IsNullOrWhiteSpace(codigo_informe) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(url))
                return BadRequest("Faltan campos.");

            var Informe = await _InformeRepositorio.PutInforme(codigo_informe, titulo, descripcion, url);
            if (Informe is null)
                return NotFound($"No se encontró el Informe {codigo_informe}.");

            return Ok(Informe);
        }

        /// <summary>
        /// Marca un Informe como borrado (eliminacodigo_informeón lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo_informe}")]
        public async Task<IActionResult> DeleteInforme(string codigo_informe)
        {
            var InformeEliminado = await _InformeRepositorio.DeleteInforme(codigo_informe);
            if (InformeEliminado is null)
                return NotFound($"No se encontró un Informe {codigo_informe}.");

            return Ok(InformeEliminado);
        }
        /// <summary>
        /// Habilita un Informe previamente borrado (reactivacodigo_informeón lógica).
        /// </summary>
        [HttpPut("HAB/{codigo_informe}")]
        public async Task<IActionResult> HabilitarInforme(string codigo_informe)
        {
            var Informe = await _InformeRepositorio.HabilitarInforme(codigo_informe);

            if (Informe is null)
                return NotFound($"No se encontró un Informe {codigo_informe}.");

            return Ok(Informe);
        }
    }
}
