using Assignment2Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public interface IAssetRepository : IRepository<AssetModel>
    {
        Task Update(AssetModel asset);
    }
}
