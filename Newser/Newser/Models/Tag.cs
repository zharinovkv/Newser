using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newser.Models
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public virtual ICollection<Node> Nodes { get; set; } = new List<Node>();


    }
}