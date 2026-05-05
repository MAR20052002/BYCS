using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IRespuestaRepositorio
    {
        Task<RespuestaDTO> GetRespuesta(string codigo_solicitud, string ci);
        Task<List<RespuestaDTO>> GetRespuesta();
        Task<List<RespuestaDTO>> GetRespuestaBorrados();
        Task<RespuestaDTO> PostRespuesta(string codigo_solicitud, string ci, bool aprobado);
        Task<RespuestaDTO> PutRespuesta(string codigo_solicitud, string ci, bool aprobado);
        Task<RespuestaDTO> DeleteRespuesta(string codigo_solicitud, string ci);
        Task<RespuestaDTO?> HabilitarRespuesta(string codigo_solicitud, string ci);
    }
}
