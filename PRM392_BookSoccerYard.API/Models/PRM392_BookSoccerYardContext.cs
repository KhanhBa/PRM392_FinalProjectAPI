﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRM392_BookSoccerYard.API.Models;

public partial class PRM392_BookSoccerYardContext : DbContext
{
    public PRM392_BookSoccerYardContext() { }
    public PRM392_BookSoccerYardContext(DbContextOptions<PRM392_BookSoccerYardContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Yard> Yards { get; set; }
    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FB29E848E");

            entity.ToTable("Customer");

            entity.Property(e => e.Id)
                 .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83F46B10344");

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("create_date");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("bookingDate");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EndTime)
                .HasColumnName("end_time");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.StartTime)
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("update_date");
            entity.Property(e => e.YardId).HasColumnName("yard_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Slot).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Slot");

            entity.HasOne(d => d.Yard).WithMany(p => p.Orders)
                .HasForeignKey(d => d.YardId)
                .HasConstraintName("FK_Order_Yard");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3213E83F2F308D59");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Id).ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.FinalPrice).HasColumnName("final_price");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.QuantityService).HasColumnName("quantity_service");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__order__49C3F6B7");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__OrderDeta__servi__48CFD27E");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3213E83F7361C034");

            entity.ToTable("Payment");

            entity.Property(e => e.Id).ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("create_date");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .HasColumnName("method");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Payment_Order");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3213E83F28EBAAD4");
            entity.ToTable("Service");
            entity.Property(e => e.Id).ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.ToTable("Slot");

            entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PriceUp).HasColumnName("price_up");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F87EBB201");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Yard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Yard__3213E83FD6998346");

            entity.ToTable("Yard");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
            .HasColumnName("status");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}