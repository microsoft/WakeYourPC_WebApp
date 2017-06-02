using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace WakeYourPcWebApp.Models
{
    public enum MachineState
    {
        Unknown,
        Available,
        Asleep,
        WakingUp,
    }

    public class Machine
    {
        public User User { get; set; }
        
        public string MachineName { get; set; }

        public string HostName { get; set; }

        [Key]
        public Guid Guid { get; set; }

        public string MacAddress { get; set; }

        public MachineState State { get; set; }

        public bool? ShouldWakeup { get; set; }
    }
}
