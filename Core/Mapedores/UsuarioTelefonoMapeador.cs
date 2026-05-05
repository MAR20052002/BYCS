using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class UsuarioTelefonoMapeador
    {
        public static UsuarioTelefonoDTO toUsuarioTelefonoDTO(this UsuarioTelefono usuariotelf)
        {
            return new UsuarioTelefonoDTO()
            {
                ci= usuariotelf.ci,
                telf = usuariotelf.telf,
                fecha_inicio = usuariotelf.fecha_inicio,
                fecha_fin = usuariotelf.fecha_fin
            };
        }
    }
}
