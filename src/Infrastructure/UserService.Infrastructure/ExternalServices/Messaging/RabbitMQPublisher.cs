using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.Application.Abstractions.Messaging;
public class RabbitMQPublisher : IEventPublisher
{
    private readonly ConnectionFactory _factory;

    public RabbitMQPublisher()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };
    }

    public Task PublishAsync<T>(T @event) where T : class
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        var queueName = typeof(T).Name;
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

        var json = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        return Task.CompletedTask;
    }
}
