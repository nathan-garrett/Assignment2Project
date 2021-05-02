using Assignment2Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public class MockAssetRepository : MockRepository<AssetModel>, IAssetRepository
    {
        private readonly List<AssetModel> _db;
        public MockAssetRepository(List<AssetModel> db) : base(db)
        {
            _db = db;
        }

        public async Task Update(AssetModel asset)
        {
            var obj = _db.Find(c => c.AssetId == asset.AssetId);
            await Task.Delay(1);
            obj = asset;

        }
    }
}
