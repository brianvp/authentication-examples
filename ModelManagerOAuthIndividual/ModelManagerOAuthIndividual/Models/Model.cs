namespace ModelManagerOAuthIndividual.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("product.Model")]
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            PartNumbers = new HashSet<PartNumber>();
        }

        public int ModelId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(25)]
        public string ManufacturerCode { get; set; }

        public int? CategoryId { get; set; }

        public string Description { get; set; }

        public string Features { get; set; }

        public int? StatusId { get; set; }

        public int? ManufacturerId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ListPrice { get; set; }

        public string ImageCollection { get; set; }

        public string CategoryCustomData { get; set; }

        public string ManufacturerCustomData { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateModified { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public virtual Manufacturer Manufacturer { get; set; }

        [JsonIgnore]
        public virtual Status Status { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartNumber> PartNumbers { get; set; }
    }
}
