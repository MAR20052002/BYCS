using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class InventarioRepositorio : IInventarioRepositorio
    {
        private readonly BYCS_DBContext _context;

        public InventarioRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<InventarioDTO?> GetInventario(string codigo)
        {
            return await _context.Inventarios
                .AsNoTracking()
                .Where(p => p.codigo == codigo && p.estado != "Borrado")
                .Select(p => p.toInventarioDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<InventarioDTO>> GetInventario()
        {
            return await _context.Inventarios
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toInventarioDTO())
                .ToListAsync();
        }

        public async Task<List<InventarioDTO>> GetInventarioBorrados()
        {
            return await _context.Inventarios
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toInventarioDTO())
                .ToListAsync();
        }

        public async Task<InventarioDTO> PostInventario(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            var Inventario = new Inventario
            {
                codigo = codigo,
                titulo = titulo,
                descripcion = descripcion,
                estado = "Activo"
            };
            _context.Inventarios.Add(Inventario);
            await _context.SaveChangesAsync();
            return Inventario.toInventarioDTO();
        }

        public async Task<InventarioDTO?> PutInventario(string codigo, string titulo, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            var Inventario = await _context.Inventarios.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado != "Borrado");
            if (Inventario == null)
                return null;
            Inventario.titulo = titulo;
            Inventario.descripcion = descripcion;
            await _context.SaveChangesAsync();
            return Inventario.toInventarioDTO();
        }

        public async Task<InventarioDTO?> DeleteInventario(string codigo)
        {
            var Inventario = await _context.Inventarios.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Activo");
            if (Inventario == null) return null;
            Inventario.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Inventario.toInventarioDTO();
        }

        public async Task<InventarioDTO?> HabilitarInventario(string codigo)
        {
            var Inventario = await _context.Inventarios.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Borrado");
            if (Inventario == null) return null;
            Inventario.estado = "Activo";
            await _context.SaveChangesAsync();
            return Inventario.toInventarioDTO();
        }
    }
}
