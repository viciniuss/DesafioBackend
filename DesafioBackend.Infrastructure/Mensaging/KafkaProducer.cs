using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DesafioBackend.Infrastructure.Messaging
{
    public class KafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topicName;

        public KafkaProducer(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topicName = configuration["Kafka:Topic"];
        }

        public async Task SendMessageAsync(string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var deliveryResult = await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Mensagem enviada: {deliveryResult.Value}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Erro ao enviar mensagem: {e.Message}");
                }
            }
        }
    }
}
