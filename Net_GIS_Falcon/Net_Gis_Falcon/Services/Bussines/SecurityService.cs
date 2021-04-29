using Net_Gis_Falcon.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net_Gis_Falcon.Services.Bussines
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();

        public bool Authenticate(Usuario user)
        {
            return daoService.FindByUser(user);
        }

        public bool AuthenticateById(Usuario user)
        {
            return daoService.FindById(user);
        }

        public bool AuthenticateByEmail(Usuario user)
        {
            return daoService.FindByEmail(user);
        }

        public bool Create(Usuario user)
        {
            return daoService.InsertAndCreateCookie(user);
        }

        public bool Update(Usuario user)
        {
            return daoService.UpdateByEmail(user);
        }
    }
}
