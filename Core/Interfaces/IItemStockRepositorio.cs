using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IItemStockRepositorio
    {
        Task<ItemStockDTO?> GetItemStock(string codigo_item, string codigo_inventario);
        Task<List<ItemStockDTO>> GetItemStock();
        Task<List<ItemStockDetailDTO>> GetItemStockAlerta();
        Task<List<ItemStockDetailDTO>> GetItemStockAlertaVacios();
        Task<List<ItemStockDTO>> GetItemStockBorrados();
        Task<ItemStockDTO> PostItemStock(string codigo_item, string codigo_inventario, int cantidad);
        Task<ItemStockDTO> PutItemStock(string codigo_item, string codigo_inventario, int cantidad);
        Task<ItemStockDTO> DeleteItemStock(string codigo_item, string codigo_inventario);
        Task<ItemStockDTO?> HabilitarItemStock(string codigo_item, string codigo_inventario);
    }
}
