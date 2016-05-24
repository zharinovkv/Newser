using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newser.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Display(Name = "Содержимое комментария")]
        [StringLength(maximumLength: 2000, MinimumLength = 7)]
        [DataType(DataType.MultilineText)]
        
        [Required]
        public string Body { get; set; }


        [Display(Name = "Дата")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public int NodeId { get; set; }
        public virtual Node Node { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class CommentsWithUser
    {
        public ICollection<Comment> Comments { get; set; }
        public ApplicationUser User { get; set; }
    }
}
