using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using ModelManagerADFS.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelManagerADFS
{
    public partial class Startup
    {
        private static string realm = ConfigurationManager.AppSettings["ida:Wtrealm"];
        private static string adfsMetadata = ConfigurationManager.AppSettings["ida:ADFSMetadata"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);


            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWsFederationAuthentication(
                new WsFederationAuthenticationOptions
                {
                    Wtrealm = realm,
                    MetadataAddress = adfsMetadata,
                    //custom code below to respond to notifications
                    Notifications = new WsFederationAuthenticationNotifications
                    {
                        AuthenticationFailed = context =>
                       {
                           return Task.FromResult(0);
                       },
                        MessageReceived = context =>
                        {
                            return Task.FromResult(0);
                        },
                        RedirectToIdentityProvider = context =>
                        {
                            return Task.FromResult(0);
                        },
                        SecurityTokenReceived = context =>
                        {
                            return Task.FromResult(0);
                        },
                        SecurityTokenValidated = context =>
                        {

                            // Is this the point I can connect this authenticated user to the Identity database (roles) ?
                            /*
                            context.AuthenticationTicket.Identity.AddClaim(
                                        new Claim(ClaimTypes.Role, "ModelEditorRole")
                                        );
                            context.AuthenticationTicket.Identity.AddClaim(
                                        new Claim(ClaimTypes.Role, "SomeOtherRole")
                                        );
                                        */

                            string accountName = "";
                            string accountEmail = "";
                            
                            foreach (var claim in context.AuthenticationTicket.Identity.Claims)
                            {
                                // instead of using "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn"
                                // use enum
                                if (claim.Type == ClaimTypes.Upn)
                                {
                                     accountName = claim.Value;

                                    //do something? Connect to ASP.NET Identity database?
                                    // Can we "log in" an account in asp.net identity matching the user identity
                                    // e.g. brianvp@vpinc.net?
                                    // or do we add the account into the identity database, and only use to query roles
                                    // from the identity database ?
                                    // mainly, we want the current principal to pass an 
                                    // [Authorize roles("roleX")] test
                                    // how about context.AuthenticationTicket.Identity.AddClaim() ? 

                                }
                                else if (claim.Type == ClaimTypes.Email)
                                {
                                    accountEmail = claim.Value;
                                }
                                else if (claim.Type == ClaimTypes.Role)
                                {   // these would be roles coming from Active Directory, not the Identity database
                                    string roleName = claim.Value;
                                }
                               
                            }

                            var db = new ApplicationDbContext();

                            //Add the user if necessary
                            var user = (from u in db.Users
                                        where u.UserName.ToLower() == accountName.ToLower()
                                        select u).FirstOrDefault();

                            //get the users roles and add to the claims
                            if (user == null)
                            {
                                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                                var adminUser = new ApplicationUser()
                                {
                                    UserName = accountName,
                                    Email = accountEmail
                                };

                                var ir = um.Create(adminUser, "placeholder#123X"); // not sure how I feel about this... does it need a password?

                            }

                            var userRoles = (from u in db.Users
                                             from ur in u.Roles
                                             join r in db.Roles on ur.RoleId equals r.Id
                                             where u.UserName.ToLower() == accountName.ToLower()
                                             select new
                                             {
                                                 RoleName = r.Name

                                             }).ToList();

                            foreach (var role in userRoles)
                            {
                                context.AuthenticationTicket.Identity.AddClaim(
                                        new Claim(ClaimTypes.Role, role.RoleName));
                            }



                            return Task.FromResult(0);
                        }



                        
                    }


                });
        }
    }
}