using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DeliverryPizza.Business
{
    public class PedidoConsumer
    {
        private readonly string _hostName = "localhost";

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "pedidoQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Mensagem recebida: {mensagem}");
                    ProcessarPedido(mensagem);
                };

                channel.BasicConsume(queue: "pedidoQueue", autoAck: true, consumer: consumer);
                Console.WriteLine("Aguardando mensagens. Pressione [enter] para sair.");
                Console.ReadLine();
            }
        }

        private void ProcessarPedido(string mensagem)
        {
            Console.WriteLine($"Pedido processado: {mensagem}");
        }
    }
}
