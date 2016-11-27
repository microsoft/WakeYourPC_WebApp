using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WakeYourPC.WakeUpService.Models;
using WakeYourPC.WakeUpService.Utilities;

namespace WakeYourPC.WakeUpService.Controllers
{
    public class MachinesController : ApiController
    {
        private string TableName = "Machine";

        private CloudTable Table;

        // GET api/values
        public IEnumerable<MachineEntity> Get(string id1)
        {
            InitializeTable();

            var query = (from machine in Table.CreateQuery<MachineEntity>()
                         where machine.PartitionKey == id1
                         select machine).Take(200);

            var machines = query.ToList();

            return machines.Select(x=> new MachineEntity()
            {
                Guid = x.Guid,
                Username = x.Username,
                ShouldWakeup = x.ShouldWakeup,
                MachineName = x.MachineName,
                HostName = x.HostName,
                MacAddress = x.MacAddress,
                State = x.State,
                Timestamp = x.Timestamp,
                ETag = x.ETag,
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
        public MachineEntity Get(string id1, string id2)
        {
            InitializeTable();

            var machineName = (from machine in Table.CreateQuery<MachineEntity>()
                               where machine.PartitionKey == id1
                               where machine.RowKey == id2
                               select machine).FirstOrDefault();
            if (machineName == null)
            {
                return new MachineEntity()
                {
                    MachineName = "Error: this machine does not belong to the user"
                };
            }

            var result = (from machine in Table.CreateQuery<MachineEntity>()
                          where machine.PartitionKey == id1
                          where machine.RowKey == id2
                          select machine).FirstOrDefault();

            return new MachineEntity()
            {
                Guid = result.Guid,
                MachineName = result.MachineName,
                HostName = result.HostName,
                MacAddress = result.MacAddress,
                State = result.State,
                ShouldWakeup = result.ShouldWakeup,
                Username = result.Username,
            };
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            InitializeTable();
        }

        // PUT api/values/5
        public async Task<MachineEntity> Put(string id1, string id2, [FromBody]MachineEntity value)
        {
            if(value == null)
            {
                return new MachineEntity
                {
                    MachineName = "Error: while deserializing the body"
                };
            }

            InitializeTable();

            MachineEntity machineEntity = (from machine in this.Table.CreateQuery<MachineEntity>()
                                 where machine.PartitionKey == id1
                                 where machine.RowKey == id2
                                 select machine).FirstOrDefault();

            if (machineEntity == null)
            {
                machineEntity = new MachineEntity
                {
                    Username = id1,
                    MachineName = id2,
                    HostName = value.HostName,
                    Guid = Guid.NewGuid().ToString(),
                    PartitionKey = id1,
                    RowKey = id2,
                    State = MachineState.Unknown.ToString(),
                    ShouldWakeup = false,
                    Timestamp = DateTime.UtcNow,
                };
            }
            else
            {
                if(value.State != null)
                {
                    machineEntity.State = value.State;
                }
                if (!string.IsNullOrWhiteSpace(value.MacAddress))
                {
                    machineEntity.MacAddress = value.MacAddress;
                }
                if(value.ShouldWakeup != null)
                {
                    machineEntity.ShouldWakeup = value.ShouldWakeup;
                }
            }

            await AzureTableHelper.InsertOrMergeEntityAsync(this.Table, machineEntity);
            return machineEntity;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            InitializeTable();
        }

    }
}
