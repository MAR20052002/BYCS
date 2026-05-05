using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IInventarioRepositorio
    {
        Task<InventarioDTO> GetInventario(string codigo);
        Task<List<InventarioDTO>> GetInventario();
        Task<List<InventarioDTO>> GetInventarioBorrados();
        Task<InventarioDTO> PostInventario(string codigo, string titulo, string descripcion);
        Task<InventarioDTO> PutInventario(string codigo, string titulo, string descripcion);
        Task<InventarioDTO> DeleteInventario(string codigo);
        Task<InventarioDTO?> HabilitarInventario(string codigo);
    }
}
