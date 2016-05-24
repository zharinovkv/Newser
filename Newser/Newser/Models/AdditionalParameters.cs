using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Newser.Models
{
    public class AdditionalParameters
    {

        public int Id { get; set; }

        public string String1 { get; set; }
        public string String2 { get; set; }
        public string String3 { get; set; }

        //[Key, ForeignKey("Node")]
        public int NodeId { get; set; }
        public virtual Node Node { get; set; }
    }
}