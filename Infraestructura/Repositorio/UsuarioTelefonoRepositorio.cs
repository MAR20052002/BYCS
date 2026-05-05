using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;
using BYCS.Core.Mapedores;

namespace BYCS.Infraestructura.Repositorio
{
    public class UsuarioTelefonoRepositorio : IUsuarioTelefonoRepositorio
    {
        private readonly BYCS_DBContext _context;

        public UsuarioTelefonoRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioTelefonoDTO>> GetUsuarioTelefono(string ci, string telf)
        {
            return await _context.UsuarioTelefonos
                .AsNoTracking()
                .Where(x => x.ci == ci
                                      && x.telf == telf
                                      && x.estado != "Borrado")
                .Select(x => (x.toUsuarioTelefonoDTO()))
                .ToListAsync();

        }

        public async Task<List<UsuarioTelefonoDTO>> GetUsuarioTelefono()
        {
            return await _context.UsuarioTelefonos
                .AsNoTracking()
                .Where(x => x.estado != "Borrado")
                .Select(x => (x.toUsuarioTelefonoDTO()))
                .ToListAsync();
        }

        public async Task<List<UsuarioTelefonoDTO>> GetUsuarioTelefonoBorrados()
        {
            return await _context.UsuarioTelefonos
                .AsNoTracking()
                .Where(x => x.estado == "Borrado")
                .Select(x => (x.toUsuarioTelefonoDTO()))
                .ToListAsync();
        }

        public async Task<UsuarioTelefonoDTO?> PostUsuarioTelefono(string ci, string telf, string fecha_inicio, string? fecha_fin)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.ci == ci && u.estado == "Activo");

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(t => t.telf == telf && t.estado == "Activo");

            if (usuario == null || telefono == null)
                return null;
            if (DateOnly.TryParse(fecha_inicio, out DateOnly fecha))
            {
                var existe = await _context.UsuarioTelefonos.AnyAsync(x =>
                x.ci == ci &&
                x.telf == telf &&
                x.fecha_inicio == fecha);

                if (existe)
                    return null;
                DateOnly? f2 = default;
                if (DateOnly.TryParse(fecha_fin, out DateOnly fecha2))
                    f2 = fecha2;
                else
                    f2 = null;
                var entity = new UsuarioTelefono
                {
                    id_usuario = usuario.id_usuario,
                    id_telefono = telefono.id_telefono,
                    ci = ci,
                    telf = telf,
                    fecha_inicio = fecha,
                    fecha_fin = f2,
                    estado = "Activo"
                };

                _context.UsuarioTelefonos.Add(entity);
                await _context.SaveChangesAsync();

                return (entity.toUsuarioTelefonoDTO());
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioTelefonoDTO?> PutUsuarioTelefono(string ci, string telf, string fecha_inicio, string? fecha_fin)
        {
            if (DateOnly.TryParse(fecha_inicio, out DateOnly fecha))
            {
                UsuarioTelefono? entity = await _context.UsuarioTelefonos
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.telf == telf &&
                    x.fecha_inicio == fecha &&
                    x.estado != "Borrado");
                if (entity == null)
                    return null;
                if (DateOnly.TryParse(fecha_fin, out DateOnly fecha2))
                    entity.fecha_fin = fecha2;
                else
                    entity.fecha_fin = null;
                /*Usuario? u = await _context.Usuarios.FirstOrDefaultAsync(x => x.ci == ci);
                if (u == null)
                    return null;
                Telefono? t = await _context.Telefonos.FirstOrDefaultAsync(x => x.telf == telf);
                if (t == null)
                    return null;*/

                await _context.SaveChangesAsync();
                return (entity.toUsuarioTelefonoDTO());
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioTelefonoDTO?> DeleteUsuarioTelefono(string ci, string telf)
        {
            var entity = await _context.UsuarioTelefonos
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.telf == telf &&
                    x.estado == "Activo");

            if (entity == null)
                return null;

            entity.estado = "Borrado";
            await _context.SaveChangesAsync();

            return (entity.toUsuarioTelefonoDTO());
        }

        public async Task<UsuarioTelefonoDTO?> HabilitarUsuarioTelefono(string ci, string telf)
        {
            var entity = await _context.UsuarioTelefonos
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.telf == telf &&
                    x.estado == "Borrado");

            if (entity == null)
                return null;

            entity.estado = "Activo";
            await _context.SaveChangesAsync();

            return (entity.toUsuarioTelefonoDTO());
        }
    }
}