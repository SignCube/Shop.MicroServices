using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {

        
        public OrderContext CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=OrderDb;Trusted_Connection=True;");

            return new OrderContext(optionsBuilder.Options);
        }
    }
}
