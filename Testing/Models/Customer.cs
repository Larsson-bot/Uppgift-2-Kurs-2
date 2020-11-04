using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Testing.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Matters = new HashSet<Matter>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [InverseProperty(nameof(Matter.Customer))]
        public virtual ICollection<Matter> Matters { get; set; }
    }
}
