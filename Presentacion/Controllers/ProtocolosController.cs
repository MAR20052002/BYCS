using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolosController : ControllerBase
    {
        private readonly IProtocoloRepositorio _ProtocoloRepositorio;

        public ProtocolosController(IProtocoloRepositorio ProtocoloRepositorio)
        {
            _ProtocoloRepositorio = ProtocoloRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Protocolos activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetProtocolo()
        {
            var Protocolos = await _ProtocoloRepositorio.GetProtocolo();
            return Ok(Protocolos);
        }
        /// <summary>
        /// Obtiene un Protocolo por su codigo.
        /// </summary>
        [HttpGet("GET/{codigo}")]
        public async Task<IActionResult> GetProtocolo(string codigo)
        {
            var Protocolo = await _ProtocoloRepositorio.GetProtocolo(codigo);
            if (Protocolo is null)
                return NotFound($"No se encontró un Protocolo {codigo}.");

            return Ok(Protocolo);
        }

        /// <summary>
        /// Obtiene la lista de Protocolos marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetProtocolosBorrados()
        {
            var Protocolos = await _ProtocoloRepositorio.GetProtocoloBorrados();
            return Ok(Protocolos);
        }

        /// <summary>
        /// Crea un nuevo Protocolo.
        /// </summary>
        [HttpPost("POST/{codigo}")]
        public async Task<IActionResult> PostProtocolo(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var ProtocoloCreado = await _ProtocoloRepositorio.PostProtocolo(codigo, titulo, descripcion);

            return CreatedAtAction(nameof(GetProtocolo), new { codigo = ProtocoloCreado.codigo }, ProtocoloCreado);
        }

        /// <summary>
        /// Actualiza un Protocolo existente.
        /// </summary>
        [HttpPut("PUT/{codigo}/{codigo_nuevo}")]
        public async Task<IActionResult> PutProtocolo(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Faltan campos.");

            var Protocolo = await _ProtocoloRepositorio.PutProtocolo(codigo, titulo, descripcion);
            if (Protocolo is null)
                return NotFound($"No se encontró el Protocolo {codigo}.");

            return Ok(Protocolo);
        }

        /// <summary>
        /// Marca un Protocolo como borrado (eliminacodigoón lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo}")]
        public async Task<IActionResult> DeleteProtocolo(string codigo)
        {
            var ProtocoloEliminado = await _ProtocoloRepositorio.DeleteProtocolo(codigo);
            if (ProtocoloEliminado is null)
                return NotFound($"No se encontró un Protocolo {codigo}.");

            return Ok(ProtocoloEliminado);
        }
        /// <summary>
        /// Habilita un Protocolo previamente borrado (reactivacodigoón lógica).
        /// </summary>
        [HttpPut("HAB/{codigo}")]
        public async Task<IActionResult> HabilitarProtocolo(string codigo)
        {
            var Protocolo = await _ProtocoloRepositorio.HabilitarProtocolo(codigo);

            if (Protocolo is null)
                return NotFound($"No se encontró un Protocolo {codigo}.");

            return Ok(Protocolo);
        }
    }
}
