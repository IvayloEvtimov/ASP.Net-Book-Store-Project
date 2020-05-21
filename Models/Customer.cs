﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name="Telephone Number")]
        public string TelephoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}