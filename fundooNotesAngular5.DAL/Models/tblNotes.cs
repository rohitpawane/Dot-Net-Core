using System;
using System.Collections.Generic;
using System.Text;

namespace fundooNotesAngular5.DAL.Models
{
    public class tblNotes
    {
        public int Id { get; set; }
        public string UserId{ get; set; }
        public string  Title { get; set; }
        public string  Content { get; set; }
        public string  ColorCode { get; set; }
        public string  Image { get; set; }
        public int isPin { get; set; }
        public int isTrash { get; set; }
        public int isArchive { get; set; }
        public string  Reminder { get; set; }
        public string  Labels { get; set; }
        public string Collaborators { get; set; }
    }
}
