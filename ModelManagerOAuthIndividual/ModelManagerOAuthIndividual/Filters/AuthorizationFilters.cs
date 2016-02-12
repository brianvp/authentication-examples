using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;

namespace ModelManagerOAuthIndividual.Filters
{
    public class AuthorizeRedirectMVCAttribute : System.Web.Mvc.AuthorizeAttribute
	{
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/AccessDenied");
            }
        }
    }

    public class AuthorizeRedirectAPIAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);

            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                //note, need to use forbidden here - 401 may be semantically correct, but will
                //appear to asp.net as an unauthenticated user and simply redirect you to the logon
                //page...   
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
}