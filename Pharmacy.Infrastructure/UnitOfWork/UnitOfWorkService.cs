using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pharmacy.Core.IServiceContracts;
using Pharmacy.Infrastructure.DbContext;
namespace Pharmacy.Core.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private  IDbContextTransaction _currentTransaction;



        public UnitOfWorkService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext; 
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await _applicationDbContext.Database.BeginTransactionAsync();  
        }

        public async Task CommitTransactionAsync()
        {
            await _applicationDbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _applicationDbContext.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _applicationDbContext.Database.CreateExecutionStrategy();
        }
    }
}
