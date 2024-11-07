using RabbitMQ.Client;
using System.Text;

namespace DeliverryPizza.Business.Messaging
{
    public class RabbitMQClient
    {
        public void PublishMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            // Criação da conexão e canal
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Declaração da fila com a opção durable
            channel.QueueDeclare(queue: "pedidoQueue",
                                 durable: true,      // A fila será preservada após o reinício
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);



            var body = Encoding.UTF8.GetBytes(message);

            // Envio da mensagem para a fila
            channel.BasicPublish(exchange: "",
                                 routingKey: "pedidoQueue",  // Fila de destino
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine("Mensagem publicada: " + message);
        }
    }
}
