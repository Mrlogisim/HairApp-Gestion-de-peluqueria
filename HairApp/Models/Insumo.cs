using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairApp.Models
{
    [Table("Insumo")]
    public class Insumo
    {
        [Key]
        [Column("id_insumo")]
        public int IdInsumo { get; set; }

        [Required(ErrorMessage = "El nombre del insumo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria")]
        [StringLength(50, ErrorMessage = "La unidad de medida no puede exceder los 50 caracteres")]
        [Column("unidad_medida")]
        [Display(Name = "Unidad de Medida")]
        public string UnidadMedida { get; set; } = string.Empty;

        [Column("fecha_vencimiento")]
        [Display(Name = "Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser mayor o igual a 0")]
        [Column("stock_minimo")]
        [Display(Name = "Stock Mínimo")]
        public int StockMinimo { get; set; }

        [Column("fecha_ultima_reposicion")]
        [Display(Name = "Última Reposición")]
        [DataType(DataType.Date)]
        public DateTime? FechaUltimaReposicion { get; set; }

        // Propiedad calculada para verificar si está por vencerse
        [NotMapped]
        public bool EstaPorVencer
        {
            get
            {
                if (!FechaVencimiento.HasValue) return false;
                return FechaVencimiento.Value <= DateTime.Now.AddDays(30);
            }
        }

        // Propiedad calculada para verificar stock bajo
        [NotMapped]
        public bool TieneStockBajo
        {
            get => Cantidad <= StockMinimo;
        }

        // Relación muchos a muchos con Servicio
        public virtual ICollection<ServicioInsumo>? ServicioInsumo { get; set; }
    }
}
