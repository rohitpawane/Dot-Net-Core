using fundooNotesAngular5.DAL.Models;
using fundooNotesAngular5.Repository;
using System;
using UserRegistrationApp.DAL.Data;

namespace fundooNotesAngular5.Service
{
    public class NotesService : NotesRepository
    {
        private readonly ApplicationDbContext _context;

        public NotesService()
        {
           
        }
        public NotesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public int PutNotes(tblNotes model)
        {
            int result=0;
            try
            {
                _context.tblNotes.Add(model);
                result = _context.SaveChanges(); 
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return result;
        }
    }
}
