using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Assignment2Project.Models;

namespace Assignment2Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AssetModel> Assets { get; set; }  //enables querying the data and retrieve results from for assets from the database
        public DbSet<ReportModel> Reports { get; set; }
        public DbSet<UpdateResolutionModel> UpdateResolve { get; set; }
    }
}
