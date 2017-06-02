using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakeYourPcWebApp.Models;

namespace WakeYourPcWebApp.ViewModels
{
    public class MachineViewModel
    {
        [Required]
        public string MachineName { get; set; }

        [Required]
        public string HostName { get; set; }

        public MachineState State { get; set; }

        public bool? ShouldWakeup { get; set; }

    }
}
