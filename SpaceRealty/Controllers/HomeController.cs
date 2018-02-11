using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceRealty.Models;
using SpaceRealty.Repos;

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
            bool authed = false;
            using (IUserRepository userRep = new UserRepository())
            {
                if (realtor.email == null)
                {
                    authed = userRep.AuthenticateUser(realtor);
                }
                else
                {
                    userRep.CreateUser(realtor);
                    authed = true;
                }
            }

            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                Properties = propRep.PopulateHouses();
            }

            if (authed == true)
                return View("~/Views/Property/Houses.cshtml", Properties);
            else
                return View("~/Views/Home/Login.cshtml");

        }
    }
}
