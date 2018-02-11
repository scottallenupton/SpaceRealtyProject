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
        void CreatePhoto(byte[] photo, int MLSNum);
        List<string> SelectPhotos(int MLSNum);
        void EditHouse(House house);
        void DeleteHouse(int MLSnum);
    }
}
