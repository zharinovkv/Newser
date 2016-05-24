using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newser.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Ext { get; set; }

        public string Name
        {
            get
            {
                return $"(Id).(Ext)";
            }
        }

        public static string GetExt(string fileName)
        {
            return fileName.Substring(fileName.Length - 3, 3);
        }
    }
}