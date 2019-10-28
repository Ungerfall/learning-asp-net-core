using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NorthwindStore.Data.Models.Validation;

namespace NorthwindStore.Data.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }

        [Required]
        [MaxLength(80)]
        [AsciiCharacters]
        public string ProductName { get; set; }

        [Required]
        public int? SupplierId { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [MaxLength(40)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "decimal(19,4)")]
        public decimal? UnitPrice { get; set; }

        [Column(TypeName = "smallint")]
        public short? UnitsInStock { get; set; }

        [Range(short.MinValue, short.MaxValue)]
        public short? UnitsOnOrder { get; set; }

        [Column(TypeName = "smallint")]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
