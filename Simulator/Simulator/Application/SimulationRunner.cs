using Microsoft.Extensions.Configuration;
using Simulator.Core.Interfaces;
using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Simulator.Application
{
    public class SimulationRunner(ICustomerGenerator customerGenerator, ICustomerApiClient apiClient, IConfiguration configuration)
    {
        private readonly ICustomerGenerator _customerGenerator = customerGenerator;
        private readonly ICustomerApiClient _apiClient = apiClient;
        private readonly IConfiguration _configuration = configuration;
        private int _currentId = 1;
        private readonly Random _random = new();

        public async Task RunAsync()
        {
            int parallelTasks = _configuration.GetValue<int?>("Simulation:ParallelTasks") ?? 10;
            var maxCustomersPerRequest = _configuration.GetValue<int?>("Simulation:MaxCustomersPerPostRequest") ?? 6;

            var tasks = new List<Task>();
            for (int i = 0; i < parallelTasks; i++)
            {
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            bool randomRequest = (_random.Next() & 1) == 0;
                            if (randomRequest)
                            {
                                // ## GET ##
                                Console.WriteLine($"[GET] Sending");
                                var swGet = Stopwatch.StartNew();
                                var fetchedCustomers = await _apiClient.GetCustomersAsync();
                                swGet.Stop();
                                Console.WriteLine($"[GET] Retrieved {fetchedCustomers.Count()} customers. in {swGet.ElapsedMilliseconds}ms");
                            }
                            else
                            {
                                // ## POST ##
                                var customersPerRequest = _random.Next(2, maxCustomersPerRequest + 1);
                                var customers = _customerGenerator.GenerateCustomers(customersPerRequest, GetNextIds(customersPerRequest)).ToList();
                                Console.WriteLine($"[POST] Sending {customers.Count} customers: IDs {string.Join(",", customers.Select(c => c.Id))}");
                                var swPost = Stopwatch.StartNew();
                                var response = await _apiClient.PostCustomersAsync(customers);
                                swPost.Stop();
                                if (!response.IsSuccessStatusCode)
                                {
                                    var body = await response.Content.ReadAsStringAsync();
                                    Console.WriteLine($"[POST-Error] status {response.StatusCode} in {swPost.ElapsedMilliseconds}ms: {body}");
                                }
                                else
                                {
                                    Console.WriteLine($"[POST] Sent {customers.Count} customers (IDs {string.Join(',', customers.Select(c => c.Id))}) in {swPost.ElapsedMilliseconds}ms");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[Error] {ex.Message}");
                        }
                    }));
            }
            await Task.WhenAll(tasks);
        }

        private int GetNextIds(int count)
        {
            lock (this)
            {
                int id = _currentId;
                _currentId += count;
                return id;
            }
        }
    }
}
