using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceRealty.Models;

namespace SpaceRealty.Repos
{
    public interface IUserRepository : IDisposable
    {
        void CreateUser(Realtor realtor);
        bool AuthenticateUser(Realtor realtor);
    }
}
