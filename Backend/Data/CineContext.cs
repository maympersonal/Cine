using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public partial class CineContext : DbContext
{
    public CineContext()
    {
    }

    public CineContext(DbContextOptions<CineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Butaca> Butacas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<Efectivo> Efectivos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Punto> Puntos { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<Tarjetum> Tarjeta { get; set; }

    public virtual DbSet<Web> Webs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5F6O745\\SQLEXPRESS;Database=Cine;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.IdA);

            entity.ToTable("Actor");

            entity.Property(e => e.IdA).HasColumnName("idA");
            entity.Property(e => e.NombreA)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Butaca>(entity =>
        {
            entity.HasKey(e => e.IdB);

            entity.ToTable("Butaca");

            entity.Property(e => e.IdB).HasColumnName("idB");
            entity.Property(e => e.IdS).HasColumnName("idS");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Ci);

            entity.ToTable("Cliente");

            entity.Property(e => e.Ci)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => new { e.IdP, e.IdS, e.Fecha, e.Ci }).HasName("PKCompra");

            entity.ToTable("Compra");

            entity.Property(e => e.IdP).HasColumnName("idP");
            entity.Property(e => e.IdS).HasColumnName("idS");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Ci)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IdPg).HasColumnName("idPg");

            entity.HasOne(d => d.CiNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Ci)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compra__Ci__0C85DE4D");

            entity.HasOne(d => d.IdPgNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdPg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compra__idPg__0D7A0286");

            entity.HasOne(d => d.Sesion).WithMany(p => p.Compras)
                .HasForeignKey(d => new { d.IdP, d.IdS, d.Fecha })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compra__0E6E26BF");

            entity.HasMany(d => d.IdBs).WithMany(p => p.Compras)
                .UsingEntity<Dictionary<string, object>>(
                    "ButacasReservada",
                    r => r.HasOne<Butaca>().WithMany()
                        .HasForeignKey("IdB")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ButacasRese__idB__18EBB532"),
                    l => l.HasOne<Compra>().WithMany()
                        .HasForeignKey("IdP", "IdS", "Fecha", "Ci")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ButacasReservada__19DFD96B"),
                    j =>
                    {
                        j.HasKey("IdP", "IdS", "Fecha", "Ci", "IdB").HasName("PKReserva");
                        j.ToTable("ButacasReservadas");
                        j.IndexerProperty<int>("IdP").HasColumnName("idP");
                        j.IndexerProperty<int>("IdS").HasColumnName("idS");
                        j.IndexerProperty<DateTime>("Fecha")
                            .HasColumnType("datetime")
                            .HasColumnName("fecha");
                        j.IndexerProperty<string>("Ci")
                            .HasMaxLength(11)
                            .IsUnicode(false)
                            .IsFixedLength();
                        j.IndexerProperty<int>("IdB").HasColumnName("idB");
                    });

            entity.HasMany(d => d.IdDs).WithMany(p => p.Compras)
                .UsingEntity<Dictionary<string, object>>(
                    "Descontado",
                    r => r.HasOne<Descuento>().WithMany()
                        .HasForeignKey("IdD")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descontado__idD__151B244E"),
                    l => l.HasOne<Compra>().WithMany()
                        .HasForeignKey("IdP", "IdS", "Fecha", "Ci")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descontado__160F4887"),
                    j =>
                    {
                        j.HasKey("IdP", "IdS", "Fecha", "Ci", "IdD").HasName("PKDescontado");
                        j.ToTable("Descontado");
                        j.IndexerProperty<int>("IdP").HasColumnName("idP");
                        j.IndexerProperty<int>("IdS").HasColumnName("idS");
                        j.IndexerProperty<DateTime>("Fecha")
                            .HasColumnType("datetime")
                            .HasColumnName("fecha");
                        j.IndexerProperty<string>("Ci")
                            .HasMaxLength(11)
                            .IsUnicode(false)
                            .IsFixedLength();
                        j.IndexerProperty<int>("IdD").HasColumnName("idD");
                    });
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.IdD).HasName("PK_Descuentos");

            entity.ToTable("Descuento");

            entity.Property(e => e.IdD).HasColumnName("idD");
            entity.Property(e => e.NombreD)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Efectivo>(entity =>
        {
            entity.HasKey(e => e.IdPg).HasName("PK__Efectivo__9DB8492F9CA4EC17");

            entity.ToTable("Efectivo");

            entity.Property(e => e.IdPg)
                .ValueGeneratedNever()
                .HasColumnName("idPg");
            entity.Property(e => e.CantidadE).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPgNavigation).WithOne(p => p.Efectivo)
                .HasForeignKey<Efectivo>(d => d.IdPg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Efectivo__idPg__7F2BE32F");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdE);

            entity.ToTable("Empleado");

            entity.Property(e => e.IdE).HasColumnName("idE");
            entity.Property(e => e.ContrasenaE)
                .HasMaxLength(512)
                .IsFixedLength();
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdG);

            entity.ToTable("Genero");

            entity.Property(e => e.IdG).HasColumnName("idG");
            entity.Property(e => e.NombreG)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPg);

            entity.ToTable("Pago");

            entity.Property(e => e.IdPg).HasColumnName("idPg");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdP);

            entity.ToTable("Pelicula");

            entity.Property(e => e.IdP).HasColumnName("idP");
            entity.Property(e => e.Imagen).HasColumnType("text");
            entity.Property(e => e.Sinopsis).HasColumnType("text");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Trailer).HasColumnType("text");

            entity.HasMany(d => d.IdAs).WithMany(p => p.IdPs)
                .UsingEntity<Dictionary<string, object>>(
                    "Elenco",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("IdA")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Elenco__idA__1DB06A4F"),
                    l => l.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("IdP")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Elenco__idP__1CBC4616"),
                    j =>
                    {
                        j.HasKey("IdP", "IdA").HasName("PKelenco");
                        j.ToTable("Elenco");
                        j.IndexerProperty<int>("IdP").HasColumnName("idP");
                        j.IndexerProperty<int>("IdA").HasColumnName("idA");
                    });

            entity.HasMany(d => d.IdGs).WithMany(p => p.IdPs)
                .UsingEntity<Dictionary<string, object>>(
                    "Genero1",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdG")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Generos__idG__2180FB33"),
                    l => l.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("IdP")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Generos__idP__208CD6FA"),
                    j =>
                    {
                        j.HasKey("IdP", "IdG").HasName("PKgeneros");
                        j.ToTable("Generos");
                        j.IndexerProperty<int>("IdP").HasColumnName("idP");
                        j.IndexerProperty<int>("IdG").HasColumnName("idG");
                    });
        });

        modelBuilder.Entity<Punto>(entity =>
        {
            entity.HasKey(e => e.IdPg).HasName("PK__Puntos__9DB8492F0CB84D88");

            entity.Property(e => e.IdPg)
                .ValueGeneratedNever()
                .HasColumnName("idPg");

            entity.HasOne(d => d.IdPgNavigation).WithOne(p => p.Punto)
                .HasForeignKey<Punto>(d => d.IdPg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Puntos__idPg__7C4F7684");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdS);

            entity.ToTable("Sala");

            entity.Property(e => e.IdS).HasColumnName("idS");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => new { e.IdP, e.IdS, e.Fecha }).HasName("PKsesion");

            entity.ToTable("Sesion");

            entity.Property(e => e.IdP).HasColumnName("idP");
            entity.Property(e => e.IdS).HasColumnName("idS");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.IdPNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.IdP)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesion__idP__08B54D69");

            entity.HasOne(d => d.IdSNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.IdS)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesion__idS__09A971A2");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.Ci).HasName("PK__Socio__32149A5BACE7E998");

            entity.ToTable("Socio");

            entity.Property(e => e.Ci)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Codigo)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Contrasena)
                .HasMaxLength(512)
                .IsFixedLength();
            entity.Property(e => e.NombreS)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CiNavigation).WithOne(p => p.Socio)
                .HasForeignKey<Socio>(d => d.Ci)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Socio__Ci__656C112C");
        });

        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.CodigoT).HasName("PK__Tarjeta__BC7B7B928CA058B7");

            entity.Property(e => e.CodigoT)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigoT");
            entity.Property(e => e.Ci)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CiNavigation).WithMany(p => p.Tarjeta)
                .HasForeignKey(d => d.Ci)
                .HasConstraintName("FK__Tarjeta__Ci__6B24EA82");
        });

        modelBuilder.Entity<Web>(entity =>
        {
            entity.HasKey(e => e.IdPg).HasName("PK__Web__9DB8492FA85DDD01");

            entity.ToTable("Web");

            entity.Property(e => e.IdPg)
                .ValueGeneratedNever()
                .HasColumnName("idPg");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CodigoT)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigoT");

            entity.HasOne(d => d.CodigoTNavigation).WithMany(p => p.Webs)
                .HasForeignKey(d => d.CodigoT)
                .HasConstraintName("FK__Web__codigoT__02084FDA");

            entity.HasOne(d => d.IdPgNavigation).WithOne(p => p.Web)
                .HasForeignKey<Web>(d => d.IdPg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Web__idPg__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
