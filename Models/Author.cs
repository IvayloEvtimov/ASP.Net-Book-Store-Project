using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Written_By> Written_Books { get; set; }
    }
}
