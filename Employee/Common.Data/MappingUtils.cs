using Common.Model;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    /// <summary>
    ///  Class having extension methods to map various entities
    /// </summary>
    public static class MappingUtils
    {
        /// <summary>
        ///  Map Employee Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddEmployeeMapping(this ModelBuilder modelBuilder)
        {
            // Mapping - Employee
            modelBuilder.Entity<Employee>().ToTable(TableConstants.EMPLOYEE).HasKey(x => x.Id);
            modelBuilder.Entity<Employee>().HasOne(x => x.PermanentAddress).WithOne().HasForeignKey<PermanentAddress>(x => x.EmployeeId);
            modelBuilder.Entity<Employee>().HasOne(x => x.CurrentAddress).WithOne().HasForeignKey<CurrentAddress>(x => x.EmployeeId);
            modelBuilder.Entity<Employee>().HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId);
        }

        /// <summary>
        ///  Map Document Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddDocumentMapping(this ModelBuilder modelBuilder)
        {
            // Mapping - Document
            modelBuilder.Entity<Document>().ToTable(TableConstants.DOCUMENT);
            modelBuilder.Entity<Document>().HasKey(x => x.Id);
        }

        /// <summary>
        ///  Map DocumentEmployee Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddDocumentEmployeeMapping(this ModelBuilder modelBuilder)
        {
            // Mapping - DocumentEmployee (Many to Many table representation)
            modelBuilder.Entity<DocumentEmployee>().ToTable(TableConstants.DOCUMENTEMPLOYEE);
            modelBuilder.Entity<DocumentEmployee>().HasKey(x => x.Id);
            modelBuilder.Entity<DocumentEmployee>().HasOne(x => x.Employee).WithMany(y => y.AssociatedDocuments).HasForeignKey(x => x.EmployeeId);
            modelBuilder.Entity<DocumentEmployee>().HasOne(x => x.Document).WithMany().HasForeignKey(x => x.DocumentId);
        }
        /// <summary>
        /// 
        ///   Map Address Entity
        ///   
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddAddressMapping(this ModelBuilder modelBuilder)
        {
            // Mapping -Address Table per hirarchy inheritance
            modelBuilder.Entity<Address>().ToTable(TableConstants.ADDRESS);
            modelBuilder.Entity<Address>().HasKey(x => x.Id);
            modelBuilder.Entity<Address>().HasDiscriminator<string>(TableConstants.ADDRESS_TYPE)
                                          .HasValue<PermanentAddress>(TableConstants.PERMANENT_ADDRES)
                                          .HasValue<CurrentAddress>(TableConstants.CURRENT_ADDRESS);
        }
    }
}
