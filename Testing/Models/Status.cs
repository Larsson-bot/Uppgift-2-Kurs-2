using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Testing.Models
{
    [Table("Status")]
    public partial class Status
    {
        public Status()
        {
            Matters = new HashSet<Matter>();
        }

        [Key]
        public int StatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string StatusDescription { get; set; }

        [InverseProperty(nameof(Matter.Status))]
        public virtual ICollection<Matter> Matters { get; set; }
    }
}
