using CustomerApi.Domain.Entities;
using CustomerApi.Infrastructure.Persistence.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Infrastructure.Persistence
{
    public class SQLiteCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public SQLiteCustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }   
        
        public async Task ReplaceData(IEnumerable<Customer> customers)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Customers.ExecuteDeleteAsync();
                await _context.BulkInsertAsync(
                    customers.ToList(),
                    new BulkConfig
                    {
                        SetOutputIdentity = false,
                        BatchSize = 4000 
                    });
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
