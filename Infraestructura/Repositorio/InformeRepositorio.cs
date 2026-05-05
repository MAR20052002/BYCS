using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class InformeRepositorio : IInformeRepositorio
    {
        private readonly BYCS_DBContext _context;

        public InformeRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<InformeDTO?> GetInforme(string codigo_informe)
        {
            return await _context.Informes
                .AsNoTracking()
                .Where(p => p.codigo_informe == codigo_informe && p.estado != "Borrado")
                .Select(p => p.toInformeDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<InformeDTO>> GetInforme()
        {
            return await _context.Informes
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toInformeDTO())
                .ToListAsync();
        }

        public async Task<List<InformeDTO>> GetInformeBorrados()
        {
            return await _context.Informes
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toInformeDTO())
                .ToListAsync();
        }

        public async Task<InformeDTO> PostInforme(string codigo_informe, string titulo, string descripcion, string url)
        {
            if (string.IsNullOrWhiteSpace(codigo_informe) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(url))
                return null;
            Informe informe = new Informe
            {
                codigo_informe = codigo_informe,
                titulo = titulo,
                descripcion = descripcion,
                url = url,
                estado = "Activo"
            };
            _context.Informes.Add(informe);
            await _context.SaveChangesAsync();
            return informe.toInformeDTO();
        }

        public async Task<InformeDTO?> PutInforme(string codigo_informe, string titulo, string descripcion, string url)
        {
            if (string.IsNullOrWhiteSpace(codigo_informe) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(url))
                return null;
            Informe? informe = await _context.Informes.FirstOrDefaultAsync(p => p.codigo_informe == codigo_informe && p.estado != "Borrado");
            if (informe == null)
                return null;
            informe.titulo = titulo;
            informe.descripcion = descripcion;
            informe.url = url;
            await _context.SaveChangesAsync();
            return informe.toInformeDTO();
        }

        public async Task<InformeDTO?> DeleteInforme(string codigo_informe)
        {
            var Informe = await _context.Informes.FirstOrDefaultAsync(p => p.codigo_informe == codigo_informe && p.estado == "Activo");
            if (Informe == null) return null;
            Informe.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Informe.toInformeDTO();
        }

        public async Task<InformeDTO?> HabilitarInforme(string codigo_informe)
        {
            var Informe = await _context.Informes.FirstOrDefaultAsync(p => p.codigo_informe == codigo_informe && p.estado == "Borrado");
            if (Informe == null) return null;
            Informe.estado = "Activo";
            await _context.SaveChangesAsync();
            return Informe.toInformeDTO();
        }
    }
}
