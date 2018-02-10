using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceRealty.Models;
using SpaceRealty.Repos;

namespace SpaceRealty.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProperty()
        {
            House newListing = new House();
            return View("~/Views/Property.cshtml", newListing);
        }

        public IActionResult SubmitProperty(House house)
        {
            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                if (!house.Edit)
                {
                    propRep.CreateHouse(house);
                }
                else
                {
                    propRep.EditHouse(house);
                }
                Properties = propRep.PopulateHouses();
            }
            return View("~/Views/Houses.cshtml", Properties);
        }

        public IActionResult EditHouse(House selectedHouse)
        {
            return View("~/Views/Property.cshtml", selectedHouse);
        }

        public IActionResult DeleteHouse(int houseId)
        {
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                propRep.DeleteHouse(houseId);
            }
            return RedirectToAction("Request");
        }

        //the table itself should be a partial view
        public IActionResult SearchHouses(List<House> houses, string searchTerm)
        {
            return View("~/Views/Houses.cshtml", houses);
        }
    }
}