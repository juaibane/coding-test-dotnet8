using DenominationRoutine.Models;
using DenominationRoutine.Services;

class Program
{
    static void Main(string[] args)
    {
        // Abailable denominations
        var denominations = new List<Denomination>
            {
                new Denomination(100),
                new Denomination(50),
                new Denomination(10)
            };

        // Amounts to process
        var payouts = new[] { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

        IPayoutCalculator calculator = new CombinationFinder(denominations);

        foreach (var amount in payouts)
        {
            Console.WriteLine($"Payout combinations for {amount} EUR:");
            var combos = calculator.Calculate(amount);
            if (!combos.Any())
            {
                Console.WriteLine(" No combinations available.");
            }
            else
            {
                foreach (var combo in combos)
                {
                    Console.WriteLine($"  - {combo}");
                }
            }
            Console.WriteLine();
        }
    }
}