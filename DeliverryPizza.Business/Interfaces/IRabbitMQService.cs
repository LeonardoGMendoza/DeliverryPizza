namespace DeliverryPizza.Business.Interfaces
{
    public interface IRabbitMQService
    {
        // Método para enviar uma mensagem para a fila do RabbitMQ
        void EnviarMensagemPedido(string mensagem);

        // Método para consumir mensagens da fila do RabbitMQ
        void ConsumirMensagens();

        // Método para criar uma fila no RabbitMQ
        void CriarFila();
        void StartConsumer();
        void StopConsumer();
    }
}
