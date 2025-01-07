using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;

namespace WebApp.Domain.Interface
{
    public interface IDBContext
    {
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        public DbSet<Product> Product { get; set; }
    }

    public interface DbSetA<T>
        where T : class, new()
    {

    }
}
