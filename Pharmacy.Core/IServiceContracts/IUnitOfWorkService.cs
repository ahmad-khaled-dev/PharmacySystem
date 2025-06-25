using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;




namespace Pharmacy.Core.IServiceContracts
{
    public interface IUnitOfWorkService
    {
         Task  BeginTransactionAsync();

        Task CommitTransactionAsync();
        
        Task RollbackTransactionAsync();

        Task<int> SaveChangesAsync();

        IExecutionStrategy CreateExecutionStrategy();
    }

}
