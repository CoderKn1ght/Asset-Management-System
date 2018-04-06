using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Asset_Management_System.Authorize;
using Asset_Management_System.Data;
using Asset_Management_System.Models.AssetManagementSystem;

namespace Asset_Management_System.Controllers
{
    [CustomLoginFilter]
    [CustomAdminOnlyFilter]
    public class UsersController : Controller
    {
        readonly AssetManagementContext _db = new AssetManagementContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(_db.Users.Where(u=>u.IsActive).Include(u=>u.Facilities).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Include(f => f.Facilities).SingleOrDefault(i => i.Id == id && i.IsActive);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var user = new User
            {
                IsActive = true,
                Facilities = new List<Facility>()
            };
            PopulateAssignedFacilities(user);
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,FirstName,LastName,IsAdmin,Password,EmailId,IsActive")] User user, string[] selectedFacilities)
        {
            if (selectedFacilities != null)
            {
                user.Facilities = new List<Facility>();
                foreach (var facility in selectedFacilities)
                {
                    var facilityToAdd = _db.Facilities.Find(int.Parse(facility));
                    user.Facilities.Add(facilityToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                SendUserMail(user);
                return RedirectToAction("Index");
            }
            PopulateAssignedFacilities(user);
            return View(user);
        }

        private static void SendUserMail(User user)
        {
            const string serverUserName = "info.assetmanagementsystem@gmail.com";
            const string serverPassword = "CSE5320#";
            var role = user.IsAdmin ? "Admin" : "Resource Checker";
            var subject = "Welcome Aboard " + role;
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(serverUserName,serverPassword)
            };
            try
            {
                var body = "Hi " + user.FirstName + " " + user.LastName + ",\n\n\t"
                           + "Your account has been created. You are appointed as the ";
                if (role == "Admin")
                {
                    body = body +
                           "Admin of our Asset Management System. Your responsibilities include managing all the users, " +
                           "facilities and resources and periodically check the report for the facilities\n\n";
                }
                else
                {
                    body = body + "Resource Checker for the following facilities of our Asset Management System : \n";
                    foreach (var facility in user.Facilities)
                    {
                        body += facility.Name + "\n";
                    }
                }

                body += "\nNavigate to http://localhost:53810 and login with your credentials mentioned below. \n" +
                        "Username : " + user.UserName +
                        "\nPassword : " + user.Password + "\n\n" +
                        "Regards," +
                        "\nAsset Management Team";
                client.Send(serverUserName, user.EmailId, subject, body);
            }
            catch (Exception ex)
            {
                var httpStatusCodeResult = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                Console.WriteLine(ex.Message);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //User user = _db.Users.Find(id);
            var user = _db.Users.Include(f => f.Facilities).SingleOrDefault(i => i.Id == id && i.IsActive);
            if (user == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedFacilities(user);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedFacilities)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var user = _db.Users.Include(f => f.Facilities).SingleOrDefault(i => i.Id == id);
            if (TryUpdateModel(user, "",
                new[]
                    {"Id", "UserName", "FirstName", "LastName", "IsAdmin", "Password", "EmailId", "IsActive"}))
            {
                try
                {
                    UpdateUserFacilities(selectedFacilities, user);
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again later.");
                }
            }
            PopulateAssignedFacilities(user);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.SingleOrDefault(i => i.Id == id && i.IsActive);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _db.Users.SingleOrDefault(i => i.Id == id && i.IsActive);
            if (user != null)
            {
                if (user.Id == 1)
                {
                    ModelState.AddModelError("", "Cannot delete the Super Admin");
                    return View("Delete", user);
                }
                user.IsActive = false;
                _db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "User does not exist");
            }
            return RedirectToAction("Index");
        }

        public void PopulateAssignedFacilities(User user)
        {
            var facilities = _db.Facilities.Where(f=>f.IsActive);
            var userFacilities = new HashSet<int>(user.Facilities.Select(f=>f.Id));
            var viewModel = new List<UserFacilityViewModel>();
            foreach (var facility in facilities)
            {
                viewModel.Add(new UserFacilityViewModel
                {
                    FacilityId = facility.Id,
                    FacilityName = facility.Name,
                    Assigned = userFacilities.Contains(facility.Id)
                });
            }

            ViewBag.Facilities = viewModel;
        }

        private void UpdateUserFacilities(string[] selectedFacilities, User user)
        {
            if (selectedFacilities == null)
            {
                user.Facilities = new List<Facility>();
                return;
            }

            var selectedFacilitiesHashSet = new HashSet<string>(selectedFacilities);
            var userFacilities = new HashSet<int>(user.Facilities.Select(f=>f.Id));
            foreach (var facility in _db.Facilities.Where(f=>f.IsActive))
            {
                if (selectedFacilitiesHashSet.Contains(facility.Id.ToString()))
                {
                    if (!userFacilities.Contains(facility.Id))
                    {
                        user.Facilities.Add(facility);
                    }
                }
                else
                {
                    if (userFacilities.Contains(facility.Id))
                    {
                        user.Facilities.Remove(facility);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
