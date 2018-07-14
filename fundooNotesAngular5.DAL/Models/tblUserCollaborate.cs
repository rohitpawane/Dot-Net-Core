using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace fundooNotesAngular5.DAL.Models
{
   public class tblUserCollaborate
    {
        public int Id { get; set; }
        public string userid  { get; set; }
        [ForeignKey("collaborator")]
        public int collaboratorId { get; set; }

        public tblCollborator collaborator { get; set; }
    }
}
