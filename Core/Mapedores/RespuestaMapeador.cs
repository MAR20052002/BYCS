using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class RespuestaMapeador
    {
        public static RespuestaDTO toRespuestaDTO(this Respuesta respuesta)
        {
            return new RespuestaDTO()
            {
                codigo_solicitud = respuesta.codigo_solicitud,
                ci = respuesta.ci,
                aprobado = respuesta.aprobado,
                fecha = respuesta.fecha
            };
        }
    }
}
