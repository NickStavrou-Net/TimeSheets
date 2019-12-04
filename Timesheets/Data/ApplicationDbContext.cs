﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timesheets.Areas.Identity.Data;
using Timesheets.Models;

namespace Timesheets.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<DepartmentProject> DepartmentProjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DepartmentProject>()
                .HasKey(dp => new { dp.DepartmentId, dp.ProjectId });
            builder.Entity<DepartmentProject>()
                .HasOne(dp => dp.Department)
                .WithMany(d => d.RelatedProjects)
                .HasForeignKey(dp => dp.DepartmentId);
            builder.Entity<DepartmentProject>()
                .HasOne(dp => dp.Project)
                .WithMany(p => p.RelatedDepartments)
                .HasForeignKey(dp => dp.ProjectId);

            builder.Entity<Project>()
                .HasOne(p => p.OwnerDepartment)
                .WithMany(d => d.OwnedProjects)
                .HasForeignKey(p => p.OwnerDepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Department>()
                .HasOne(p => p.DepartmentHead)
                .WithMany(u => u.HeadingDepartments)
                .HasForeignKey(p => p.DepartmentHeadId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
