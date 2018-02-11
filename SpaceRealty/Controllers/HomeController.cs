using Microsoft.AspNetCore.Mvc;
using SpaceRealty.Models;
using SpaceRealty.Repos;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceRealty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Home(Realtor realtor)
        {
            //TODO: Prevent user access into system without authentication
            //Authenticate User
            bool authed = false;
            using (IUserRepository userRep = new UserRepository())
            {
                //If email is null, then user is an existing user
                if (realtor.email == null)
                {
                    //Auth existing user
                    authed = userRep.AuthenticateUser(realtor);
                }
                else
                {
                    //Create new user
                    userRep.CreateUser(realtor);
                    authed = true;
                }
            }

            List<House> Properties;
            //Retrieve all properties
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                Properties = propRep.PopulateHouses();
            }

            //Display houses if authenticated
            if (authed == true)
                return View("~/Views/Property/Houses.cshtml", Properties);
            else
                return View("~/Views/Home/Login.cshtml");

        }
    }
}
