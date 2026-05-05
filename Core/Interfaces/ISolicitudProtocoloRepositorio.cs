using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface ISolicitudProtocoloRepositorio
    {
        Task<List<SolicitudProtocoloDTO>> GetSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud);
        Task<List<SolicitudProtocoloDTO>> GetSolicitudProtocolo();
        Task<List<SolicitudProtocoloDTO>> GetSolicitudProtocoloBorrados();
        Task<SolicitudProtocoloDTO> PostSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud, string fecha_inicio, string fecha_fin);
        Task<SolicitudProtocoloDTO> PutSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud, string fecha_inicio, string fecha_fin);
        Task<SolicitudProtocoloDTO> DeleteSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud);
        Task<SolicitudProtocoloDTO?> HabilitarSolicitudProtocolo(string codigo_protocolo, string codigo_solicitud);
    }
}
