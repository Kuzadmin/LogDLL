using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace LogDLL
{
    [Serializable]
    public enum TypeOperation
    {
        [XmlEnum(Name = "Insert")]
        Insert = 1,
        [XmlEnum(Name = "Update")]
        Update = 2,
        [XmlEnum(Name = "Remove")]
        Remove = 3
    };

    [Serializable]
    public class SQL
    {
        [Required]
        public string tableName { get; set; }
        [Required]
        public TypeOperation typeOperation { get; set; }
        public string UniqueID { get; set; }
        public string Request { get; set; }
    }

    [Serializable]
    public class SQLSimple
    {
        [Required]
        public string tableName { get; set; }
        [Required]
        public string typeOperation { get; set; }
        public string UniqueID { get; set; }
        public string Request { get; set; }
    }
}
