using LibaryAccess.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAccess.Models
{
    public class Errand
    {
        public Errand()
        {

        }
        public Errand(long id, long customerId, string catagory, string status, string description, DateTime created)
        {
            Id = id;
            CustomerId = customerId;
            Catagory = catagory;
            Status = status;
            Description = description;
            Created = created;

        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Catagory { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual Customer Customer { get; set; }
    }

}
