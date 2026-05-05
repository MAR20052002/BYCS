using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class CorreoRepositorio : ICorreoRepositorio
    {
        private readonly BYCS_DBContext _context;

        public CorreoRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<CorreoDTO?> GetCorreo(string correo)
        {
            return await _context.Correos
                .AsNoTracking()
                .Where(p => p.correo == correo && p.estado != "Borrado")
                .Select(p => p.toCorreoDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<CorreoDTO>> GetCorreo()
        {
            return await _context.Correos
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toCorreoDTO())
                .ToListAsync();
        }

        public async Task<List<CorreoDTO>> GetCorreoBorrados()
        {
            return await _context.Correos
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toCorreoDTO())
                .ToListAsync();
        }

        public async Task<CorreoDTO> PostCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return null;
            var Correo = new Correo
            {
                correo = correo,
                estado = "Activo"
            };
            _context.Correos.Add(Correo);
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }

        public async Task<CorreoDTO?> PutCorreo(string correo, string correo_nuevo)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(correo_nuevo))
                return null;
            var Correo = await _context.Correos.FirstOrDefaultAsync(p => p.correo == correo && p.estado != "Borrado");
            if (Correo == null)
                return null;
            Correo.correo = correo_nuevo;
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }

        public async Task<CorreoDTO?> DeleteCorreo(string correo)
        {
            var Correo = await _context.Correos.FirstOrDefaultAsync(p => p.correo == correo && p.estado == "Activo");
            if (Correo == null) return null;
            Correo.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }

        public async Task<CorreoDTO?> HabilitarCorreo(string correo)
        {
            var Correo = await _context.Correos.FirstOrDefaultAsync(p => p.correo == correo && p.estado == "Borrado");
            if (Correo == null) return null;
            Correo.estado = "Activo";
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }
    }
}
