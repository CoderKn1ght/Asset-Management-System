using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Asset_Management_System.Authorize;
using Asset_Management_System.Data;
using Asset_Management_System.Models.AssetManagementSystem;

namespace Asset_Management_System.Controllers
{
    [CustomLoginFilter]
    public class FacilitiesController : Controller
    {
        private readonly AssetManagementContext  _db = new AssetManagementContext();
        // GET: Facilities
        public ActionResult Index()
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var user = _db.Users.SingleOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return View(user.IsAdmin != true ? user.Facilities.Where(f => f.IsActive).ToList() : _db.Facilities.Where(f => f.IsActive).ToList());
            }

            ModelState.AddModelError("", "Invalid User");
            return View();
        }

        public ActionResult Resources(int id)
        {
            var relatedResources = _db.Facilities.Include(x => x.Resources).SingleOrDefault(f => f.Id == id && f.IsActive);
            if (relatedResources == null)
            {
                ModelState.AddModelError("", "No such Facility Exists");
            }
            return View(relatedResources);
        }

        // GET: Facilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var facility = _db.Facilities.SingleOrDefault(f=>f.IsActive && f.Id == id);
            if (facility == null || facility.IsActive == false)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // GET: Facilities/Create
        [CustomAdminOnlyFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Address,IsActive")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                _db.Facilities.Add(facility);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facility);
        }

        // GET: Facilities/Edit/5
        [CustomAdminOnlyFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var facility = _db.Facilities.SingleOrDefault(f => f.IsActive && f.Id == id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Address")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(facility).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facility);
        }

        // GET: Facilities/Delete/5
        [CustomAdminOnlyFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var facility = _db.Facilities.SingleOrDefault(f => f.IsActive && f.Id == id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var facility = _db.Facilities.SingleOrDefault(f => f.IsActive && f.Id == id);
            if (facility == null) return RedirectToAction("Index");
            if (facility.Resources.Count > 0) { 
                ModelState.AddModelError("","Cannot delete a facility that has resources assigned to it. Kindly delete the resources and try again.");
                return View("Delete", facility);
            }
            facility.IsActive = false;
            _db.SaveChanges();
            return RedirectToAction("Index");
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