using DenominationRoutine.Models;

namespace DenominationRoutine.Services
{
    public interface IPayoutCalculator
    {
        IEnumerable<PayoutCombination> Calculate(int amount);
    }
}
