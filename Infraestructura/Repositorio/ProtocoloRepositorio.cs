using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class ProtocoloRepositorio : IProtocoloRepositorio
    {
        private readonly BYCS_DBContext _context;

        public ProtocoloRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<ProtocoloDTO?> GetProtocolo(string codigo)
        {
            return await _context.Protocolos
                .AsNoTracking()
                .Where(p => p.codigo == codigo && p.estado != "Borrado")
                .Select(p => p.toProtocoloDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProtocoloDTO>> GetProtocolo()
        {
            return await _context.Protocolos
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toProtocoloDTO())
                .ToListAsync();
        }

        public async Task<List<ProtocoloDTO>> GetProtocoloBorrados()
        {
            return await _context.Protocolos
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toProtocoloDTO())
                .ToListAsync();
        }

        public async Task<ProtocoloDTO> PostProtocolo(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            var Protocolo = new Protocolo
            {
                codigo = codigo,
                titulo = titulo,
                descripcion = descripcion,
                estado = "Activo"
            };
            _context.Protocolos.Add(Protocolo);
            await _context.SaveChangesAsync();
            return Protocolo.toProtocoloDTO();
        }

        public async Task<ProtocoloDTO?> PutProtocolo(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            var Protocolo = await _context.Protocolos.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado != "Borrado");
            if (Protocolo == null)
                return null;
            Protocolo.titulo = titulo ?? Protocolo.titulo;
            Protocolo.descripcion = descripcion ?? Protocolo.descripcion;
            await _context.SaveChangesAsync();
            return Protocolo.toProtocoloDTO();
        }

        public async Task<ProtocoloDTO?> DeleteProtocolo(string codigo)
        {
            var Protocolo = await _context.Protocolos.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Activo");
            if (Protocolo == null) return null;
            Protocolo.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Protocolo.toProtocoloDTO();
        }

        public async Task<ProtocoloDTO?> HabilitarProtocolo(string codigo)
        {
            var Protocolo = await _context.Protocolos.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Borrado");
            if (Protocolo == null) return null;
            Protocolo.estado = "Activo";
            await _context.SaveChangesAsync();
            return Protocolo.toProtocoloDTO();
        }
    }
}
