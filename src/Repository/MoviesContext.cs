using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Repository
{
	public partial class MoviesContext : DbContext
	{
		public MoviesContext()
		{
		}

		public MoviesContext(DbContextOptions<MoviesContext> options)
				: base(options)
		{
		}

		public virtual DbSet<Movie> Movies { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Movies");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

			modelBuilder.Entity<Movie>(entity =>
			{
				entity.ToTable("Movie");

				entity.Property(e => e.ReleaseDate).HasColumnType("date");

				entity.Property(e => e.Title)
									.IsRequired()
									.HasMaxLength(200);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
