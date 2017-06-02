using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WakeYourPcWebApp.Models;
using WakeYourPcWebApp.ViewModels;

namespace WakeYourPcWebApp.Controllers.Api.v1
{
    [Route("api/v1/users/{username}/")]
    public class MachinesController : Controller
    {
        private readonly IConfigurationRoot _config;
        private readonly IWakeupRepository _repository;
        private readonly ILogger<MachinesController> _logger;

        public MachinesController(IConfigurationRoot config, IWakeupRepository repository,
            ILogger<MachinesController> logger)
        {
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("machines")]
        public IActionResult Machines(string username)
        {
            try
            {
                var machines = _repository.GetAllMachines(username);
                var machinesViewModel = Mapper.Map<IEnumerable<MachineViewModel>>(machines);
                return Ok(machinesViewModel);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured while getting list of machines : {exception}");
                return BadRequest("Error occured while getting list of machines");
            }
        }

        [HttpGet("wakeup")]
        public IActionResult Wakeup(string username)
        {
            try
            {
                var machines = _repository.GetMachinesToWakeup(username);
                var machinesViewModel = Mapper.Map<IEnumerable<MachineViewModel>>(machines);
                return Ok(machinesViewModel);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured while getting wakeup machines : {exception}");
                return BadRequest("Error occured while getting wakeup machines");
            }
        }

        [HttpPost("machines")]
        public async Task<IActionResult> AddMachine([FromBody] MachineViewModel machineViewModel, string username)
        {
            if (ModelState.IsValid)
            {
                var machine = Mapper.Map<Machine>(machineViewModel);
                _repository.AddMachine(machine, username);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/v1/machines/{machineViewModel.MachineName}",
                        Mapper.Map<MachineViewModel>(machine));
                }
                _logger.LogError("Error occured while adding a machine");
            }
            return BadRequest("Failed to save the Machine to database");
        }

    }
}
