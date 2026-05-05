using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;
using BYCS.Core.Mapedores;

namespace BYCS.Infraestructura.Repositorio
{
    public class RespuestaInformeRepositorio : IRespuestaInformeRepositorio
    {
        private readonly BYCS_DBContext _context;

        public RespuestaInformeRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<List<RespuestaInformeDTO>> GetRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe)
        {
            return await _context.RespuestaInformes
                .AsNoTracking()
                .Where(x => x.codigo_solicitud == codigo_solicitud
                                      && x.codigo_informe == codigo_informe
                                      && x.ci == ci
                                      && x.estado != "Borrado")
                .Select(x => (x.toRespuestaInformeDTO()))
                .ToListAsync();

        }

        public async Task<List<RespuestaInformeDTO>> GetRespuestaInforme()
        {
            return await _context.RespuestaInformes
                .AsNoTracking()
                .Where(x => x.estado != "Borrado")
                .Select(x => (x.toRespuestaInformeDTO()))
                .ToListAsync();
        }

        public async Task<List<RespuestaInformeDTO>> GetRespuestaInformeBorrados()
        {
            return await _context.RespuestaInformes
                .AsNoTracking()
                .Where(x => x.estado == "Borrado")
                .Select(x => (x.toRespuestaInformeDTO()))
                .ToListAsync();
        }

        public async Task<RespuestaInformeDTO?> PostRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe)
        {
            Respuesta? respuesta = await _context.Respuestas
                .FirstOrDefaultAsync(u => u.codigo_solicitud == codigo_solicitud && u.ci == ci && u.estado == "Activo");
            if (respuesta == null)
                return null;
            Informe? informe = await _context.Informes
                .FirstOrDefaultAsync(u => u.codigo_informe == codigo_informe && u.estado == "Activo");
            if (informe == null)
                return null;
            RespuestaInforme respuestainforme = new RespuestaInforme
            {
                id_solicitud = respuesta.id_solicitud,
                id_usuario = respuesta.id_usuario,
                id_informe = informe.id_informe,
                codigo_solicitud = respuesta.codigo_solicitud,
                ci = respuesta.ci,
                codigo_informe = informe.codigo_informe,
                estado = "Activo"
            };

            _context.RespuestaInformes.Add(respuestainforme);
            await _context.SaveChangesAsync();

            return (respuestainforme.toRespuestaInformeDTO());
        }

        public async Task<RespuestaInformeDTO?> PutRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe)
        {
            RespuestaInforme? respuestainforme = await _context.RespuestaInformes
                .FirstOrDefaultAsync(x => x.codigo_solicitud == codigo_solicitud
                                      && x.codigo_informe == codigo_informe
                                      && x.ci == ci
                                      && x.estado != "Borrado");

            Respuesta? respuesta = await _context.Respuestas
                .FirstOrDefaultAsync(u => u.codigo_solicitud == codigo_solicitud && u.ci == ci && u.estado == "Activo");
            if (respuesta == null)
                return null;
            Informe? informe = await _context.Informes
                .FirstOrDefaultAsync(u => u.codigo_informe == codigo_informe && u.estado == "Activo");
            if (informe == null)
                return null;
            else
            {
                respuestainforme.id_solicitud = respuesta.id_solicitud;
                respuestainforme.id_usuario = respuesta.id_solicitud;
                respuestainforme.id_informe = informe.id_informe;
                respuestainforme.codigo_solicitud = respuesta.codigo_solicitud;
                respuestainforme.ci = respuesta.ci;
                respuestainforme.codigo_informe = informe.codigo_informe;

                await _context.SaveChangesAsync();
                return (respuestainforme.toRespuestaInformeDTO());
            }
        }

        public async Task<RespuestaInformeDTO?> DeleteRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe)
        {
            var entity = await _context.RespuestaInformes
                .FirstOrDefaultAsync(x => x.codigo_solicitud == codigo_solicitud
                                      && x.codigo_informe == codigo_informe
                                      && x.ci == ci 
                                      && x.estado == "Activo");

            if (entity == null)
                return null;

            entity.estado = "Borrado";
            await _context.SaveChangesAsync();

            return (entity.toRespuestaInformeDTO());
        }

        public async Task<RespuestaInformeDTO?> HabilitarRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe)
        {
            var entity = await _context.RespuestaInformes
                .FirstOrDefaultAsync(x => x.codigo_solicitud == codigo_solicitud
                                      && x.codigo_informe == codigo_informe
                                      && x.ci == ci
                                      && x.estado == "Borrado");

            if (entity == null)
                return null;

            entity.estado = "Activo";
            await _context.SaveChangesAsync();

            return (entity.toRespuestaInformeDTO());
        }
    }
}