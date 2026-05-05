using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo), IsUnique = true)]
    public class Item
    {
        [Key]
        public int id_item { get; set; }
        public string codigo { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string estado { get; set; } = "Activo";
        public ICollection<ItemStock>? StockItems { get; set; }
    }
}
