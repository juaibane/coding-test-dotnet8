using DenominationRoutine.Models;

namespace DenominationRoutine.Services
{
    public class CombinationFinder(IEnumerable<Denomination> denominations) : IPayoutCalculator
    {
        // Ordered to generate combinations with larger bills first 
        private readonly List<Denomination> _denominations= denominations.OrderByDescending(d => d.Value).ToList();

        public IEnumerable<PayoutCombination> Calculate(int amount)
        {
            var result = new List<PayoutCombination>();
            CalculateRecursive(amount, 0, new Dictionary<Denomination, int>(), result);
            return result;
        }

        private void CalculateRecursive(int remaining, int index,
            Dictionary<Denomination, int> current, List<PayoutCombination> result)
        {
            // If we have processed all denominations
            if (index >= _denominations.Count)
            {
                if (remaining == 0)
                {
                    // Add a copy of the current combination
                    result.Add(new PayoutCombination(
                        current.ToDictionary(entry => entry.Key, entry => entry.Value)
                    ));
                }
                return;
            }

            var denom = _denominations[index];
            int maxCount = remaining / denom.Value;

            // Try from the maximum number of bills possible to 0
            for (int count = maxCount; count >= 0; count--)
            {
                current[denom] = count;
                CalculateRecursive(remaining - count * denom.Value, index + 1, current, result);
            }
            current.Remove(denom);
        }
    }

}
