using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DeliverryPizza.Business.RabbitMQClient
{
    public class RabbitMQConsumer
    {
        private readonly string _hostName = "localhost"; // Nome do host do RabbitMQ

        public void ConsumirMensagens()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            // Criação da conexão e canal
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Consumindo mensagens da fila "pedidoQueue"
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Processamento da mensagem
                    Console.WriteLine("Mensagem recebida: " + message);

                    // Acknowledge manual, para garantir que a mensagem não será perdida
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao processar a mensagem: " + ex.Message);
                    // Em um cenário real, seria interessante reprocessar ou colocar em uma fila de mensagens de erro.
                }
            };

            // Inicia o consumo da fila
            channel.BasicConsume(
                queue: "pedidoQueue",  // Nome da fila
                autoAck: false,         // Disable automatic acknowledgment
                consumer: consumer);

            Console.WriteLine("Pressione [enter] para sair.");
            Console.ReadLine();
        }
    }
}
