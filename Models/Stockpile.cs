using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Stockpile
    {
        [Key]
        public int BookId { get; set; }

        public int Volume { get; set; }

        public Book Book { get; set; }
    }
}
