using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EquationDB
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
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GeneratorProj\DiffEq\EquationDB\Eqs.mdf;Integrated Security=True");
                optionsBuilder.UseLoggerFactory(DBLoggerFactory);
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
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Eq);

                entity.Property(e => e.Latex);

                entity.Property(e => e.Solution);

                entity.Property(e => e.SolutionLatex);

                entity.Property(e => e.Type).ValueGeneratedNever();
            });
        }
    }
}
