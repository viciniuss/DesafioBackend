using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace DesafioBackend.Infrastructure.Messaging
{
    public class KafkaConsumer
    {
        private readonly string _bootstrapServers;
        private readonly string _topicName;
        private readonly string _groupId;

        public KafkaConsumer(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topicName = configuration["Kafka:Topic"];
            _groupId = configuration["Kafka:GroupId"];
        }

        public void ConsumeMessages()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_topicName);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            Console.WriteLine($"Mensagem recebida: {consumeResult.Message.Value}");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Erro ao consumir mensagem: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Canceled
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
