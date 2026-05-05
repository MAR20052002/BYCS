using Microsoft.AspNetCore.Mvc;
using BYCS.Core.Interfaces;
using BYCS.Core.DTOs;

namespace BYCS.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio UsuarioRepositorio)
        {
            _UsuarioRepositorio = UsuarioRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Usuarios activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetUsuario()
        {
            var Usuarios = await _UsuarioRepositorio.GetUsuario();
            return Ok(Usuarios);
        }
        /// <summary>
        /// Obtiene un Usuario por su CI.
        /// </summary>
        [HttpGet("GET/{ci}")]
        public async Task<IActionResult> GetUsuario(string ci)
        {
            var Usuario = await _UsuarioRepositorio.GetUsuario(ci);
            if (Usuario is null)
                return NotFound($"No se encontró un Usuario con CI {ci}.");

            return Ok(Usuario);
        }

        /// <summary>
        /// Obtiene la lista de Usuarios marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetUsuariosBorrados()
        {
            var Usuarios = await _UsuarioRepositorio.GetUsuarioBorrados();
            return Ok(Usuarios);
        }

        /// <summary>
        /// Crea un nuevo Usuario.
        /// </summary>
        [HttpPost("POST/{ci}/{nombre}/{apellido_p}/{apellido_m}")]
        public async Task<IActionResult> PostUsuario(string ci, string nombre, string? apellido_p, string? apellido_m)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Faltan campos.");

            var UsuarioCreado = await _UsuarioRepositorio.PostUsuario(ci, nombre, apellido_p, apellido_m);

            return CreatedAtAction(nameof(GetUsuario), new { ci = UsuarioCreado.ci }, UsuarioCreado);
        }

        /// <summary>
        /// Actualiza un Usuario existente.
        /// </summary>
        [HttpPut("PUT/{ci}/{nombre}/{apellido_p}/{apellido_m}")]
        public async Task<IActionResult> PutUsuario(string ci, string nombre, string? apellido_p, string? apellido_m)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Faltan campos.");

            var Usuario = await _UsuarioRepositorio.PutUsuario(ci, nombre, apellido_p, apellido_m);
            if (Usuario is null)
                return NotFound($"No se encontró el Usuario con CI {ci}.");

            return Ok(Usuario);
        }

        /// <summary>
        /// Marca un Usuario como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("DEL/{ci}")]
        public async Task<IActionResult> DeleteUsuario(string ci)
        {
            var UsuarioEliminado = await _UsuarioRepositorio.DeleteUsuario(ci);
            if (UsuarioEliminado is null)
                return NotFound($"No se encontró un Usuario con CI {ci}.");

            return Ok(UsuarioEliminado);
        }
        /// <summary>
        /// Habilita un Usuario previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("HAB/{ci}")]
        public async Task<IActionResult> HabilitarUsuario(string ci)
        {
            var Usuario = await _UsuarioRepositorio.HabilitarUsuario(ci);

            if (Usuario is null)
                return NotFound($"No se encontró un Usuario con CI {ci}.");

            return Ok(Usuario);
        }
        [HttpGet("GET/{ci}/Telefonos")]
        public async Task<IActionResult> GetUsuarioTelfCountDTO(string ci)
        {
            var UsuarioTelfCountDTO = await _UsuarioRepositorio.GetUsuarioTelfCountDTO(ci);
            if (UsuarioTelfCountDTO is null)
                return NotFound($"No se encontró un Usuario con CI {ci}.");

            return Ok(UsuarioTelfCountDTO);
        }
        [HttpGet("GET/{ci}/Correos")]
        public async Task<IActionResult> GetUsuarioConCorreos(string ci)
        {
            var UsuarioConCorreos = await _UsuarioRepositorio.GetUsuarioConCorreos(ci);
            if (UsuarioConCorreos is null)
                return NotFound($"No se encontró un Usuario con CI {ci}.");

            return Ok(UsuarioConCorreos);
        }
        [HttpGet("GET/{ci}/Contactos")]
        public async Task<IActionResult> GetUsuarioContactos(string ci)
        {
            var UsuarioContactos = await _UsuarioRepositorio.GetUsuarioContactos(ci);
            if (UsuarioContactos is null)
                return NotFound($"No se encontró un Usuario con CI {ci}.");

            return Ok(UsuarioContactos);
        }
        [HttpGet("GET/UsuariosConTelefonos")]
        public async Task<IActionResult> GetCantidadTelefonosPorUsuario()
        {
            List<UsuarioTelfCountDTO> UsuarioContactos = await _UsuarioRepositorio.GetCantidadTelefonosPorUsuario();
            if (UsuarioContactos is null)
                return NotFound($"No se encontró usuarios.");

            return Ok(UsuarioContactos);
        }
        [HttpGet("GET/UsuariosSinTelefonos")]
        public async Task<IActionResult> GetUsuariosSinTelefonos()
        {
            List<UsuarioDTO> UsuarioContactos = await _UsuarioRepositorio.GetUsuariosSinTelefonos();
            if (UsuarioContactos is null)
                return NotFound($"No se encontró usuarios.");

            return Ok(UsuarioContactos);
        }
        [HttpGet("GET/UsuariosConTelefonos/{ci}")]
        public async Task<IActionResult> GetCantidadTelefonosPorUsuario(string ci)
        {
            if (ci == null)
                return null;
            UsuarioTelfCountDTO? UsuarioContactos = await _UsuarioRepositorio.GetCantidadTelefonosPorUsuario(ci);
            if (UsuarioContactos is null)
                return NotFound($"No se encontró usuarios.");

            return Ok(UsuarioContactos);
        }
        [HttpGet("GET/UsuariosSumaTelefonos")]
        public async Task<IActionResult> GetSumaTelefonosPorUsuario()
        {
            List<UsuarioTelfSumaDTO> UsuarioContactos = await _UsuarioRepositorio.GetSumaTelefonosPorUsuario();
            if (UsuarioContactos is null)
                return NotFound($"No se encontró usuarios.");

            return Ok(UsuarioContactos);
        }
    }
}
