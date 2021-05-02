using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public interface IUnitOfWork
    {
        IAssetRepository Assets { get; set; }

        Task Save();
    }
}
