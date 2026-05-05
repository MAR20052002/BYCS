using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class InformeMapeador
    {
        public static InformeDTO toInformeDTO(this Informe informe)
        {
            return new InformeDTO()
            {
                codigo_informe = informe.codigo_informe,
                titulo = informe.titulo,
                descripcion = informe.descripcion,
                url = informe.url
            };
        }
    }
}
