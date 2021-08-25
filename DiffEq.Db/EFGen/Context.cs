using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DiffEq.DB
{
    public partial class Context : DbContext
    {
        public Context()
        {
            Database.EnsureCreated();
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Equation> Equation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cs = AppDomain.CurrentDomain.BaseDirectory;
                optionsBuilder.UseSqlServer(@$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={cs}Eqs.mdf;Integrated Security=True");
                //optionsBuilder.UseLoggerFactory(DBLoggerFactory);
            }
        }
        public static readonly ILoggerFactory DBLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new DBLoggerProvider());
        });
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Equation>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Eq).IsRequired();

                entity.Property(e => e.Latex).IsRequired();

                entity.Property(e => e.Solution).IsRequired();

                entity.Property(e => e.SolutionLatex).IsRequired();

                entity.Property(e => e.Type).IsRequired();
            });
        }
    }
}
