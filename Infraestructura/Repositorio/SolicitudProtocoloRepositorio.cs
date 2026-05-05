using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;
using BYCS.Core.Mapedores;

namespace BYCS.Infraestructura.Repositorio
{
    public class SolicitudProtocoloRepositorio : ISolicitudProtocoloRepositorio
    {
        private readonly BYCS_DBContext _context;

        public SolicitudProtocoloRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<List<SolicitudProtocoloDTO>> GetSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud)
        {
            return await _context.SolicitudProtocolos
                .AsNoTracking()
                .Where(x => x.codigo_protocolo == codigo_protocolo
                                      && x.codigo_solicitud == codigo_solicitud
                                      && x.estado != "Borrado")
                .Select(x => (x.toSolicitudProtocoloDTO()))
                .ToListAsync();

        }

        public async Task<List<SolicitudProtocoloDTO>> GetSolicitudProtocolo()
        {
            return await _context.SolicitudProtocolos
                .AsNoTracking()
                .Where(x => x.estado != "Borrado")
                .Select(x => (x.toSolicitudProtocoloDTO()))
                .ToListAsync();
        }

        public async Task<List<SolicitudProtocoloDTO>> GetSolicitudProtocoloBorrados()
        {
            return await _context.SolicitudProtocolos
                .AsNoTracking()
                .Where(x => x.estado == "Borrado")
                .Select(x => (x.toSolicitudProtocoloDTO()))
                .ToListAsync();
        }

        public async Task<SolicitudProtocoloDTO?> PostSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud, string fecha_inicio, string fecha_fin)
        {
            Protocolo? protocolo = await _context.Protocolos
                .FirstOrDefaultAsync(u => u.codigo == codigo_protocolo && u.estado == "Activo");

            Solicitud? Solicitud = await _context.Solicitudes
                .FirstOrDefaultAsync(t => t.codigo == codigo_solicitud && t.estado == "Activo");

            if (protocolo == null || Solicitud == null)
                return null;
            if (DateOnly.TryParse(fecha_inicio, out DateOnly fecha))
            {
                var existe = await _context.SolicitudProtocolos.AnyAsync(x =>
                x.codigo_protocolo == codigo_protocolo &&
                x.codigo_solicitud == codigo_solicitud &&
                x.fecha_inicio == fecha);

                if (existe)
                    return null;
                DateOnly f2 = default;
                if (DateOnly.TryParse(fecha_fin, out DateOnly fecha2))
                    f2 = fecha2;
                else
                    return null;
                var entity = new SolicitudProtocolo
                {
                    id_protocolo = protocolo.id_protocolo,
                    id_solicitud = Solicitud.id_solicitud,
                    codigo_protocolo = codigo_protocolo,
                    codigo_solicitud = codigo_solicitud,
                    fecha_inicio = fecha,
                    fecha_fin = f2,
                    estado = "Activo"
                };

                _context.SolicitudProtocolos.Add(entity);
                await _context.SaveChangesAsync();

                return (entity.toSolicitudProtocoloDTO());
            }
            else
            {
                return null;
            }
        }

        public async Task<SolicitudProtocoloDTO?> PutSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud, string fecha_inicio, string? fecha_fin)
        {
            if (DateOnly.TryParse(fecha_inicio, out DateOnly fecha))
            {
                var entity = await _context.SolicitudProtocolos
                .FirstOrDefaultAsync(x =>
                    x.codigo_protocolo == codigo_protocolo &&
                    x.codigo_solicitud == codigo_solicitud &&
                    x.fecha_inicio == fecha &&
                    x.estado != "Borrado");

                if (entity == null)
                    return null;
                if (DateOnly.TryParse(fecha_fin, out DateOnly fecha2))
                    entity.fecha_fin = fecha2;
                else
                    return null;

                await _context.SaveChangesAsync();
                return (entity.toSolicitudProtocoloDTO());
            }
            else
            {
                return null;
            }
        }

        public async Task<SolicitudProtocoloDTO?> DeleteSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud)
        {
            var entity = await _context.SolicitudProtocolos
                .FirstOrDefaultAsync(x =>
                    x.codigo_protocolo == codigo_protocolo &&
                    x.codigo_solicitud == codigo_solicitud &&
                    x.estado == "Activo");

            if (entity == null)
                return null;

            entity.estado = "Borrado";
            await _context.SaveChangesAsync();

            return (entity.toSolicitudProtocoloDTO());
        }

        public async Task<SolicitudProtocoloDTO?> HabilitarSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud)
        {
            var entity = await _context.SolicitudProtocolos
                .FirstOrDefaultAsync(x =>
                    x.codigo_protocolo == codigo_protocolo &&
                    x.codigo_solicitud == codigo_solicitud &&
                    x.estado == "Borrado");

            if (entity == null)
                return null;

            entity.estado = "Activo";
            await _context.SaveChangesAsync();

            return (entity.toSolicitudProtocoloDTO());
        }
    }
}