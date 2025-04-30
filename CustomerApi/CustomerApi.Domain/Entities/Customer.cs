namespace CustomerApi.Domain.Entities
{
    public class Customer
    {
        public int Order { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
