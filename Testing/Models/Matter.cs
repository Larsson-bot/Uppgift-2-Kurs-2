using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Testing.Models
{
    [Table("Matter")]
    public partial class Matter
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [StringLength(30)]
        public string MatterCatagory { get; set; }
        public int StatusId { get; set; }
        [Required]
        public string MatterDescription { get; set; }
        public DateTime Time { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Matters")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(StatusId))]
        [InverseProperty("Matters")]
        public virtual Status Status { get; set; }
    }
}
