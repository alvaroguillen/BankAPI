using System;
using System.Collections.Generic;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data;

public partial class BancoContext : DbContext
{
    public BancoContext()
    {
    }

    public BancoContext(DbContextOptions<BancoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    public virtual DbSet<TipoTransaccion> TipoTransaccions { get; set; }

    public virtual DbSet<TransaccionBancarium> TransaccionBancaria { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC0732354E09");

            entity.ToTable("Cliente");

            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3214EC07F042F4BB");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Saldo).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Cuenta_Cliente");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuenta_TipoCuenta");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoCuen__3214EC07B1749242");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoTran__3214EC0745991A5A");

            entity.ToTable("TipoTransaccion");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransaccionBancarium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC0755B399AE");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.TransaccionBancaria)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransaccionBancaria_Cuenta");

            entity.HasOne(d => d.TipoTransaccion).WithMany(p => p.TransaccionBancaria)
                .HasForeignKey(d => d.TipoTransaccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransaccionBancaria_TipoTransaccion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
