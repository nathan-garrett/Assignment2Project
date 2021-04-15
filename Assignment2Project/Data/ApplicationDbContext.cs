using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ReportManagementSystem.Models;
using Assignment2Project.Models;

namespace Assignment2Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AssetsModel> AssetsModel { get; set; }
        public DbSet<Assignment2Project.Models.ReportModel> ReportModel { get; set; }
    }
}
