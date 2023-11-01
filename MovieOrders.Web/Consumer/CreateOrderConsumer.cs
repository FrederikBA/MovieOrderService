using System.Text.Json;
using Confluent.Kafka;
using MovieOrders.Web.Interface.DomainServices;
using MovieOrders.Web.Model.Dto;

namespace MovieOrders.Web.Consumer
{
    public class CreateOrderConsumer : BackgroundService
    {
        private const string BootstrapServers = "localhost:9092";
        private const string GroupId = "movie_orders";
        private const string Topic = "create_movie_order";

        private readonly IServiceProvider _serviceProvider;

        public CreateOrderConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield(); //Find out what this does, cannot compile without

            var config = new ConsumerConfig
            {
                GroupId = GroupId,
                BootstrapServers = BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest, // Always start at the beginning of the topic
                AllowAutoCreateTopics = true
            };

            using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                var cancelToken = new CancellationTokenSource();

                //Subscribe to topic
                consumerBuilder.Subscribe(Topic);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var consumeResult = consumerBuilder.Consume(cancelToken.Token);
                        var jsonObj = consumeResult.Message.Value;

                        using var scope = _serviceProvider.CreateScope();
                        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                        var movies = JsonSerializer.Deserialize<List<MovieDto>>(jsonObj);

                        if (movies != null)
                        {
                            await orderService.CreateOrderAsync(movies);
                        }
                    }
                }
                catch (Exception e)
                {
                    consumerBuilder.Close();
                }
            }
        }
    }
}