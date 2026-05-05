using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;
using BYCS.Core.Mapedores;

namespace BYCS.Infraestructura.Repositorio
{
    public class UsuarioCorreoRepositorio : IUsuarioCorreoRepositorio
    {
        private readonly BYCS_DBContext _context;

        public UsuarioCorreoRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioCorreoDTO>> GetUsuarioCorreo(string ci, string correo)
        {
            return await _context.UsuarioCorreos
                .AsNoTracking()
                .Where(x => x.ci == ci
                                      && x.correo == correo
                                      && x.estado != "Borrado")
                .Select(x => (x.toUsuarioCorreoDTO()))
                .ToListAsync();

        }

        public async Task<List<UsuarioCorreoDTO>> GetUsuarioCorreo()
        {
            return await _context.UsuarioCorreos
                .AsNoTracking()
                .Where(x => x.estado != "Borrado")
                .Select(x => (x.toUsuarioCorreoDTO()))
                .ToListAsync();
        }

        public async Task<List<UsuarioCorreoDTO>> GetUsuarioCorreoBorrados()
        {
            return await _context.UsuarioCorreos
                .AsNoTracking()
                .Where(x => x.estado == "Borrado")
                .Select(x => (x.toUsuarioCorreoDTO()))
                .ToListAsync();
        }

        public async Task<UsuarioCorreoDTO?> PostUsuarioCorreo(string ci, string correo, string fecha_inicio, string? fecha_fin)
        {
            Usuario? usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.ci == ci && u.estado == "Activo");

            Correo? Correo = await _context.Correos
                .FirstOrDefaultAsync(t => t.correo == correo && t.estado == "Activo");

            if (usuario == null || Correo == null)
                return null;
            if (DateOnly.TryParse(fecha_inicio, out DateOnly fecha))
            {
                var existe = await _context.UsuarioCorreos.AnyAsync(x =>
                x.ci == ci &&
                x.correo == correo &&
                x.fecha_inicio == fecha);

                if (existe)
                    return null;
                DateOnly? f2 = default;
                if (DateOnly.TryParse(fecha_fin, out DateOnly fecha2))
                    f2 = fecha2;
                else
                    f2 = null;
                var entity = new UsuarioCorreo
                {
                    id_usuario = usuario.id_usuario,
                    id_correo = Correo.id_correo,
                    ci = ci,
                    correo = correo,
                    fecha_inicio = fecha,
                    fecha_fin = f2,
                    estado = "Activo"
                };

                _context.UsuarioCorreos.Add(entity);
                await _context.SaveChangesAsync();

                return (entity.toUsuarioCorreoDTO());
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioCorreoDTO?> PutUsuarioCorreo(string ci, string correo, string fecha_inicio, string? fecha_fin)
        {
            if (DateOnly.TryParse(fecha_inicio, out DateOnly fecha))
            {
                var entity = await _context.UsuarioCorreos
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.correo == correo &&
                    x.fecha_inicio == fecha &&
                    x.estado != "Borrado");

                if (entity == null)
                    return null;
                if (DateOnly.TryParse(fecha_fin, out DateOnly fecha2))
                    entity.fecha_fin = fecha2;
                else
                    entity.fecha_fin = null;

                await _context.SaveChangesAsync();
                return (entity.toUsuarioCorreoDTO());
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioCorreoDTO?> DeleteUsuarioCorreo(string ci, string correo)
        {
            var entity = await _context.UsuarioCorreos
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.correo == correo &&
                    x.estado == "Activo");

            if (entity == null)
                return null;

            entity.estado = "Borrado";
            await _context.SaveChangesAsync();

            return (entity.toUsuarioCorreoDTO());
        }

        public async Task<UsuarioCorreoDTO?> HabilitarUsuarioCorreo(string ci, string correo)
        {
            var entity = await _context.UsuarioCorreos
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.correo == correo &&
                    x.estado == "Borrado");

            if (entity == null)
                return null;

            entity.estado = "Activo";
            await _context.SaveChangesAsync();

            return (entity.toUsuarioCorreoDTO());
        }
    }
}