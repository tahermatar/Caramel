using Caramel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.DbModel.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public int CreatedId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }
        public virtual User User { get; set; }
    }
}
