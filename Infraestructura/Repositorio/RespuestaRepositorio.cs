using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class RespuestaRepositorio : IRespuestaRepositorio
    {
        private readonly BYCS_DBContext _context;

        public RespuestaRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<RespuestaDTO?> GetRespuesta(string codigo_solicitud, string ci)
        {
            return await _context.Respuestas
                .AsNoTracking()
                .Where(p => p.codigo_solicitud == codigo_solicitud && p.ci == ci && p.estado != "Borrado")
                .Select(p => p.toRespuestaDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaDTO>> GetRespuesta()
        {
            return await _context.Respuestas
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toRespuestaDTO())
                .ToListAsync();
        }

        public async Task<List<RespuestaDTO>> GetRespuestaBorrados()
        {
            return await _context.Respuestas
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toRespuestaDTO())
                .ToListAsync();
        }

        public async Task<RespuestaDTO> PostRespuesta(string codigo_solicitud, string ci, bool aprobado)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci))
                return null;
            Usuario? U = await _context.Usuarios.FirstOrDefaultAsync(p => p.ci == ci && p.estado != "Borrado");
            if (U == null)
                return null;
            Solicitud? S = await _context.Solicitudes.FirstOrDefaultAsync(p => p.codigo == codigo_solicitud && p.estado != "Borrado");
            if (S == null)
                return null;
            Respuesta Respuesta = new Respuesta
            {
                id_solicitud = S.id_solicitud,
                id_usuario = U.id_usuario,
                codigo_solicitud = S.codigo,
                ci = U.ci,
                aprobado = aprobado,
                fecha = DateOnly.FromDateTime(DateTime.Today),
                estado = "Activo"
            };
            _context.Respuestas.Add(Respuesta);
            await _context.SaveChangesAsync();
            return Respuesta.toRespuestaDTO();
        }

        public async Task<RespuestaDTO?> PutRespuesta(string codigo_solicitud, string ci, bool aprobado)
        {
            if (string.IsNullOrWhiteSpace(codigo_solicitud) || string.IsNullOrWhiteSpace(ci))
                return null;
            Respuesta? Respuesta = await _context.Respuestas.FirstOrDefaultAsync(p => p.codigo_solicitud == codigo_solicitud && p.ci == ci && p.estado != "Borrado");
            if (Respuesta == null)
                return null;
            Usuario? U = await _context.Usuarios.FirstOrDefaultAsync(p => p.ci == ci && p.estado != "Borrado");
            if (U == null)
                return null;
            Solicitud? S = await _context.Solicitudes.FirstOrDefaultAsync(p => p.codigo == codigo_solicitud && p.estado != "Borrado");
            if (S == null)
                return null;

            Respuesta.id_solicitud = S.id_solicitud;
            Respuesta.id_usuario = U.id_usuario;
            Respuesta.codigo_solicitud = S.codigo;
            Respuesta.ci = U.ci;
            Respuesta.aprobado = aprobado;
            await _context.SaveChangesAsync();
            return Respuesta.toRespuestaDTO();
        }

        public async Task<RespuestaDTO?> DeleteRespuesta( string codigo_solicitud, string ci)
        {
            Respuesta? Respuesta = await _context.Respuestas.FirstOrDefaultAsync(p => p.codigo_solicitud == codigo_solicitud && p.ci == ci && p.estado == "Activo");
            if (Respuesta == null) return null;
            Respuesta.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Respuesta.toRespuestaDTO();
        }

        public async Task<RespuestaDTO?> HabilitarRespuesta(string codigo_solicitud, string ci)
        {
            Respuesta? Respuesta = await _context.Respuestas.FirstOrDefaultAsync(p => p.codigo_solicitud == codigo_solicitud && p.ci == ci && p.estado == "Borrado");
            if (Respuesta == null) return null;
            Respuesta.estado = "Activo";
            await _context.SaveChangesAsync();
            return Respuesta.toRespuestaDTO();
        }
    }
}
