using DeliverryPizza.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverryPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitMQController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        // Endpoint para iniciar o consumidor RabbitMQ
        [HttpPost("start-consumer")]
        public IActionResult StartConsumer()
        {
            try
            {
                // Inicia o consumidor RabbitMQ
                _rabbitMQService.StartConsumer();
                return Ok("Consumidor iniciado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao iniciar o consumidor: {ex.Message}");
            }
        }

        // Endpoint para parar o consumidor RabbitMQ (opcional)
        [HttpPost("stop-consumer")]
        public IActionResult StopConsumer()
        {
            try
            {
                // Para o consumidor RabbitMQ
                _rabbitMQService.StopConsumer();
                return Ok("Consumidor parado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao parar o consumidor: {ex.Message}");
            }
        }
    }
}

