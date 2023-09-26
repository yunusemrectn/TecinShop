﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        [Display(Name = "Aciklama")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
