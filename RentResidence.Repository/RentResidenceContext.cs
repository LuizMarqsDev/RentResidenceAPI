using Microsoft.EntityFrameworkCore;
using RentResidence.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentResidence.Repository
{
    public class RentResidenceContext : DbContext
    {
        public RentResidenceContext(DbContextOptions<RentResidenceContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Residence> Residences { get; set; }
        
    }
}
