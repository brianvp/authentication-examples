namespace ModelManagerOAuthIndividual.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product.PartNumber")]
    public partial class PartNumber
    {
        public int PartNumberId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string InvoiceDescription { get; set; }

        public int? ModelId { get; set; }

        public int? StatusId { get; set; }

        [StringLength(25)]
        public string InventoryPartNumber { get; set; }

        [StringLength(25)]
        public string ManufacturerPartNumber { get; set; }

        public string CategoryCustomData { get; set; }

        public string ManufacturerCustomData { get; set; }

        [Column(TypeName = "money")]
        public decimal? ListPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        [StringLength(12)]
        public string UPC { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateModified { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }

        public virtual Model Model { get; set; }

        public virtual Status Status { get; set; }
    }
}
