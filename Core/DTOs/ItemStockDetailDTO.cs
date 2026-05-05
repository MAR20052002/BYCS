namespace BYCS.Core.DTOs
{
    public class ItemStockDetailDTO
    {
        public string titulo_inventario { get; set; } = null!;
        public string nombre_item { get; set; } = null!;
        public int cantidad { get; set; }
    }
}
