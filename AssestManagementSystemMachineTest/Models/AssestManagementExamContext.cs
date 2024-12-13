using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssestManagementSystemMachineTest.Models;

public partial class AssestManagementExamContext : DbContext
{
    public AssestManagementExamContext()
    {
    }

    public AssestManagementExamContext(DbContextOptions<AssestManagementExamContext> options)
        : base(options)
    {
    }
    public virtual DbSet <LoginUser> LoginUsers {get;set;}
    public virtual DbSet<AssetDetail> AssetDetails { get; set; }

    public virtual DbSet<AssetMain> AssetMains { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }
    public object AssetDefanition { get; internal set; }
    public object UserTypes { get; internal set; }
    public object LoginUser { get; internal set; }
    //public object LoginUsers { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetDetail>(entity =>
        {
            entity.HasKey(e => e.AssetDetailId).HasName("PK__AssetDet__876801DDE4734ADF");

            entity.HasIndex(e => e.SerialNumber, "UQ__AssetDet__048A0008C459C062").IsUnique();

            entity.Property(e => e.FromDate).HasColumnType("date");
            entity.Property(e => e.LastUpdated).HasColumnType("date");
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.WarrantyPeriod).HasMaxLength(50);
        });

        modelBuilder.Entity<AssetMain>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__AssetMai__43492352E124DB16");

            entity.ToTable("AssetMain");

            entity.Property(e => e.AssetName).HasMaxLength(100);
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.VId).HasColumnName("V_Id");

            entity.HasOne(d => d.AssetDetail).WithMany(p => p.AssetMains)
                .HasForeignKey(d => d.AssetDetailId)
                .HasConstraintName("FK__AssetMain__Asset__7A672E12");

            entity.HasOne(d => d.VIdNavigation).WithMany(p => p.AssetMains)
                .HasForeignKey(d => d.VId)
                .HasConstraintName("FK__AssetMain__V_Id__797309D9");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__Purchase__A3420A57866B6C89");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.PId).HasColumnName("P_Id");
            entity.Property(e => e.AdId).HasColumnName("Ad_Id");
            entity.Property(e => e.AtId).HasColumnName("At_Id");
            entity.Property(e => e.PurchareOrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PurchareOrder_No");
            entity.Property(e => e.PurchaseDeliveryDate)
                .HasColumnType("date")
                .HasColumnName("PurchaseDelivery_Date");
            entity.Property(e => e.PurchaseOrderDate)
                .HasColumnType("date")
                .HasColumnName("PurchaseOrder_Date");
            entity.Property(e => e.PurchaseQuantity)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Purchase_Quantity");
            entity.Property(e => e.PurchaseStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Purchase_Status");
            entity.Property(e => e.VId).HasColumnName("V_Id");

            entity.HasOne(d => d.VIdNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VId)
                .HasConstraintName("FK__PurchaseOr__V_Id__4AB81AF0");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VId).HasName("PK__Vendor__B35D778C1C7A693E");

            entity.ToTable("Vendor");

            entity.Property(e => e.VId).HasColumnName("V_Id");
            entity.Property(e => e.AtId).HasColumnName("At_Id");
            entity.Property(e => e.VendorAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vendor_Address");
            entity.Property(e => e.VendorFromDate)
                .HasColumnType("date")
                .HasColumnName("VendorFrom_Date");
            entity.Property(e => e.VendorName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Vendor_Name");
            entity.Property(e => e.VendorToDate)
                .HasColumnType("date")
                .HasColumnName("VendorTo_Date");
            entity.Property(e => e.VendorType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Vendor_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
