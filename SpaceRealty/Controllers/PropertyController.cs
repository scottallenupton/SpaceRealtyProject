using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            //Open Create window with new house
            House newListing = new House();
            return View("~/Views/Property/CreateProperty.cshtml", newListing);
        }

        public IActionResult CreateProperty(House house, IFormFile fileSelect)
        {
            //Create house in database
            using (var target = new MemoryStream())
            {
                fileSelect.CopyTo(target);
                house.PhotoData = target.ToArray();
            }
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
            //Open Edit house window with existing house
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                selectedHouse.Photos = propRep.SelectPhotos(selectedHouse.MLSNum);
            }
            return View("~/Views/Property/EditProperty.cshtml", selectedHouse);
        }
        public IActionResult EditProperty(House house, IFormFile fileSelect)
        {
            //Edit house in database
            using (var target = new MemoryStream())
            {
                fileSelect.CopyTo(target);
                house.PhotoData = target.ToArray();
            }
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
            //Delete house from database
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
            //Search house functionality, onkeyup of search box
            List<House> Properties;
            using (IPropertyRepository propRep = new PropertyRepository())
            {
                Properties = propRep.PopulateHouses();
            }
            if (searchTerm != "" && searchTerm != null)
            {
                //Select all houses where the search term exists in the correct fields
                Properties = Properties.Where(p => p.MLSNum.ToString().Contains(searchTerm) ||
                p.City.Contains(searchTerm) || p.State.Contains(searchTerm) ||
                p.ZipCode.ToString().Contains(searchTerm) || p.Bedrooms.ToString().Contains(searchTerm) ||
                p.Bathrooms.ToString().Contains(searchTerm) || p.SquareFeet.ToString().Contains(searchTerm)).ToList();
            }

            return PartialView("_HousesList", Properties);
        }
    }
}