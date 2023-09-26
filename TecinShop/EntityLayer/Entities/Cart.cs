using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        [Display(Name = "Urun")]
        public int ProductId { get; set; }
        public virtual Product  Product{ get; set; }

        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Fiyat")]
        public Decimal Price { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [Display(Name = "Resim")]
        public string Image { get; set; }

        [Display(Name = "Kullanici")]
        public int UserId { get; set; }
    }
}
