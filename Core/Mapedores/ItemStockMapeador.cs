using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class ItemStockMapeador
    {
        public static ItemStockDTO toItemStockDTO(this ItemStock ItemStock)
        {
            return new ItemStockDTO()
            {
                codigo_item = ItemStock.codigo_item,
                codigo_inventario = ItemStock.codigo_inventario,
                cantidad = ItemStock.cantidad
            };
        }
    }
}
