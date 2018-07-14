using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fundooNotesAngular5.DAL.Models
{
    public class tblCollborator
    {
        [Key]
        public int Id { get; set; }
        public string  Userid { get; set; }
        public int noteid { get; set; }
    }
}
