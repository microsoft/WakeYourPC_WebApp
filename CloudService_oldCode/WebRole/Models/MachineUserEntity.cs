using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WakeYourPC.WakeUpService.Models
{
    using Microsoft.WindowsAzure.Storage.Table;
    /*
    public class MachineUserEntity : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineUserEntity"/> class.
        /// Your entity type must expose a parameter-less constructor
        /// </summary>
        public MachineUserEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineUserEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        public MachineUserEntity(string guid, string username)
        {
            PartitionKey = username;
            RowKey = guid;
        }

        public string Username { get; set; }

        public string Guid { get; set; }

    }
    */
}