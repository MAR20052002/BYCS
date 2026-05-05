using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class SolicitudRepositorio : ISolicitudRepositorio
    {
        private readonly BYCS_DBContext _context;

        public SolicitudRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<SolicitudDTO?> GetEmergencia(string codigo)
        {
            return await _context.Solicitudes
                .AsNoTracking()
                .Where(p => p.codigo == codigo && p.estado != "Borrado")
                .Select(p => p.toSolicitudDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<SolicitudDTO>> GetEmergencia()
        {
            return await _context.Solicitudes
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toSolicitudDTO())
                .ToListAsync();
        }

        public async Task<List<SolicitudDTO>> GetEmergenciaBorrados()
        {
            return await _context.Solicitudes
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toSolicitudDTO())
                .ToListAsync();
        }

        public async Task<SolicitudDTO> PostEmergencia(string codigo, string ci, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            Usuario? usuario = await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != "Borrado")
                .Select(p => p)
                .FirstOrDefaultAsync();
            if (usuario == null)
                return null;
            var Solicitud = new Solicitud
            {
                id_usuario = usuario.id_usuario,
                codigo = codigo,
                ci = usuario.ci,
                descripcion = descripcion,
                estado = "Activo"
            };
            _context.Solicitudes.Add(Solicitud);
            await _context.SaveChangesAsync();
            return Solicitud.toSolicitudDTO();
        }

        public async Task<SolicitudDTO?> PutEmergencia(string codigo, string ci, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            Usuario? usuario = await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != "Borrado")
                .Select(p => p)
                .FirstOrDefaultAsync();
            if (usuario == null)
                return null;
            Solicitud? Solicitud = await _context.Solicitudes.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado != "Borrado");
            if (Solicitud == null)
                return null;
            Solicitud.id_usuario = usuario.id_usuario;
            Solicitud.codigo = codigo ?? Solicitud.codigo;
            Solicitud.ci = usuario.ci ?? Solicitud.ci;
            Solicitud.descripcion = descripcion ?? Solicitud.descripcion;
            await _context.SaveChangesAsync();
            return Solicitud.toSolicitudDTO();
        }

        public async Task<SolicitudDTO?> DeleteEmergencia(string codigo)
        {
            var Solicitud = await _context.Solicitudes.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Activo");
            if (Solicitud == null) return null;
            Solicitud.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Solicitud.toSolicitudDTO();
        }

        public async Task<SolicitudDTO?> HabilitarEmergencia(string codigo)
        {
            var Solicitud = await _context.Solicitudes.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Borrado");
            if (Solicitud == null) return null;
            Solicitud.estado = "Activo";
            await _context.SaveChangesAsync();
            return Solicitud.toSolicitudDTO();
        }
    }
}
