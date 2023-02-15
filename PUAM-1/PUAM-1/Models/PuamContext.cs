using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PUAM_1.Models;

public partial class PuamContext : DbContext
{
    public PuamContext()
    {
    }

    public PuamContext(DbContextOptions<PuamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adulto> Adultos { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NJCRGQI;Integrated Security=True;Initial Catalog = PUAM;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adulto>(entity =>
        {
            entity.HasKey(e => e.IdAdultos).HasName("PK__Adulto__B44BB313E3C1ECD8");

            entity.ToTable("Adulto");

            entity.HasIndex(e => e.Cedula, "UQ__Adulto__B4ADFE38EF83A121").IsUnique();

            entity.Property(e => e.IdAdultos).HasColumnName("idAdultos");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Adultos)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK__Adulto__idProgra__22751F6C");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.IdCarrera).HasName("PK__Carrera__7B19E79175AF4789");

            entity.ToTable("Carrera");

            entity.HasIndex(e => e.CodigoCarrera, "UQ__Carrera__2D5445FDB6C09050").IsUnique();

            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.CodigoCarrera)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Facultad)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.NombreCarrera)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PK__Clases__17317A687D748E85");

            entity.HasIndex(e => e.NombreClase, "UQ__Clases__D5D04117E6BF7936").IsUnique();

            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.NombreClase)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__AEFFDBC5D313ED9C");

            entity.HasIndex(e => e.Cedula, "UQ__Estudian__B4ADFE38D427C741").IsUnique();

            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Coordinador)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CarreraNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.Carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__Carre__236943A5");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__Inscripc__3D58AB6996091277");

            entity.ToTable("Inscripcion");

            entity.HasIndex(e => e.CedulaAdulto, "UQ__Inscripc__48EE4F11D8431A1B").IsUnique();

            entity.Property(e => e.IdInscripcion).HasColumnName("idInscripcion");
            entity.Property(e => e.CedulaAdulto)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdClase).HasColumnName("idClase");

            entity.HasOne(d => d.CedulaAdultoNavigation).WithOne(p => p.Inscripcion)
                .HasPrincipalKey<Adulto>(p => p.Cedula)
                .HasForeignKey<Inscripcion>(d => d.CedulaAdulto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__Cedul__2739D489");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__idCla__2645B050");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.IdPrograma).HasName("PK__Programa__467DDFD6D46D96B9");

            entity.HasIndex(e => e.Programa1, "UQ__Programa__68006339C64D2DF5").IsUnique();

            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.Programa1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Programa");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__Registro__62FC8F58F3CFCF2C");

            entity.ToTable("Registro");

            entity.HasIndex(e => e.CedulaEstudiante, "UQ__Registro__138CA48B99BC347F").IsUnique();

            entity.Property(e => e.IdRegistro).HasColumnName("idRegistro");
            entity.Property(e => e.CedulaAdulto)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CedulaEstudiante)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Evidencia)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.CedulaAdultoNavigation).WithMany(p => p.Registros)
                .HasPrincipalKey(p => p.Cedula)
                .HasForeignKey(d => d.CedulaAdulto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registro__Cedula__25518C17");

            entity.HasOne(d => d.CedulaEstudianteNavigation).WithOne(p => p.Registro)
                .HasPrincipalKey<Estudiante>(p => p.Cedula)
                .HasForeignKey<Registro>(d => d.CedulaEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registro__Cedula__245D67DE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
