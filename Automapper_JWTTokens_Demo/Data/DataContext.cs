using Automapper_JWTTokens_Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automapper_JWTTokens_Demo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
