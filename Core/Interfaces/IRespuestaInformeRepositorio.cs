using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IRespuestaInformeRepositorio
    {
        Task<List<RespuestaInformeDTO>> GetRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe);
        Task<List<RespuestaInformeDTO>> GetRespuestaInforme();
        Task<List<RespuestaInformeDTO>> GetRespuestaInformeBorrados();
        Task<RespuestaInformeDTO> PostRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe);
        Task<RespuestaInformeDTO> PutRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe);
        Task<RespuestaInformeDTO> DeleteRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe);
        Task<RespuestaInformeDTO?> HabilitarRespuestaInforme(string codigo_solicitud, string ci, string codigo_informe);
    }
}
