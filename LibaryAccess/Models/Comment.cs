using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAccess.Models
{
    public class Comment
    {
        public Comment()
        {

        }
        public Comment(long id, long errandId, string description, DateTime created)
        {
            Id = id;
            ErrandId = errandId;
            Description = description;
            Created = created;
        }

        public long Id { get; set; }
        public long ErrandId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
