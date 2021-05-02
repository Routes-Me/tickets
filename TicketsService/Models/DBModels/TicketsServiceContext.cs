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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
