using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class ItemMapeador
    {
        public static ItemDTO toItemDTO(this Item item)
        {
            return new ItemDTO()
            {
                codigo = item.codigo,
                nombre = item.nombre,
                descripcion = item.descripcion
            };
        }
    }
}
