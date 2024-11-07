using DeliverryPizza.Business.Interfaces;
using DeliverryPizza.Model;
using DeliverryPizza.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverryPizza.Business
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IRabbitMQService _rabbitMQService;

        public PedidoService(IPedidoRepository pedidoRepository, IRabbitMQService rabbitMQService)
        {
            _pedidoRepository = pedidoRepository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            try
            {
                return await _pedidoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar todos os pedidos: " + ex.Message);
                return new List<Pedido>();
            }
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            try
            {
                return await _pedidoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar pedido por id: " + ex.Message);
                return null;
            }
        }


        public async Task AddAsync(Pedido pedido)
        {
            try
            {
                await _pedidoRepository.AddAsync(pedido);
                _rabbitMQService.EnviarMensagemPedido($"Novo pedido criado: {pedido.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar pedido: " + ex.Message);
            }
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            try
            {
                await _pedidoRepository.UpdateAsync(pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar pedido: " + ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _pedidoRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir pedido: " + ex.Message);
            }
        }
    }
}
