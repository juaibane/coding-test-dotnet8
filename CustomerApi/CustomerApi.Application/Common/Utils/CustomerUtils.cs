using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Common.Utils
{
    public static class CustomerUtils
    {
        public static void BinarySearchForInsertPosition(List<Customer> existingSortedList, Customer customerToInsert)
        {
            int startIndex = 0;
            int endIndex = existingSortedList.Count - 1;

            while (startIndex <= endIndex)
            {
                int middleIndex = startIndex + ((endIndex - startIndex) >> 1);

                // Compare by last name first
                int lastNameComparison = string.Compare(
                    existingSortedList[middleIndex].LastName,
                    customerToInsert.LastName,
                    StringComparison.OrdinalIgnoreCase);

                int comparisonResult;
                if (lastNameComparison == 0)
                {
                    // If last names match, compare by first name
                    comparisonResult = string.Compare(
                        existingSortedList[middleIndex].FirstName,
                        customerToInsert.FirstName,
                        StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    comparisonResult = lastNameComparison;
                }

                if (comparisonResult < 0)
                {
                    // The middle element comes before the one to insert
                    startIndex = middleIndex + 1;
                }
                else
                {
                    // The middle element comes after or equals the one to insert
                    endIndex = middleIndex - 1;
                }
            }

            // startIndex is the correct insertion position
            existingSortedList.Insert(startIndex, customerToInsert);
        }

        public static void InsertSorted(List<Customer> existing, IEnumerable<Customer> toInsert)
        {
            foreach (var customer in toInsert)
                BinarySearchForInsertPosition(existing, customer);
        }

        public static void ResetOrder(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customer.Order = 0; 
            }
        }
    }
}
