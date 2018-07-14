using fundooNotesAngular5.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserRegistrationApp.DAL.Models;

namespace UserRegistrationApp.DAL.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }
        public  DbSet<tblLabel> tblLabels { get; set; }
        public DbSet<tblNotes> tblNotes{ get; set; }
        public DbSet<tblCollborator> tblCollaborator { get; set; }
        public DbSet<tblUserCollaborate> tblUserCollaborates { get; set; }
        public DbSet<tblUserLabel> tblUserLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
