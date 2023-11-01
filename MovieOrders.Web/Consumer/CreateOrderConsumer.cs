using Confluent.Kafka;

namespace MovieOrders.Web.Consumer
{
    public class CreateOrderConsumer : BackgroundService
    {
        private const string BootstrapServers = "localhost:9092";
            private const string Topic = "create_movie_order";

        private readonly IConsumer<Ignore, string> _consumer;

        public CreateOrderConsumer()
        {
            var config = new ConsumerConfig 
            {
                BootstrapServers = BootstrapServers,
                GroupId = "consumer-group-id",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe(Topic);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(stoppingToken);

                        if (consumeResult != null)
                        {
                            // Process the received message as needed
                            Console.WriteLine($"Received message: {consumeResult.Message.Value} at partition {consumeResult.Partition}, offset {consumeResult.Offset}");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Log or handle the cancellation if needed
                    }
                    catch (ConsumeException e)
                    {
                        // Handle exceptions occurred during consumption
                        Console.WriteLine($"Error occurred: {e.Error.Reason}");
                    }
                }
            }
            finally
            {
                _consumer.Close();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
