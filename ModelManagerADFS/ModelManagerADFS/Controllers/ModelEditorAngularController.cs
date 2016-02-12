using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelManagerADFS.Controllers
{
    [Authorize]
    public class ModelEditorAngularController : Controller
    {
        // GET: ModelEditorAngular
        public ActionResult Index()
        {
            return View();
        }
    }
}