using System;
using System.ComponentModel.DataAnnotations;

namespace Newser.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Имя пользователя")]
        [StringLength(maximumLength: 20, MinimumLength = 7)]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(maximumLength: 20, MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(maximumLength: 100)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreateDate { get; set; }
    }
}
