using BYCS.Core.DTOs;
using BYCS.Core.Models;
namespace BYCS.Core.Mapedores
{
    public static class InventarioMapeador
    {
        public static InventarioDTO toInventarioDTO(this Inventario Inventario)
        {
            return new InventarioDTO()
            {
                codigo = Inventario.codigo,
                titulo = Inventario.titulo,
                descripcion = Inventario.descripcion
            };
        }
    }
}
