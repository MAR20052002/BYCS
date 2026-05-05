using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class CorreoMapeador
    {
        public static CorreoDTO toCorreoDTO(this Correo Correo)
        {
            return new CorreoDTO()
            {
                correo = Correo.correo
            };
        }
    }
}
