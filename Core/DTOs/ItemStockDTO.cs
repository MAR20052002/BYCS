namespace BYCS.Core.DTOs
{
    public class ItemStockDTO
    {
        public string codigo_item { get; set; } = null!;
        public string codigo_inventario { get; set; } = null!;
        public int cantidad { get; set; }
    }
}
