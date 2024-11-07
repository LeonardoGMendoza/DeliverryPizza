using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverryPizza.Model;

namespace DeliverryPizza.Repository.Interface
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllAsync();          // Método para obter todos os pedidos
        Task<Pedido?> GetByIdAsync(int id);               // Método para obter um pedido por ID
        Task AddAsync(Pedido pedido);                     // Método para adicionar um novo pedido
        Task UpdateAsync(Pedido pedido);                  // Método para atualizar um pedido existente
        Task DeleteAsync(int id);                         // Método para excluir um pedido por ID
    }
}
