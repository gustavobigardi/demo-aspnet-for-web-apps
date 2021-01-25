using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using demo_aspnet_for_web_apps.Models;
using demo_aspnet_for_web_apps.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace demo_aspnet_for_web_apps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StressController : ControllerBase
    {
        private readonly SimpleDataRepository _repository;

        public StressController(SimpleDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("cpu")]
        public IActionResult StressCpu()
        {
            _repository.SetCpuStop(false);

            int percentage = 80;
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                (new Thread(() =>
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    while (true)
                    {

                        if (watch.ElapsedMilliseconds > percentage)
                        {
                            Thread.Sleep(100 - percentage);
                            watch.Reset();
                            watch.Start();
                        }

                        if (_repository.GetCpuStop())
                        {
                            break;
                        }
                    }
                })).Start();
            }
            return Ok();
        }

        [HttpGet("memory")]
        public IActionResult StressMem()
        {
            for (int i = 0; i < 500000; i++)
            {
                _repository.AddData(new SampleData() { Data = Guid.NewGuid().ToString() });
            }

            return Ok(new { Total = _repository.Count() });
        }

        [HttpGet("stop")]
        public IActionResult Stop()
        {
            _repository.SetCpuStop(true);
            return Ok();
        }
    }
}