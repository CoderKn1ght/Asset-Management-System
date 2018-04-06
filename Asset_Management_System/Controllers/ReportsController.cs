using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Asset_Management_System.Authorize;
using Asset_Management_System.Data;

namespace Asset_Management_System.Controllers
{
    [CustomLoginFilter]
    public class ReportsController : Controller
    {
        AssetManagementContext _db = new AssetManagementContext();
        // GET: Reports
        public ActionResult Index()
        {
            return View(_db.Facilities.Where(f => f.IsActive).Include(r=>r.Resources).ToList());
        }
    }
}