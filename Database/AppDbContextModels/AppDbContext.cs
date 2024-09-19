using Microsoft.EntityFrameworkCore;

namespace Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<TblTest> TblTests { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.UseCollation("utf8mb4_0900_ai_ci")
			.HasCharSet("utf8mb4");

		modelBuilder.Entity<TblTest>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("tbl_test");

			entity.Property(e => e.Id).ValueGeneratedNever();
			entity.Property(e => e.CreatedDate).HasColumnType("datetime");
			entity.Property(e => e.Description).HasMaxLength(45);
			entity.Property(e => e.Name).HasMaxLength(45);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
