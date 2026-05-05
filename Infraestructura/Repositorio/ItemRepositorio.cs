using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Mapedores;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;

namespace BYCS.Infraestructura.Repositorio
{
    public class ItemRepositorio : IItemRepositorio
    {
        private readonly BYCS_DBContext _context;

        public ItemRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<ItemDTO?> GetItem(string codigo)
        {
            return await _context.Items
                .AsNoTracking()
                .Where(p => p.codigo == codigo && p.estado != "Borrado")
                .Select(p => p.toItemDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<ItemDTO>> GetItem()
        {
            return await _context.Items
                .AsNoTracking()
                .Where(p => p.estado != "Borrado")
                .Select(p => p.toItemDTO())
                .ToListAsync();
        }

        public async Task<List<ItemDTO>> GetItemBorrados()
        {
            return await _context.Items
                .AsNoTracking()
                .Where(p => p.estado == "Borrado")
                .Select(p => p.toItemDTO())
                .ToListAsync();
        }

        public async Task<ItemDTO> PostItem(string codigo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            var Item = new Item
            {
                codigo = codigo,
                nombre = nombre,
                descripcion = descripcion,
                estado = "Activo"
            };
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return Item.toItemDTO();
        }

        public async Task<ItemDTO?> PutItem(string codigo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                return null;
            var Item = await _context.Items.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado != "Borrado");
            if (Item == null)
                return null;
            Item.nombre = nombre;
            Item.descripcion = descripcion;
            await _context.SaveChangesAsync();
            return Item.toItemDTO();
        }

        public async Task<ItemDTO?> DeleteItem(string codigo)
        {
            var Item = await _context.Items.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Activo");
            if (Item == null) return null;
            Item.estado = "Borrado";
            await _context.SaveChangesAsync();
            return Item.toItemDTO();
        }

        public async Task<ItemDTO?> HabilitarItem(string codigo)
        {
            var Item = await _context.Items.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == "Borrado");
            if (Item == null) return null;
            Item.estado = "Activo";
            await _context.SaveChangesAsync();
            return Item.toItemDTO();
        }
    }
}
