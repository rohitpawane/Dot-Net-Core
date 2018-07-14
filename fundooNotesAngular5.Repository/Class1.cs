using fundooNotesAngular5.DAL.Models;
using System;
using UserRegistrationApp.DAL.Data;

namespace fundooNotesAngular5.Repository
{
    public interface NotesRepository
    {
         int PutNotes(tblNotes model);
    }
}
