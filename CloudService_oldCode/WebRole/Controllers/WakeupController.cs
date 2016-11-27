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
    public class WakeupController : ApiController
    {
        private string MachineTableName = "Machine";

        private CloudTable MachineTable;

        // GET api/values
        public IEnumerable<MachineEntity> Get(string id1)
        {
            InitializeTable();

            var query = (from machine in MachineTable.CreateQuery<MachineEntity>()
                         where machine.PartitionKey == id1
                         where machine.ShouldWakeup == true
                         select machine).Take(200);

            var machines = query.ToList();

            return machines.Select(x => new MachineEntity()
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
            if (this.MachineTable == null)
            {
                this.MachineTable = AzureTableHelper.GetTableReferenceAsync(MachineTableName);
            }
        }

        // GET api/values/5
        public MachineEntity Get(string id1, string id2)
        {
            InitializeTable();

            var machineName = (from machine in MachineTable.CreateQuery<MachineEntity>()
                               where machine.PartitionKey == id1
                               where machine.RowKey == id2
                               where machine.ShouldWakeup == true
                               select machine).FirstOrDefault();
            if (machineName == null)
            {
                return new MachineEntity()
                {
                    MachineName = "Error: this machine does not belong to the user"
                };
            }

            var result = (from machine in MachineTable.CreateQuery<MachineEntity>()
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

        // DELETE api/values/5
        public void Delete(int id)
        {
            InitializeTable();
        }

    }
}
