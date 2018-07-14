using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fundooNotesAngular5.DAL.Models;
using fundooNotesAngular5.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationApp.DAL.Data;
using UserRegistrationApp.DAL.Models;

namespace fundooNotesAngular5.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signinManager;
        public NotesController(ApplicationDbContext context)
        {
            _context = context;
            //_userManager = userManager;
            //_signinManager = signInManager;
        }
        public string AddLabel(tblLabel model)
        {
            int i = 0;
            try
            {
                _context.tblLabels.Add(model);
                i = _context.SaveChanges();

            }
            catch (Exception ex)
            {

                ex.ToString();
            }


            if (i > 0)
            {
                return "Label Added Successfully";
            }
            else
            {
                return "Something went Wrong";
            }
        }



        [HttpGet, AllowAnonymous]
        public List<tblLabel> GetLabel()
        {
            var list = new List<tblLabel>();
            //tblLabel data=_context.tblLabels.FirstOrDefault().toList

            var data = from a in _context.tblLabels
                       select a;
            foreach (tblLabel item in data)
            {
                list.Add(item);
            }
            return list;
        }


        public int PutNotes(tblNotes model)
        {

            int result = 0;
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
        [Authorize]
        public IActionResult GetNotes(string userid,int noteid,string localuserid)
        {
            IActionResult response = null;
            var collaboratelist = new List<tblCollborator>();
            var list = new List<tblNotes>();
            if (noteid > 0)
            {
                var data = from a in _context.tblNotes
                           where a.UserId == userid && a.Id==noteid
                           select a;
                foreach (tblNotes item in data)
                {
                    list.Add(item);
                }
                var localdata = from b in _context.tblNotes
                                where b.UserId == localuserid
                                select b;
                foreach (tblNotes item in localdata)
                {
                    list.Add(item);
                }
            }
            else
            {
                var data = from a in _context.tblNotes
                           where a.UserId == userid
                           select a;
                foreach (tblNotes item in data)
                {
                    list.Add(item);
                }
                var notesdata = from a in _context.tblNotes
                                where a.UserId == localuserid
                                select a;
                foreach (tblNotes item in notesdata)
                {
                    list.Add(item);
                }


                var collaborate = from b in _context.tblUserCollaborates
                                  where b.userid == userid
                                  select b;
                int collaborateid = 0;
                foreach (tblUserCollaborate item in collaborate)
                {
                    collaborateid = item.collaboratorId;
                }
                var collaboratedata = from c in _context.tblCollaborator
                                      where c.Id == collaborateid
                                      select c;

                foreach (tblCollborator item in collaboratedata)
                {
                    collaboratelist.Add(item);
                }

            }


            response = Ok(new { userdata = list, collaborator = collaboratelist });
            return response;

        }

        [HttpPost]
        public int UpdateNote([FromBody]tblNotes note)
        {
            int result = 0;
            try
            {

                //var id = from a in _context.tblNotes
                //         where a.Id == note.Id
                //         select a;
                tblNotes notes = _context.tblNotes.Where<tblNotes>(a => a.Id == note.Id).First();
                notes.isPin = note.isPin;
                notes.isArchive = note.isArchive;
                notes.isTrash = note.isTrash;
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return result;

        }
        [HttpGet]
        [AllowAnonymous]
        public List<tblNotes> GetLabeledNotes(string label)
        {
            var list = new List<tblNotes>();
            var labels = from a in _context.tblNotes
                         select a;
            foreach (tblNotes item in labels)
            {
                if (item.Labels != null)
                {
                    if (item.Labels.Contains(label))
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        [AllowAnonymous]
        public async Task<ApplicationUser> RegisterUser([FromBody]ApplicationUser user)
        {
            try
            {
                _context.Users.Add(user);
                int i = _context.SaveChanges();

                //var result =await  _userManager.CreateAsync(user);

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return user;
        }

        [HttpPost]
        public List<ApplicationUser> Login([FromBody] ApplicationUser model)
        {
            var list = new List<ApplicationUser>();

            var data = from a in _context.Users
                       where a.Email == model.Email
                       select a;
            foreach (ApplicationUser item in data)
            {
                list.Add(item);
            }
            return list;
        }


        [HttpPost]
        public IActionResult AddCollaborator([FromBody]ApplicationUser model)
        {
            ActionResult response = null;
            try
            {

                var user = from a in _context.Users
                           where a.Email == model.Email
                           select a;

                string userid = String.Empty;
                foreach (ApplicationUser item in user)
                {
                    userid = item.Id;
                }

                tblCollborator coll = new tblCollborator();
                coll.noteid = Convert.ToInt32(model.postalcode);
                coll.Userid = userid;
                _context.tblCollaborator.Add(coll);
                int j = _context.SaveChanges();

                var colluser = from a in _context.tblCollaborator
                               where a.Userid == userid
                               select a;
                int collid = 0;
                foreach (tblCollborator item in colluser)
                {
                    collid = item.Id;

                }
                tblUserCollaborate collnew = new tblUserCollaborate();
                collnew.collaboratorId = collid;
                collnew.userid = model.aadharno;
                _context.tblUserCollaborates.Add(collnew);
                int i = _context.SaveChanges();
                response = Ok(new { status = "Collaborator added successfully" });

            }
            catch (Exception ex)
            {

                ex.ToString();
                response = Ok(new { status = ex.ToString() });
            }
            
            return response;
        }
        [HttpDelete]
        public IActionResult DeleteCollaborator(string id)
        {
            IActionResult response = null ;
            var listCollaboratorId = new List<int>();
            var listcollate = new List<tblCollborator>();
            try
            {
                var collaboratorids = from a in _context.tblCollaborator
                                      where a.Userid == id
                                      select a;
                foreach (tblCollborator item in collaboratorids)
                {
                    listCollaboratorId.Add(Convert.ToInt32(item.Id));
                    listcollate.Add(item);

                }

                foreach (tblCollborator itemdata in listcollate)
                {
                    _context.tblCollaborator.Remove(itemdata);
                    int i = _context.SaveChanges();
                }


                //foreach (int collaborateids in listCollaboratorId)
                //{
                //    var collaboratoridstodelete = from b in _context.tblUserCollaborates
                //                                  where b.collaboratorId == collaborateids
                //                                  select b;
                //    foreach (tblUserCollaborate item in collaboratoridstodelete)
                //    {
                //        _context.tblUserCollaborates.Remove(item);
                //        int i = _context.SaveChanges();
                //    }

                //}

                response = Ok(new { Success="Note UnCollaborated"});
            }
            catch (Exception ex)
            {

                response = Ok(new { Error = ex.ToString() });
            }
            return response;

        }


        [HttpGet]
        public IActionResult GetCollaborator(string id)
        {
            var list = new List<tblCollborator>();

            var ownercollaborators = new List<tblUserCollaborate>();
            IActionResult response = null;
            var sharedwith = new List<ApplicationUser>();
            try
            {
                var owner = from a in _context.tblUserCollaborates
                            where a.userid == id
                            select a;
                foreach (tblUserCollaborate item in owner)
                {
                    ownercollaborators.Add(item);
                }
                if (ownercollaborators.Count > 0)
                {
                    var collaborators = new List<tblCollborator>();
                    foreach (tblUserCollaborate item in ownercollaborators)
                    {
                        var collaboratordata = from b in _context.tblCollaborator
                                               where b.Id == item.collaboratorId
                                               select b;
                        foreach (tblCollborator note in collaboratordata)
                        {
                            collaborators.Add(note);
                        }
                    }
                    var sharedfrom = new List<ApplicationUser>();
                    var ownerdata = from c in _context.Users
                                    where c.Id == id
                                    select c;
                    foreach (ApplicationUser userdata in ownerdata)
                    {
                        sharedfrom.Add(userdata);
                    }
                    foreach (tblCollborator item in collaborators)
                    {
                        var shareddata = from d in _context.Users
                                         where d.Id == item.Userid
                                         select d;
                        foreach (ApplicationUser userdata in shareddata)
                        {
                            sharedwith.Add(userdata);
                        }
                    }

                    response = Ok(new { collaborate = collaborators, noteowner = sharedfrom,sharedto=sharedwith, status = "owner" });
                }
                else
                {
                    var collaborators = new List<tblCollborator>();
                    var collaboratorsuserids = new List<string>();
                    var noteids = new List<int>();
                        var collaboratordata = from b in _context.tblCollaborator
                                               where b.Userid == id
                                               select b;
                        foreach (tblCollborator note in collaboratordata)
                        {
                            collaborators.Add(note);
                        }
                    foreach (tblCollborator item in collaborators)
                    {
                        noteids.Add(item.noteid);
                        var allshareduser = from x in _context.tblCollaborator
                                            where x.noteid == item.noteid && x.Userid!=id
                                            select x;
                        foreach (tblCollborator collemail in allshareduser )
                        {
                            collaboratorsuserids.Add(collemail.Userid);
                        }
                    }
                    var userids = new List<string>();
                    var useremails = new List<ApplicationUser>();
                    foreach (int noteid in noteids)
                    {
                        var notedata = from c in _context.tblNotes
                                       where c.Id == noteid
                                       select c;
                        foreach (tblNotes item in notedata)
                        {
                            userids.Add(item.UserId);
                        }
                    }
                    foreach (string item in userids)
                    {
                        var useremail = from d in _context.Users
                                        where d.Id == item
                                        select d;
                        foreach (ApplicationUser emails in useremail)
                        {
                            useremails.Add(emails);
                        }
                    }

                    foreach (tblCollborator item in collaborators)
                    {
                        collaboratorsuserids.Add(item.Userid);
                    }
                    var collaboratorsemail = new List<ApplicationUser>();
                    foreach (string item in collaboratorsuserids)
                    {
                        var collemails = from x in _context.Users
                                         where x.Id == item
                                         select x;
                        foreach (ApplicationUser emails in collemails)
                        {
                            collaboratorsemail.Add(emails);
                        }
                    }

                    response = Ok(new { collaborate = collaborators,shredfromemail=useremails,sharedwith=collaboratorsemail,userids=userids ,status = "shared" });
                    }

                



            }
            catch (Exception ex)
            {

                ex.ToString();
            }



        


            return response;
        }


                [HttpPost]
        public IActionResult PostAllCollaborator([FromBody]ApplicationUser model)
        {
            //aadharno=userid
            //postalcode=noteid
            //email=email


            IActionResult response = null;
            var validuser = new List<ApplicationUser>();
            var ownernote = new List<tblNotes>();
            string collaborateid = String.Empty;
            int collatesharedid = 0;
            var sharedcollaboratorid = new List<int>();
            try
            {
                //verify user is registered or not
                var valid = from a in _context.Users
                            where a.Email == model.Email
                            select a;
                foreach (ApplicationUser item in valid)
                {
                    validuser.Add(item);
                    collaborateid = item.Id;
                }
                if (validuser.Count > 0)
                {
                    var owner = from b in _context.tblNotes
                                where b.Id == Convert.ToInt32(model.postalcode) && b.UserId == model.aadharno
                                select b;
                    foreach (tblNotes item in owner) 
                    {
                        ownernote.Add(item);
                    }
                    if (ownernote.Count > 0)
                    {
                        tblCollborator coll = new tblCollborator();
                        coll.Userid = collaborateid;
                        coll.noteid = Convert.ToInt32(model.postalcode);
                        _context.tblCollaborator.Add(coll);
                        int i = _context.SaveChanges();

                        var collaboratordata = from c in _context.tblCollaborator
                                               where c.Userid == collaborateid
                                               select c;

                        foreach (tblCollborator item in collaboratordata)
                        {
                            sharedcollaboratorid.Add(item.Id);
                        }

                        foreach (int data in sharedcollaboratorid)
                        {
                            tblUserCollaborate usercoll = new tblUserCollaborate();
                            usercoll.collaboratorId = data;
                            usercoll.userid = model.aadharno;
                            _context.tblUserCollaborates.Add(usercoll);
                            int  j= _context.SaveChanges();
                        }




                    }
                    else
                    {

                       
                        tblCollborator coll = new tblCollborator();
                        coll.Userid = collaborateid;
                        coll.noteid = Convert.ToInt32(model.postalcode);
                        _context.tblCollaborator.Add(coll);
                        int i = _context.SaveChanges();

                        var tblCollData = from d in _context.tblCollaborator
                                          where d.noteid == coll.noteid
                                          select d;

                        foreach (tblCollborator item in tblCollData)
                        {
                            collatesharedid = item.Id;
                        }

                        var findowner = (from e in _context.tblCollaborator
                                         join f in _context.tblUserCollaborates
                                          on e.Id equals f.collaboratorId
                                         select new { e.noteid,e.Userid,f.collaboratorId,f.userid}).ToList();


                        foreach (var item in findowner)
                        {
                            tblUserCollaborate usercoll = new tblUserCollaborate();
                            usercoll.userid = item.userid;
                            usercoll.collaboratorId = collatesharedid;
                            _context.tblUserCollaborates.Add(usercoll);
                            int k = _context.SaveChanges();
                             
                            
                        }


                    }

                    response = Ok(new { owner=ownernote,status="owner"});
                }
                else
                {
                    return response = Ok(new { ErrorMessage="User is not registered so note cannot be shared"});
                }


            }
            catch (Exception ex)
            {

                ex.ToString();
            }


            return response;

        }



        [HttpGet]
        public IActionResult GetAllCollaborators(string id)
        {
            IActionResult response = null;
            
            var owner = new List<tblUserCollaborate>();
            try
            {
                var findowner = from a in _context.tblUserCollaborates
                                where a.userid == id
                                select a;
                foreach (var item in findowner)
                {
                    owner.Add(item);
                }
                if (owner.Count > 0)
                {


                    List<CollaboratorModel> list = new List<CollaboratorModel>();
                    //foreach (var data in owner)
                    //{
                        //var getnotes=from b in _context.tblCollaborator
                        //             where b.Id==data.collaboratorId
                        //             select 

                        var getnotes= (from b in _context.tblCollaborator
                                        join c in _context.tblNotes 
    on b.noteid equals c.Id
                                        join d in _context.tblUserCollaborates on b.Id equals d.collaboratorId
                                        select new { b.noteid, b.Id,b.Userid, c.Title, c.Content,d.collaboratorId,d.userid }).ToList();

                        foreach (var notedata in getnotes)
                        {
                            list.Add(new CollaboratorModel
                            {
                                nodeid =notedata.noteid,
                                noteuserid =notedata.Id,
                                collaboratoruserid=notedata.Userid,
                                Title=notedata.Title,
                                Content=notedata.Content,
                                collaboratorid=notedata. collaboratorId,
                                usercollaboraterid=notedata.userid
                            });
                        }

                        response = Ok(new { ownerdata=owner,shareddata=list});    

                    }
                    //return response = Ok(new { ownerdata=owner});
                //}

            }
            catch (Exception ex)
            {

                ex.ToString();
            }

            return response;
        }




    }


    public class CollaboratorModel
    {
        public int nodeid { get; set; }
        public int noteuserid { get; set; }
        public string collaboratoruserid { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
        public int collaboratorid { get; set; }
        public string  usercollaboraterid { get; set; }
    }

}