using SawDust.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawDust.DataAccess
{
    interface IDataAccess
    {
        /* User */

        Boolean Add(User user);
        Boolean Update(User user);
        List<User> getAllUsers();
        Boolean Delete(User user);

        /* Client */

        Boolean Add(Client client);
        Boolean Update(Client client);
        List<Client> GetAllClients();
        Boolean Delete(Client client);

        /* Job */

        Boolean Add(Job job);
        Boolean Update(Job job);
        Boolean Delete(Job job);
        List<Job> GetAllJobs();
        List<Job> GetAllJobsByClient(Client client);

    }
}
