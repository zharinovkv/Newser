using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newser.Models
{
    public class Node
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        [StringLength(maximumLength: 20, MinimumLength = 7)]
        [Required]
        public string Title { get; set; }
               

        [Display(Name = "Содержимое")]
        [StringLength(maximumLength: 2000, MinimumLength = 7)]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Body { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Теги")]
        [DataType(DataType.Text)]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public int ParametersId { get; set; }
        public virtual AdditionalParameters Parameters { get; set; }

        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
    
    public class CustomServerValidation
    {
        public static string ValidResult(string text)
        {

            string str = text;
            char[] ar = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                ar[i] = str[i];
            }

            for (int i = 1; i < ar.Length; i++)
            {
                if (Char.IsUpper(ar[i]))
                {
                    return "InValid";
                }                
            }
            return "Valid";
        }
    }

    public class NodeWithCommentInfo
    {
        public Node Node { get; set; }

        public int CommentCount { get; set; }

        public DateTime? LastCommentDate { get; set; }
    }

    public class NodeWithNewComment
    {
        public Node Node { get; set; }

        public Comment NewComment { get; set; }

        public ApplicationUser User { get; set; }


    }
}
