using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WakeYourPC.WakeUpService.Models
{
    using Microsoft.WindowsAzure.Storage.Table;

    public class UserEntity : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineEntity"/> class.
        /// Your entity type must expose a parameter-less constructor
        /// </summary>
        public UserEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        public UserEntity(string username, string password = "")
        {
            PartitionKey = username.FirstOrDefault().ToString();
            RowKey = username;
        }

        /// <summary>
        /// Gets or sets the User name for the customer.
        /// A property for use in Table storage must be a public property of a 
        /// supported type that exposes both a getter and a setter.        
        /// </summary>
        /// <value>
        /// The User name.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the customer.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

    }
}