using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class UsuarioCorreoMapeador
    {
        public static UsuarioCorreoDTO toUsuarioCorreoDTO(this UsuarioCorreo usuariocorreo)
        {
            return new UsuarioCorreoDTO()
            {
                ci= usuariocorreo.ci,
                correo = usuariocorreo.correo,
                fecha_inicio = usuariocorreo.fecha_inicio,
                fecha_fin = usuariocorreo.fecha_fin
            };
        }
    }
}
