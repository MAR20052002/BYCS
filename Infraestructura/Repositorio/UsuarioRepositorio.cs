using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BYCS_DBContext _context;

        public UsuarioRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDTO?> GetUsuario(string ci)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != "Borrado")
                .Select(p => p.toUsuarioDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<UsuarioDTO>> GetUsuario()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toUsuarioDTO())
                .ToListAsync();
        }

        public async Task<List<UsuarioDTO>> GetUsuarioBorrados()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toUsuarioDTO())
                .ToListAsync();
        }

        public async Task<UsuarioDTO> PostUsuario(string ci, string nombre, string? apellido_p, string? apellido_m)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre))
                return null;
            var Usuario = new Usuario
            {
                ci = ci,
                nombre = nombre,
                apellido_p = apellido_p ?? "-1",
                apellido_m = apellido_m ?? "-1",
                estado = "Activo"
            };
            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();
            return Usuario.toUsuarioDTO();
        }

        public async Task<UsuarioDTO?> PutUsuario(string ci, string nombre, string? apellido_p, string? apellido_m)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre))
                return null;
            var Usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.ci == ci && p.estado != "Borrado");
            if (Usuario == null)
                return null;
            Usuario.ci = ci;
            Usuario.nombre = nombre ?? Usuario.nombre;
            Usuario.apellido_p = apellido_p ?? Usuario.apellido_p;
            Usuario.apellido_m = apellido_m ?? Usuario.apellido_m;
            await _context.SaveChangesAsync();
            return Usuario.toUsuarioDTO();
        }

        public async Task<UsuarioDTO?> DeleteUsuario(string ci)
        {
            var Usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.ci == ci && p.estado == "Activo");
            if (Usuario == null) return null;
            Usuario.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Usuario.toUsuarioDTO();
        }

        public async Task<UsuarioDTO?> HabilitarUsuario(string ci)
        {
            var Usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.ci == ci && p.estado == "Borrado");
            if (Usuario == null) return null;
            Usuario.estado = "Activo";
            await _context.SaveChangesAsync();
            return Usuario.toUsuarioDTO();
        }
        public async Task<UsuarioTelefonosDTO?> GetUsuarioTelfCountDTO(string ci)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.ci == ci && u.estado == "Activo")
                .FirstOrDefaultAsync();

            if (usuario == null) return null;

            var telefonos = await _context.UsuarioTelefonos
                .Where(ut => ut.id_usuario == usuario.id_usuario && ut.estado != "Borrado")
                .Select(ut => ut.Telefono.toTelefonoDTO())
                .ToListAsync();

            return new UsuarioTelefonosDTO
            {
                Usuario = new List<UsuarioDTO> { usuario.toUsuarioDTO() },
                Telefono = telefonos
            };
        }
        public async Task<UsuarioCorreosDTO?> GetUsuarioConCorreos(string ci)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.ci == ci && u.estado == "Activo")
                .FirstOrDefaultAsync();

            if (usuario == null) return null;

            var correos = await _context.UsuarioCorreos
                .Where(uc => uc.id_usuario == usuario.id_usuario && uc.estado != "Borrado")
                .Select(uc => uc.Correo.toCorreoDTO())
                .ToListAsync();

            return new UsuarioCorreosDTO
            {
                Usuario = new List<UsuarioDTO> { usuario.toUsuarioDTO() },
                Correo = correos
            };
        }
        public async Task<UsuarioContactosDTO?> GetUsuarioContactos(string ci)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.ci == ci && u.estado == "Activo")
                .FirstOrDefaultAsync();

            if (usuario == null) return null;

            var telefonos = await _context.UsuarioTelefonos
                .Where(ut => ut.id_usuario == usuario.id_usuario && ut.estado != "Borrado")
                .Select(ut => ut.Telefono.toTelefonoDTO())
                .ToListAsync();

            var correos = await _context.UsuarioCorreos
                .Where(uc => uc.id_usuario == usuario.id_usuario && uc.estado != "Borrado")
                .Select(uc => uc.Correo.toCorreoDTO())
                .ToListAsync();

            return new UsuarioContactosDTO
            {
                Usuario = new List<UsuarioDTO> { usuario.toUsuarioDTO() },
                Telefono = telefonos,
                Correo = correos
            };
        }
        public async Task<List<UsuarioTelfCountDTO>>? GetCantidadTelefonosPorUsuario()
        {
            var query = await (
                from ut in _context.UsuarioTelefonos
                where ut.estado != "Borrado"
                group ut by ut.ci into grupo
                select new UsuarioTelfCountDTO
                {
                    ci = grupo.Key,
                    cantidadTelefonos = grupo.Count(),
                    Telefonos = grupo
                        .Where(x => x.Telefono.estado != "Borrado")
                        .Select(x => x.Telefono.toTelefonoDTO())
                        .ToList()
                }
            ).ToListAsync();
            if (query == null)
                return null;
            return query;
        }
        public async Task<UsuarioTelfCountDTO?> GetCantidadTelefonosPorUsuario(string ci)
        {
            var query = await (
                from ut in _context.UsuarioTelefonos
                where ut.estado != "Borrado" && ut.ci == ci
                group ut by ut.ci into grupo
                select new UsuarioTelfCountDTO
                {
                    ci = grupo.Key,
                    cantidadTelefonos = grupo.Count(),
                    Telefonos = grupo
                        .Where(x => x.Telefono.estado != "Borrado")
                        .Select(x => x.Telefono.toTelefonoDTO())
                        .ToList()
                }
            ).FirstOrDefaultAsync();

            return query;
        }
        public async Task<List<UsuarioTelfSumaDTO>> GetSumaTelefonosPorUsuario()
        {
            var query = await (
                from ut in _context.UsuarioTelefonos
                where ut.estado != "Borrado"
                group ut by ut.ci into grupo
                select new UsuarioTelfSumaDTO
                {
                    ci = grupo.Key,
                    cantidadTelefonos = grupo.Count(),

                    // SUM forzado (teléfono como número)
                    sumaCoeficientes = grupo.Sum(x =>
                        Convert.ToInt32(x.telf.Replace(" ", "").Replace("-", ""))
                    ),

                    Telefonos = grupo
                        .Select(x => x.Telefono.toTelefonoDTO())
                        .ToList()
                }
            ).ToListAsync();

            return query;
        }
        public async Task<List<UsuarioDTO>> GetUsuariosSinTelefonos()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(u => !_context.UsuarioTelefonos
                    .Any(ut => ut.id_usuario == u.id_usuario))
                .Select(u => u.toUsuarioDTO())
                .ToListAsync();
        }
    }
}
