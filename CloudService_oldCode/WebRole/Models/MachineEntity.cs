using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WakeYourPC.WakeUpService.Models
{
    using Microsoft.WindowsAzure.Storage.Table;
    public enum MachineState
    {
        Available,
        Asleep,
        WakingUp,
        Unknown
    }

    public class MachineEntity : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineEntity"/> class.
        /// Your entity type must expose a parameter-less constructor
        /// </summary>
        public MachineEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="name">The last name.</param>
        /// <param name="firstName">The first name.</param>
        public MachineEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string Username { get; set; }

        public string MachineName { get; set; }

        public string HostName { get; set; }

        public string Guid { get; set; }

        public string MacAddress { get; set; }

        public string State { get; set; }

        public bool? ShouldWakeup { get; set; }
    }
}