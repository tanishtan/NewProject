using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject
{
    internal class BooksDataModel
    {
        //instance of books ( each book )
        [Table("Books")]
        
        public class Book
        {
            [Key]
            public int BookId { get; set; }
            [Required]
            public string Title { get; set; }
            public string ISBN { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public bool IsActive { get; set; }
            public DateTime PublicationDate { get; set; }
        }

        [Table("Authors")]
        [PrimaryKey("AuthorId")]
        [Index("FirstName", "LastName", AllDescending = false, IsUnique = true, Name = "IDX_Author_Names")]
        public class Author
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int AuthorId { get; set; }
            [StringLength(50)]
            [Required]
            public string FirstName { get; set; }
            [StringLength(50)]
            [Required]
            public string LastName { get; set; }
            [StringLength(50)]
            public string City { get; set; }
            [StringLength(50)]
            public string Country { get; set; }
            
        }

        public class BooksDbContext : DbContext
        {
            public DbSet<Book>Books { get; set; }
            public DbSet<Author>Authors { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(
                    @"server=(local);database=BooksDb;integrated security=sspi;trustservercertificate=true"
                    );
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //modelBuilder.Entity<Book>().HasKey(c => c.BookId);
                modelBuilder.Entity<Book>().HasIndex(c => c.ISBN);
                modelBuilder.Entity<Book>().HasData(
                     new Book { BookId = 1, Title = "EF Core", Author = "Eurofins", Price = 1000m, ISBN = "12345", IsActive = true, PublicationDate = new DateTime(2023, 12, 12) },
                     new Book { BookId = 2, Title = "EF Core Tools", Author = "Eurofins", Price = 1100m, ISBN = "1234598765", IsActive = false, PublicationDate = new DateTime(2023, 12, 12) }
                    );
            }
        }
    }
}
