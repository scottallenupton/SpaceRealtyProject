using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceRealty.Models;

namespace SpaceRealty.Repos
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser(Realtor realtor)
        {
            
        }

        public bool AuthenticateUser(Realtor realtor)
        {
            return true;
        }

        public Realtor RetrieveUser(int id)
        {
            throw new NotImplementedException();
        }

        //TODO: Dispose properly
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
