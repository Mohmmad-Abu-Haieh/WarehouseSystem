﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersModule.Entity;

namespace Entity.Entity
{
    public class Country : BaseEntity<Guid>
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    }
}
