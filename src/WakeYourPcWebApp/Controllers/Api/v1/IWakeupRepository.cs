using System.Collections.Generic;
using System.Threading.Tasks;
using WakeYourPcWebApp.Models;

namespace WakeYourPcWebApp.Controllers.Api.v1
{
    public interface IWakeupRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(string username);
        IEnumerable<Machine> GetAllMachines(string username);
        IEnumerable<Machine> GetMachinesToWakeup(string username);
        void AddMachine(Machine machine, string username);

        Task<bool> SaveChangesAsync();
        void AddUser(User user);
    }
}