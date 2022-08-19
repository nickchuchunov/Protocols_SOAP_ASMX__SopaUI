using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicService.Data
{
    public class SampleServiceDbContext: DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Consultation> Consultations { get; set; }
                
        public SampleServiceDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
