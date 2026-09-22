using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models;

public partial class FirstDbContext : DbContext
{
    public FirstDbContext()
    {
    }

    public FirstDbContext(DbContextOptions<FirstDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Inscompany> Inscompanies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderservice> Orderservices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=6432;Database=first_db;Username=user;Password=secret");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("client_pk");

            entity.ToTable("client");

            entity.HasIndex(e => e.Passport, "client_unique").IsUnique();

            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Company).HasColumnName("company");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasColumnType("character varying")
                .HasColumnName("middlename");
            entity.Property(e => e.Number)
                .HasColumnType("character varying")
                .HasColumnName("number");
            entity.Property(e => e.Passport)
                .HasColumnType("character varying")
                .HasColumnName("passport");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_inscompany_fk");
        });

        modelBuilder.Entity<Inscompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inscompany_pk");

            entity.ToTable("inscompany");

            entity.HasIndex(e => new { e.Id, e.Inn }, "inscompany_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Bik)
                .HasColumnType("character varying")
                .HasColumnName("bik");
            entity.Property(e => e.Inn)
                .HasColumnType("character varying")
                .HasColumnName("inn");
            entity.Property(e => e.Rs)
                .HasColumnType("character varying")
                .HasColumnName("rs");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pk");

            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datestart).HasColumnName("datestart");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.LoginNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Login)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_client_fk");
        });

        modelBuilder.Entity<Orderservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderservices_pk");

            entity.ToTable("orderservices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idorder).HasColumnName("idorder");
            entity.Property(e => e.Idservice).HasColumnName("idservice");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.IdorderNavigation).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Idorder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderservices_order_fk");

            entity.HasOne(d => d.IdserviceNavigation).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Idservice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderservices_services_fk");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("newtable_pk");

            entity.ToTable("services");

            entity.Property(e => e.Code)
                .HasDefaultValueSql("nextval('newtable_code_seq'::regclass)")
                .HasColumnName("code");
            entity.Property(e => e.Average).HasColumnName("average");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
