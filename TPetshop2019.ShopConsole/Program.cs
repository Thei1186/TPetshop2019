using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.ApplicationServices.Services;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Infrastructure.Data;
using TPetshop2019.Infrastructure.Data.Repositories;

namespace TPetshop2019.ShopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            FakeDB.InitData();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IOwnerRepository, OwnerRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IOwnerService, OwnerService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var petService = serviceProvider.GetRequiredService<IPetService>();
            var ownerService = serviceProvider.GetRequiredService<IOwnerService>();
            Printer petPrinter = new Printer(petService, ownerService);
        }
    }
}
