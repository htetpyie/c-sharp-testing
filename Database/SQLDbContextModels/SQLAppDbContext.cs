﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.SQLDbContextModels;

public partial class SQLAppDbContext : DbContext
{
    public SQLAppDbContext()
    {
    }

    public SQLAppDbContext(DbContextOptions<SQLAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBatch> TblBatches { get; set; }

    public virtual DbSet<TblClass> TblClasses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HPPM;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Batc__3214EC07C80D3D0D");

            entity.ToTable("Tbl_Batch");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<TblClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Clas__3214EC07649FB9E9");

            entity.ToTable("Tbl_Class");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
