
using Microsoft.EntityFrameworkCore;

namespace Tienda.Domain.Entities;

public partial class TiendaDbContext : DbContext
{
    public TiendaDbContext()
    {
    }

    public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }
    
    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceHeader> InvoiceHeaders { get; set; }

    public virtual DbSet<InvoicePayment> InvoicePayments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carts__51BCD797F3C60ABF");

            entity.Property(e => e.Id).HasColumnName("CartID");
            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Carts) // Me queda la espina aquí de no poder hacer una relación 1 a 1
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK__Carts__UserID__47DBAE45");

            // Agregado para hacer si desaparece el error: (sin éxito)
            // SqlClient.SqlException (0x80131904): Invalid column name 'CartItemID1'. Invalid column name 'CartID1'.
            entity.HasMany(d => d.CartItems).WithOne(c => c.Cart)
                .HasForeignKey(c => c.CartID);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItems__51BCD797F3C60ABF");

            entity.Property(e => e.Id).HasColumnName("CartItemID");
            entity.Property(e => e.CartID).HasColumnName("CartID");
            entity.Property(e => e.ProductID).HasColumnName("ProductID");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartID)
                .HasConstraintName("FK__CartItems__CartI__4AB81AF0");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductID)
                .HasConstraintName("FK__CartItems__Produ__4BAC3F29");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__19093A2B7B1101C5");

            entity.Property(e => e.Id).HasColumnName("CategoryID");
            entity.Property(e => e.Name).HasMaxLength(100);

            // Configurando la relación uno a muchos
            entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(p => p.CategoryId);
        });
        // ============ Antes INVOICE ============================
        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F1578F1E8B7D58C");

            entity.Property(e => e.Id).HasColumnName("InvoiceDetailID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", false)
                .HasColumnType("decimal(29, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__InvoiceDe__Invoi__5629CD9C");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__InvoiceDe__Produ__571DF1D5");
        });

        modelBuilder.Entity<InvoiceHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceH__D796AAD5A3ABA6AE");

            entity.Property(e => e.Id).HasColumnName("InvoiceID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.InvoiceHeaders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__InvoiceHe__UserI__52593CB8");
        });

        modelBuilder.Entity<InvoicePayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceP__9B556A58ADF154C1");

            entity.Property(e => e.Id).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoicePayments)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__InvoicePa__Invoi__59FA5E80");
        });
    
        // ============ Antes INVOICE ============================

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__B40CC6ED1696CB06");

            entity.Property(e => e.Id).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__1788CCACD3EE9679");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053472C80FD5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);

            // RELACIÓN A: MANY INVOICES
            entity.HasMany(d => d.InvoiceHeaders).WithOne(p => p.User) // En Invoices también está configurada esta relación 
                .HasForeignKey(d => d.UserId)/*
                .HasConstraintName("FK__Users__InvoiceID__4316F928")*/;
            // RELACIÓN A: ONE? CART
            entity.HasMany(d => d.Carts).WithOne(p => p.User) // En Invoices también está configurada esta relación 
                .HasForeignKey(d => d.UserID)/*
                .HasConstraintName("FK__Users__CartID__4316F928")*/;
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
