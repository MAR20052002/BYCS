using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class UsuarioMapeador
    {
        public static UsuarioDTO toUsuarioDTO(this Usuario usuario)
        {
            return new UsuarioDTO()
            {
                ci = usuario.ci,
                nombre = usuario.nombre,
                apellido_p = usuario.apellido_p,
                apellido_m = usuario.apellido_m
            };
        }
    }
}
