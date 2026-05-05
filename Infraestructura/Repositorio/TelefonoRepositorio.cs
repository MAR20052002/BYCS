using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class TelefonoRepositorio : ITelefonoRepositorio
    {
        private readonly BYCS_DBContext _context;

        public TelefonoRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<TelefonoDTO?> GetTelefono(string telf)
        {
            return await _context.Telefonos
                .AsNoTracking()
                .Where(p => p.telf == telf && p.estado != "Borrado")
                .Select(p => p.toTelefonoDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<TelefonoDTO>> GetTelefono()
        {
            return await _context.Telefonos
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toTelefonoDTO())
                .ToListAsync();
        }

        public async Task<List<TelefonoDTO>> GetTelefonoBorrados()
        {
            return await _context.Telefonos
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toTelefonoDTO())
                .ToListAsync();
        }

        public async Task<TelefonoDTO> PostTelefono(string telf)
        {
            if (string.IsNullOrWhiteSpace(telf))
                return null;
            var Telefono = new Telefono
            {
                telf = telf,
                estado = "Activo"
            };
            _context.Telefonos.Add(Telefono);
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }

        public async Task<TelefonoDTO?> PutTelefono(string telf, string telf_nuevo)
        {
            if (string.IsNullOrWhiteSpace(telf) || string.IsNullOrWhiteSpace(telf_nuevo))
                return null;
            var Telefono = await _context.Telefonos.FirstOrDefaultAsync(p => p.telf == telf && p.estado != "Borrado");
            if (Telefono == null)
                return null;
            Telefono.telf = telf_nuevo;
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }

        public async Task<TelefonoDTO?> DeleteTelefono(string telf)
        {
            var Telefono = await _context.Telefonos.FirstOrDefaultAsync(p => p.telf == telf && p.estado == "Activo");
            if (Telefono == null) return null;
            Telefono.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }

        public async Task<TelefonoDTO?> HabilitarTelefono(string telf)
        {
            var Telefono = await _context.Telefonos.FirstOrDefaultAsync(p => p.telf == telf && p.estado == "Borrado");
            if (Telefono == null) return null;
            Telefono.estado = "Activo";
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }
    }
}
