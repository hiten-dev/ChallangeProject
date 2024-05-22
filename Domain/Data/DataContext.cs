using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeApplication.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }
        public DbSet<Customer> customers { get; set; }
    }
}
