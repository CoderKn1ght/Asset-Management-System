using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;
using Biz.Interfaces;
using Core.Domains;
using Web.ViewModels;

namespace Web.Controllers
{
    public class StandardController : Controller
    {

        #region Properties

        private readonly IFacilityService _facilityService;

        #endregion

        #region Constructor

        public StandardController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        #endregion

        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var facilities = _facilityService.GetAll();
            var model = new StandardIndexViewModel(facilities);
            return View("Index",model);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var facility = new Facility()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Landmark = model.Landmark,
                        Address = model.Address,
                        Address2 = model.Address2,
                        City = model.City,
                        State = model.State,
                        ZipCode = model.ZipCode,
                        IsActive = true
                    };

                    _facilityService.InsertOrUpdate(facility);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var facility = _facilityService.GetById(id??0);

            if (facility == null)
            {
                return HttpNotFound();
            }

            var model = new FacilityViewModel(facility);
            return View("Edit",model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(FacilityViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var facility = new Facility()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Landmark = model.Landmark,
                        Address = model.Address,
                        Address2 = model.Address2,
                        City = model.City,
                        State = model.State,
                        ZipCode = model.ZipCode,
                        IsActive = true
                    };

                    _facilityService.InsertOrUpdate(facility);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return RedirectToAction("Edit",model);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var facility = _facilityService.GetById(id ?? 0);

            if (facility == null)
            {
                return HttpNotFound();
            }

            var model = new FacilityViewModel(facility);
            return View("Details", model);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var facility = _facilityService.GetById(id??0);
            var model = new FacilityViewModel(facility);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View("Delete",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var facility = _facilityService.GetById(id);
                _facilityService.Delete(facility);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
