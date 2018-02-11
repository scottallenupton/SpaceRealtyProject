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

        public IActionResult CreateHouse()
        {
            House newListing = new House();
            return View("~/Views/Property/CreateProperty.cshtml", newListing);
        }

        public IActionResult CreateProperty(House house)
        {
            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                propRep.CreateHouse(house);
                Properties = propRep.PopulateHouses();
            }
            return View("~/Views/Property/Houses.cshtml", Properties);
        }

        public IActionResult EditHouse(House selectedHouse)
        {
            return View("~/Views/Property/EditProperty.cshtml", selectedHouse);
        }
        public IActionResult EditProperty(House house)
        {
            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                propRep.EditHouse(house);
                Properties = propRep.PopulateHouses();
            }
            return View("~/Views/Property/Houses.cshtml", Properties);
        }


        public IActionResult DeleteHouse(int houseId)
        {
            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                propRep.DeleteHouse(houseId);
                Properties = propRep.PopulateHouses();
            }
            return View("~/Views/Property/Houses.cshtml", Properties);
        }

        public IActionResult SearchHouses(string searchTerm)
        {
            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                Properties = propRep.PopulateHouses();
            }
            if (searchTerm != "" && searchTerm != null)
            {
                Properties = Properties.Where(p => p.MLSNum.ToString().Contains(searchTerm) ||
                p.City.Contains(searchTerm) || p.State.Contains(searchTerm) ||
                p.ZipCode.ToString().Contains(searchTerm) || p.Bedrooms.ToString().Contains(searchTerm) ||
                p.Bathrooms.ToString().Contains(searchTerm) || p.SquareFeet.ToString().Contains(searchTerm)).ToList();
            }

            return PartialView("_HousesList", Properties);
        }
    }
}