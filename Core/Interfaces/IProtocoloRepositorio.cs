using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IProtocoloRepositorio
    {
        Task<ProtocoloDTO> GetProtocolo(string codigo);
        Task<List<ProtocoloDTO>> GetProtocolo();
        Task<List<ProtocoloDTO>> GetProtocoloBorrados();
        Task<ProtocoloDTO> PostProtocolo(string codigo, string titulo, string descripcion);
        Task<ProtocoloDTO> PutProtocolo(string codigo, string titulo, string descripcion);
        Task<ProtocoloDTO> DeleteProtocolo(string codigo);
        Task<ProtocoloDTO?> HabilitarProtocolo(string codigo);
    }
}
