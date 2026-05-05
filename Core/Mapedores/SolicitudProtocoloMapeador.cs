using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class SolicitudProtocoloMapeador
    {
        public static SolicitudProtocoloDTO toSolicitudProtocoloDTO(this SolicitudProtocolo solicitudprotocolo)
        {
            return new SolicitudProtocoloDTO()
            {
                codigo_protocolo = solicitudprotocolo.codigo_protocolo,
                codigo_solicitud = solicitudprotocolo.codigo_solicitud,
                fecha_inicio = solicitudprotocolo.fecha_inicio,
                fecha_fin = solicitudprotocolo.fecha_fin
            };
        }
    }
}
