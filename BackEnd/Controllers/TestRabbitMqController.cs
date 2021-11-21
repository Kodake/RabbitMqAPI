using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRabbitMqController : Controller
    {
        private readonly AmqpService amqpService;

        public TestRabbitMqController(AmqpService amqpService)
        {
            this.amqpService = amqpService ?? throw new ArgumentNullException(nameof(amqpService));
        }

        [HttpPost("")]
        public IActionResult PublishMessage([FromBody] object message)
        {
            amqpService.PublishMessage(message);
            return Ok();
        }
    }
}
