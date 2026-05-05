using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class RespuestaInformeMapeador
    {
        public static RespuestaInformeDTO toRespuestaInformeDTO(this RespuestaInforme respuestainforme)
        {
            return new RespuestaInformeDTO()
            {
                codigo_informe = respuestainforme.codigo_informe,
                ci = respuestainforme.ci,
                codigo_solicitud = respuestainforme.codigo_solicitud,
                fecha = respuestainforme.fecha
            };
        }
    }
}
