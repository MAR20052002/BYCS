using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo_item), nameof(codigo_inventario), IsUnique = true)]
    public class ItemStock
    {
        [Key]
        public int id_item { get; set; }
        [Key]
        public int id_inventario { get; set; }
        public string codigo_item { get; set; } = null!;
        public string codigo_inventario { get; set; } = null!;
        public int cantidad { get; set; }
        public string estado { get; set; } = "Activo";
        [ForeignKey("id_item")]
        [JsonIgnore]
        public Item Item { get; set; } = null!;
        [ForeignKey("id_inventario")]
        [JsonIgnore]
        public Inventario Inventario { get; set; } = null!;
    }
}
