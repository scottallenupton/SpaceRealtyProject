using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceRealty.Models;

namespace SpaceRealty.Repos
{
    public interface IPropertyRepository : IDisposable
    {
        List<House> PopulateHouses();
        void CreateHouse(House house);
        void EditHouse(House house);
        void DeleteHouse(int MLSnum);
    }
}
