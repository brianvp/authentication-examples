using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ModelManagerOAuthIndividual.Controllers
{
    public class SecurityController : ApiController
    {
        // /api/security/UserAuthenticated
        [HttpGet]
        [AllowAnonymous]
        public bool UserAuthenticated()
        {
            return RequestContext.Principal.Identity.IsAuthenticated;
        }

        // /api/security/UserInrole?rolename=ModelEditorRole
        [HttpGet]
        [AllowAnonymous]
        public bool UserInRole(string roleName)
        {
            return User.IsInRole(roleName);
        }
    }
}
