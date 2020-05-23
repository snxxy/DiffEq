using Microsoft.EntityFrameworkCore;

namespace EquationDB
{
    public partial class Context : DbContext
    {
        public Context()
        {
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
                optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\GeneratorProj\DiffEq\EquationDB\Eqs.mdf; Integrated Security = True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Equation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Eq).HasMaxLength(500);

                entity.Property(e => e.Latex).HasMaxLength(500);

                entity.Property(e => e.Solution).HasMaxLength(500);

                entity.Property(e => e.SolutionLatex).HasMaxLength(500);

                entity.Property(e => e.Type).ValueGeneratedNever();
            });
        }
    }
}
