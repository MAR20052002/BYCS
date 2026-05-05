using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class SolicitudMapeador
    {
        public static SolicitudDTO toSolicitudDTO(this Solicitud solicitud)
        {
            return new SolicitudDTO()
            {
                codigo = solicitud.codigo,
                ci = solicitud.ci,
                descripcion = solicitud.descripcion
            };
        }
    }
}
