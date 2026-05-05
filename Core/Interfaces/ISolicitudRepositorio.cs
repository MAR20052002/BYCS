using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface ISolicitudRepositorio
    {
        Task<SolicitudDTO> GetEmergencia(string codigo);
        Task<List<SolicitudDTO>> GetEmergencia();
        Task<List<SolicitudDTO>> GetEmergenciaBorrados();
        Task<SolicitudDTO> PostEmergencia(string codigo, string ci, string descripcion);
        Task<SolicitudDTO> PutEmergencia(string codigo, string ci, string descripcion);
        Task<SolicitudDTO> DeleteEmergencia(string codigo);
        Task<SolicitudDTO> HabilitarEmergencia(string codigo);
    }
}
