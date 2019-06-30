using MassTransit;
using MTDecoupling.Framework.Contract;
using System;

namespace MTProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var bus = Bus.Factory.CreateUsingRabbitMq(c =>
            {
                c.Host("localhost", "/", f =>
                {
                    f.Username("guest");
                    f.Password("guest");
                });
            });

            bus.Start();

            bus.Publish(new Email { Name = "kranthi" });

            Console.ReadKey();

            bus.Stop();
        }
    }
}
