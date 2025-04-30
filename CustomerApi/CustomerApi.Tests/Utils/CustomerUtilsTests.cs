using CustomerApi.Application.Common.Utils;
using CustomerApi.Domain.Entities;
using Xunit;

namespace CustomerApi.Tests.Utils
{
    public class CustomerUtilsTests
    {
            [Fact]
        public void InsertSorted_InsertsAtCorrectPosition()
        {
            var list = new List<Customer>
                {
                    new Customer { LastName = "A", FirstName = "Z" },
                    new Customer { LastName = "C", FirstName = "A" }
                };
            var toInsert = new Customer { LastName = "B", FirstName = "M" };

            CustomerUtils.BinarySearchForInsertPosition(list, toInsert);

            Assert.Equal(new[] { "A Z", "B M", "C A" }, list.Select(c => c.LastName + " " + c.FirstName));
        }

        [Fact]
        public void InsertSorted_InsertsAllCorrectly()
        {
            var existing = new List<Customer>
                {
                    new Customer { LastName = "A", FirstName = "A" }
                };
            var toInsert = new[] {
                    new Customer { LastName = "C", FirstName = "A" },
                    new Customer { LastName = "B", FirstName = "A" }
                };

            CustomerUtils.InsertSorted(existing, toInsert);

            Assert.Equal(new[] { "A A", "B A", "C A" }, existing.Select(c => c.LastName + " " + c.FirstName));
        }
    }
}
