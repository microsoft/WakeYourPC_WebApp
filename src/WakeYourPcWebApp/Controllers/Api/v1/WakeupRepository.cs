using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakeYourPcWebApp.Models;

namespace WakeYourPcWebApp.Controllers.Api.v1
{
    public class WakeupRepository : IWakeupRepository
    {
        private readonly WakeupContext _context;

        public WakeupRepository(WakeupContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(x => x != null && x.Username == username);
        }

        public IEnumerable<Machine> GetAllMachines(string username)
        {
            return _context.Machines
                .Where(x => x != null && x.User.Username == username).ToList();
        }

        public IEnumerable<Machine> GetMachinesToWakeup(string username)
        {
            return _context.Machines
                .Where(x => x != null && x.User.Username == username 
                && x.ShouldWakeup.HasValue && x.ShouldWakeup.Value).ToList();
        }

        public void AddMachine(Machine machine, string username)
        {
            machine.User = this.GetUser(username);
            _context.Add(machine);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
        }
    }
}
