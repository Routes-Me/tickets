using Microsoft.EntityFrameworkCore;

namespace TicketsService.Models.DBModels
{
    public partial class TicketsServiceContext : DbContext
    {
        public TicketsServiceContext()
        {
        }

        public TicketsServiceContext(DbContextOptions<TicketsServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.HasKey(e => e.TicketId).HasName("PRIMARY");

                entity.ToTable("tickets");

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("double");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.Validity)
                    .HasColumnName("validity")
                    .HasColumnType("smallint");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Currencies>(entity =>
            {
                entity.HasKey(e => e.CurrencyId).HasName("PRIMARY");

                entity.ToTable("currencies");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("char(3)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol")
                    .HasColumnType("nvarchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
