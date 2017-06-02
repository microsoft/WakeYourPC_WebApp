using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeYourPcWebApp.Models
{
    public class WakeupContextSeedData
    {
        private readonly WakeupContext _context;

        public WakeupContextSeedData(WakeupContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Machines.Any())
            {

                var machine = new Machine
                {
                    HostName = "Machine 1",
                    MachineName = "Machine 1",
                    Guid = Guid.NewGuid(),
                    User = new User
                    {
                        Username = "saimanoj"
                    }
                };

                _context.Users.Add(machine.User);

                _context.Machines.Add(machine);

                await _context.SaveChangesAsync();

            };

        }
    }
}

