namespace DenominationRoutine.Models
{
    public record PayoutCombination(Dictionary<Denomination, int> Counts)
    {
        public override string ToString()
        {
            // Combination formater "1 x 50 EUR + 5 x 10 EUR"
            return string.Join(" + ", Counts
                .Where(keyValue => keyValue.Value > 0)
                .Select(keyValue => $"{keyValue.Value} x {keyValue.Key.Value} EUR"));
        }
    }
}
