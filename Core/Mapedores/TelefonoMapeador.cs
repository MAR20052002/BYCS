using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class TelefonoMapeador
    {
        public static TelefonoDTO toTelefonoDTO(this Telefono telefono)
        {
            return new TelefonoDTO()
            {
                telf = telefono.telf
            };
        }
    }
}
