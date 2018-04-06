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
    public class ResourcesController : Controller
    {
         private readonly AssetManagementContext _db = new AssetManagementContext();

        // GET: Resources
        public ActionResult Index()
        {
            var resources = _db.Resources.Where(r => r.IsActive).Include(f => f.Facility);
            return View(resources.ToList());
        }

        // GET: Resources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = _db.Resources.SingleOrDefault(r=>r.IsActive == true && r.Id == id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            ViewBag.FacilityId = new SelectList(_db.Facilities.Where(f => f.IsActive), "Id", "Name");
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ResourceName,Quantity,Description,Color,Size,IsActive,FacilityId")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _db.Resources.Add(resource);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacilityId = new SelectList(_db.Facilities.Where(f=>f.IsActive), "Id", "Name", resource.FacilityId);
            return View(resource);
        }

        public ActionResult UpdateQuantity(int? id, string validate)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = _db.Resources.SingleOrDefault(r => r.IsActive == true && r.Id == id);

            if (resource == null)
            {
                return HttpNotFound();
            }

            var viewModel = new InventoryCheckViewModel
            {
                ResourceId = resource.Id,
                Quantity = resource.Quantity,
                ResourceName = resource.ResourceName,
                FacilityId = resource.FacilityId
            };
                
            if ((string)Session["IsAdmin"] == "True")
            {
                viewModel.ResourceCheckerComments = resource.ResourceCheckerComments;
            }

            ViewBag.Validate = validate;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity([Bind(Include = "ResourceId,ResourceName,Quantity,ResourceCheckerComments,AdminComments,FacilityId")] InventoryCheckViewModel resource, string validate)
        {
            
            var currentResource = _db.Resources.SingleOrDefault(r => r.Id == resource.ResourceId);
            if (ModelState.IsValid && currentResource!=null)
            {
                if (currentResource.Quantity != resource.Quantity)
                {
                    if((string)Session["IsAdmin"] == "False")
                    {
                        currentResource.AdminComments = null;
                        currentResource.IsValid = false;
                    }
                    if ((resource.AdminComments == null || resource.AdminComments.Equals(string.Empty)))
                        currentResource.IsValid = false;
                    else
                    {
                        currentResource.IsValid = true;
                    }
                }
                else
                {
                    currentResource.IsValid = true;
                }
                currentResource.AdminComments = resource.AdminComments;
                currentResource.Quantity = resource.Quantity;
                currentResource.ResourceCheckerComments = resource.ResourceCheckerComments;
                _db.Entry(currentResource).State = EntityState.Modified;
                _db.SaveChanges();
                var facilityId = currentResource.FacilityId;
                if (validate != null)
                {
                    return RedirectToAction("Index", "Reports");
                }
                return RedirectToAction("Resources", "Facilities", new { id = facilityId });
            }

            return HttpNotFound();
        }
        // GET: Resources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = _db.Resources.SingleOrDefault(r => r.IsActive == true && r.Id == id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacilityId = new SelectList(_db.Facilities, "Id", "Name", resource.FacilityId);
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ResourceName,Quantity,Description,Color,Size,IsActive,FacilityId")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(resource).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityId = new SelectList(_db.Facilities, "Id", "Name", resource.FacilityId);
            return View(resource);
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = _db.Resources.SingleOrDefault(r => r.IsActive == true && r.Id == id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var resource = _db.Resources.SingleOrDefault(r => r.IsActive == true && r.Id == id);
            if (resource == null)
            {
                ModelState.AddModelError("", "Resource not found");
            }
            else
            {
                resource.IsActive = false;
                _db.SaveChanges();
            }
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
