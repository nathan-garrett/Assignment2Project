using Assignment2Project.Data;
using Assignment2Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public class AssetRepository : Repository<AssetModel>, IAssetRepository
    {
        private readonly ApplicationDbContext _db;
        public AssetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task Update(AssetModel asset)
        {
            var ass = await _db.Assets.FirstOrDefaultAsync(c => c.AssetId == asset.AssetId);

            if (ass != null)
            {
                ass.AssetName = asset.AssetName;
            }
        }
    }
}
