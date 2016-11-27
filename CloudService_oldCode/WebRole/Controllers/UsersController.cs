using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WakeYourPC.WakeUpService.Models;
using WakeYourPC.WakeUpService.Utilities;

namespace WakeYourPC.WakeUpService.Controllers
{
    public class UsersController : ApiController
    {
        private string TableName = "User";
        private CloudTable Table;

        // GET api/values
        public IEnumerable<UserEntity> Get()
        {
            InitializeTable();
            var query = (from user in Table.CreateQuery<UserEntity>()
                         select user).Take(100);
            var users = query.ToList();

            return users.Select(x => new UserEntity()
            {
                Username = x.Username
            });
        }

        private void InitializeTable()
        {
            if (this.Table == null)
            {
                this.Table = AzureTableHelper.GetTableReferenceAsync(TableName);
            }
        }

        // GET api/values/5
        public UserEntity Get(string id)
        {
            InitializeTable();
            var result = (from user in Table.CreateQuery<UserEntity>()
                         where user.Username == id
                         select user).FirstOrDefault();
            if(result == null)
            {
                return new UserEntity()
                {
                    Username = "Error: user not found"
                };
            }

            return new UserEntity()
            {
                Username = result.Username
            };
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            InitializeTable();
        }

        // PUT api/values/5
        public async Task<UserEntity> Put(string id, [FromBody]UserEntity value)
        {
            if (value == null)
            {
                return new UserEntity
                {
                    Username = "Error: while deserializing the body"
                };
            }

            InitializeTable();

            UserEntity userEntity = (from machine in this.Table.CreateQuery<UserEntity>()
                                           where machine.PartitionKey == id.First().ToString()
                                           where machine.RowKey == id
                                           select machine).FirstOrDefault();

            if (userEntity == null)
            {
                userEntity = new UserEntity
                {
                    Username = id,
                    PartitionKey = id.First().ToString(),
                    RowKey = id,
                    Timestamp = DateTime.UtcNow,
                };
            }

            await AzureTableHelper.InsertOrMergeEntityAsync(this.Table, userEntity);
            return userEntity;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            InitializeTable();
        }


    }
}
