using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IItemRepositorio
    {
        Task<ItemDTO> GetItem(string codigo);
        Task<List<ItemDTO>> GetItem();
        Task<List<ItemDTO>> GetItemBorrados();
        Task<ItemDTO> PostItem(string codigo, string nombre, string descripcion);
        Task<ItemDTO> PutItem(string codigo, string nombre, string descripcion);
        Task<ItemDTO> DeleteItem(string codigo);
        Task<ItemDTO?> HabilitarItem(string codigo);
    }
}
