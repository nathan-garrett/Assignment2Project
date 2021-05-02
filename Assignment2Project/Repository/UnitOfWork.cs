using Assignment2Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IAssetRepository Assets { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Assets = new AssetRepository(db);

        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
