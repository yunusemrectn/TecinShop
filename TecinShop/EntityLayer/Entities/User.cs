using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        public string Surname { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez.")]
        //[Display(Name = "Email")]
        //[StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        //[EmailAddress(ErrorMessage = "E-mail formati seklinde giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        [Display(Name = "KullaniciAdi")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez.")]
        //[Display(Name = "Sifre1")]
        //[StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Rol")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalidir.")]
        public string Role { get; set; }

    }
}
