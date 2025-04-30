using Simulator.Core.Interfaces;
using Simulator.Core.Models;

namespace Simulator.Infrastructure.Services
{
    public class CustomerGenerator : ICustomerGenerator
    {
        private static readonly string[] FirstNames = { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
        private static readonly string[] LastNames = { "Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane" };
        private readonly Random _random = new();

        public IEnumerable<CustomerDto> GenerateCustomers(int customersToGenerate, int startId)
        {
            for (int i = 0; i < customersToGenerate; i++)
            {
                yield return new CustomerDto
                {
                    Id = startId + i,
                    FirstName = FirstNames[_random.Next(FirstNames.Length)],
                    LastName = LastNames[_random.Next(LastNames.Length)],
                    Age = _random.Next(10, 91)
                };
            }
        }
    }
}
