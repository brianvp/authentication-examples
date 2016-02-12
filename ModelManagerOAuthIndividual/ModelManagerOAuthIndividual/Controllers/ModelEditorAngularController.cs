using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelManagerOAuthIndividual.Filters;

namespace ModelManagerOAuthIndividual.Controllers
{
    [AuthorizeRedirectMVC]
    public class ModelEditorAngularController : Controller
    {
        // GET: ModelEditorAngular
        public ActionResult Index()
        {
            return View();
        }
    }
}