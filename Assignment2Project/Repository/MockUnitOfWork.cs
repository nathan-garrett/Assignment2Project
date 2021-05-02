using Assignment2Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public IAssetRepository Assets { get; set; }

        public MockUnitOfWork()
        {
            Assets = new MockAssetRepository(GenerateAssets());

        }

        public async Task Save()
        {
            await Task.Delay(1);
        }

        public List<AssetModel> GenerateAssets()
        {
            List<AssetModel> asset = new List<AssetModel>();

            asset.Add(new AssetModel
            {
                AssetId = 1,
                AssetName = "AssetTest1"
            });

            asset.Add(new AssetModel
            {
                AssetId = 2,
                AssetName = "AssetTest2"
            });

            asset.Add(new AssetModel
            {
                AssetId = 3,
                AssetName = "AssetTest3"
            });

            return asset;
        }
    }
}
