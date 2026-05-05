using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class ProtocoloMapeador
    {
        public static ProtocoloDTO toProtocoloDTO(this Protocolo protocolo)
        {
            return new ProtocoloDTO()
            {
                codigo = protocolo.codigo,
                titulo = protocolo.titulo,
                descripcion = protocolo.descripcion
            };
        }
    }
}
