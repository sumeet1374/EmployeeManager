using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    /// <summary>
    ///  Employee DB Context class which is unit of work in this case
    /// </summary>
    public class EmployeeContext : DbContext, IUnitOfWork
    {
        private readonly string _connectionString;
        public EmployeeContext(string connectionString)
        {
            _connectionString = connectionString;

        }
        /// <summary>
        ///  Fluent API Mapping
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEmployeeMapping();
            modelBuilder.AddDocumentMapping();
            modelBuilder.AddDocumentEmployeeMapping();
            modelBuilder.AddAddressMapping();
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
          //  optionsBuilder.UseLazyLoadingProxies();

        }
        public void Commit()
        {
            SaveChanges();
        }

        public T GetContext<T>() where T : class
        {
            return (this as T);
        }
    }
}
