using DeliverryPizza.Business.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DeliverryPizza.Business.RabbitMQClient
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private IModel _channel;
        private IConnection _connection;

        public RabbitMQService(string hostName, string queueName)
        {
            _hostName = hostName;
            _queueName = queueName;
        }

        // Implementação do método para enviar a mensagem para a fila
        public void EnviarMensagemPedido(string mensagem)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Envia a mensagem para a fila
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: Encoding.UTF8.GetBytes(mensagem));
                Console.WriteLine($"Mensagem enviada: {mensagem}");
            }
        }

        // Implementação do método para consumir mensagens da fila
        public void ConsumirMensagens()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declara a fila para o consumidor
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mensagem recebida: {mensagem}");
                // Aqui você pode adicionar a lógica de processamento da mensagem
            };

            // Inicia o consumo da fila
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            Console.WriteLine("Consumidor iniciado...");
        }

        // Implementação do método para criar a fila no RabbitMQ
        public void CriarFila()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declara a fila
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                Console.WriteLine($"Fila '{_queueName}' criada com sucesso.");
            }
        }

        // Método para iniciar o consumidor
        public void StartConsumer()
        {
            if (_channel == null)
            {
                ConsumirMensagens();
            }
            else
            {
                Console.WriteLine("O consumidor já está em execução.");
            }
        }

        // Método para parar o consumidor
        public void StopConsumer()
        {
            if (_channel != null)
            {
                _channel.Close();
                _connection.Close();
                _channel = null;
                _connection = null;
                Console.WriteLine("Consumidor parado.");
            }
            else
            {
                Console.WriteLine("Nenhum consumidor ativo para parar.");
            }
        }
    }
}
