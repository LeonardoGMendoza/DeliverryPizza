using DeliverryPizza.Business;
using DeliverryPizza.Business.Interfaces;
using DeliverryPizza.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverryPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        private readonly IRabbitMQService _rabbitMQService; // Adicionando a dependência do RabbitMQ

        // Modificando o construtor para injetar o IRabbitMQService
        public PedidoController(PedidoService pedidoService, IRabbitMQService rabbitMQService)
        {
            _pedidoService = pedidoService;
            _rabbitMQService = rabbitMQService; // Atribuindo o serviço injetado
        }

        // Método para buscar todos os pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        // Método para buscar um pedido por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            return pedido == null ? NotFound() : Ok(pedido);
        }

        // Método para adicionar um novo pedido
        [HttpPost]
        public async Task<ActionResult> PostPedido(Pedido pedido)
        {
            // Adiciona o pedido no banco de dados
            await _pedidoService.AddAsync(pedido);

            // Envia uma mensagem para o RabbitMQ
            _rabbitMQService.EnviarMensagemPedido($"Novo pedido criado: {pedido.Id}");

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        // Método para atualizar um pedido
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
                return BadRequest();

            await _pedidoService.UpdateAsync(pedido);
            return NoContent();
        }

        // Método para excluir um pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            await _pedidoService.DeleteAsync(id);
            return NoContent();
        }
    }
}



//using DeliverryPizza.Business;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace DeliverryPizza.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PedidoController : ControllerBase
//    {
//        private readonly PedidoService _pedidoService;

//        public PedidoController(PedidoService pedidoService)
//        {
//            _pedidoService = pedidoService;
//        }

//        // Método para buscar todos os pedidos
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
//        {
//            var pedidos = await _pedidoService.GetAll();
//            return Ok(pedidos);
//        }

//        // Método para buscar um pedido por id
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Pedido>> GetPedido(int id)
//        {
//            var pedido = await _pedidoService.GetById(id);
//            return pedido == null ? NotFound() : Ok(pedido);
//        }

//        // Método para adicionar um novo pedido
//        [HttpPost]
//        public async Task<ActionResult> PostPedido(Pedido pedido)
//        {
//            await _pedidoService.Add(pedido);
//            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
//        }

//        // Método para atualizar um pedido
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
//        {
//            if (id != pedido.Id)
//                return BadRequest();

//            await _pedidoService.Update(pedido);
//            return NoContent();
//        }

//        // Método para excluir um pedido
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePedido(int id)
//        {
//            await _pedidoService.Delete(id);
//            return NoContent();
//        }
//    }
//}



//using DeliverryPizza.Business;
//using DeliverryPizza.Model;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace DeliverryPizza.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PedidoController : ControllerBase
//    {
//        private readonly PedidoService _pedidoService;

//        public PedidoController(PedidoService pedidoService)
//        {
//            _pedidoService = pedidoService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
//        {
//            return Ok(await _pedidoService.GetAll());
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Pedido>> GetPedido(int id)
//        {
//            var pedido = await _pedidoService.GetById(id);
//            return pedido == null ? NotFound() : Ok(pedido);
//        }

//        [HttpPost]
//        public async Task<ActionResult> PostPedido(Pedido pedido)
//        {
//            await _pedidoService.Add(pedido);
//            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
//        {
//            if (id != pedido.Id)
//                return BadRequest();

//            await _pedidoService.Update(pedido);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePedido(int id)
//        {
//            await _pedidoService.Delete(id);
//            return NoContent();
//        }
//    }
//}
