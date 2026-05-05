using Microsoft.EntityFrameworkCore;
using BYCS.Core.DTOs;
using BYCS.Core.Interfaces;
using BYCS.Core.Models;
using BYCS.Infraestructura.Data;
using BYCS.Core.Mapedores;

namespace BYCS.Infraestructura.Repositorio
{
    public class ItemStockRepositorio : IItemStockRepositorio
    {
        private readonly BYCS_DBContext _context;

        public ItemStockRepositorio(BYCS_DBContext context)
        {
            _context = context;
        }

        public async Task<ItemStockDTO?> GetItemStock(string codigo_item, string codigo_inventario)
        {
            return await _context.ItemStocks
                .AsNoTracking()
                .Where(x => x.codigo_item == codigo_item
                                      && x.codigo_inventario == codigo_inventario
                                      && x.estado != "Borrado")
                .Select(x => (x.toItemStockDTO()))
                .FirstOrDefaultAsync();

        }

        public async Task<List<ItemStockDTO>> GetItemStock()
        {
            return await _context.ItemStocks
                .AsNoTracking()
                .Where(x => x.estado != "Borrado")
                .Select(x => (x.toItemStockDTO()))
                .ToListAsync();
        }

        public async Task<List<ItemStockDetailDTO>> GetItemStockAlerta()
        {
            return await _context.ItemStocks
                .AsNoTracking()
                .Where(x => x.estado != "Borrado" && x.cantidad <= 10)
                .Select(x => new ItemStockDetailDTO
                {
                    titulo_inventario = x.Inventario.titulo,
                    nombre_item = x.Item.nombre,
                    cantidad = x.cantidad
                })
                .ToListAsync();
        }

        public async Task<List<ItemStockDetailDTO>> GetItemStockAlertaVacios()
        {
            return await _context.ItemStocks
                .AsNoTracking()
                .Where(x => x.estado != "Borrado" && x.cantidad == 0)
                .Select(x => new ItemStockDetailDTO
                {
                    titulo_inventario = x.Inventario.titulo,
                    nombre_item = x.Item.nombre,
                    cantidad = x.cantidad
                })
                .ToListAsync();
        }

        public async Task<List<ItemStockDTO>> GetItemStockBorrados()
        {
            return await _context.ItemStocks
                .AsNoTracking()
                .Where(x => x.estado == "Borrado")
                .Select(x => (x.toItemStockDTO()))
                .ToListAsync();
        }

        public async Task<ItemStockDTO?> PostItemStock(string codigo_item, string codigo_inventario, int cantidad)
        {
            Item? item = await _context.Items
                .FirstOrDefaultAsync(u => u.codigo == codigo_item && u.estado == "Activo");

            Inventario? inventario = await _context.Inventarios
                .FirstOrDefaultAsync(t => t.codigo == codigo_inventario && t.estado == "Activo");

            if (item == null || inventario == null)
                return null;
            Boolean existe = await _context.ItemStocks.AnyAsync(x =>
                 x.codigo_item == codigo_item &&
                 x.codigo_inventario == codigo_inventario
                 );

            if (existe)
                return null;
            ItemStock entity = new ItemStock
            {
                id_item = item.id_item,
                id_inventario = inventario.id_inventario,
                codigo_item = codigo_item,
                codigo_inventario = codigo_inventario,
                cantidad = cantidad,
                estado = "Activo"
            };

            _context.ItemStocks.Add(entity);
            await _context.SaveChangesAsync();

            return (entity.toItemStockDTO());
        }

        public async Task<ItemStockDTO?> PutItemStock(string codigo_item, string codigo_inventario, int cantidad)
        {
            ItemStock? entity = await _context.ItemStocks
                            .FirstOrDefaultAsync(x =>
                                x.codigo_item == codigo_item &&
                                x.codigo_inventario == codigo_inventario &&
                                x.estado != "Borrado");
            if (entity == null)
                return null;

            Item? u = await _context.Items.FirstOrDefaultAsync(x => x.codigo == codigo_item);
            if (u == null)
                return null;
            Inventario? t = await _context.Inventarios.FirstOrDefaultAsync(x => x.codigo == codigo_inventario);
            if (t == null)
                return null;
            entity.cantidad = cantidad;
            await _context.SaveChangesAsync();
            return (entity.toItemStockDTO());
        }

        public async Task<ItemStockDTO?> DeleteItemStock(string codigo_item, string codigo_inventario)
        {
            var entity = await _context.ItemStocks
                .FirstOrDefaultAsync(x =>
                    x.codigo_item == codigo_item &&
                    x.codigo_inventario == codigo_inventario &&
                    x.estado == "Activo");

            if (entity == null)
                return null;

            entity.estado = "Borrado";
            await _context.SaveChangesAsync();

            return (entity.toItemStockDTO());
        }

        public async Task<ItemStockDTO?> HabilitarItemStock(string codigo_item, string codigo_inventario)
        {
            var entity = await _context.ItemStocks
                .FirstOrDefaultAsync(x =>
                    x.codigo_item == codigo_item &&
                    x.codigo_inventario == codigo_inventario &&
                    x.estado == "Borrado");

            if (entity == null)
                return null;

            entity.estado = "Activo";
            await _context.SaveChangesAsync();

            return (entity.toItemStockDTO());
        }
    }
}