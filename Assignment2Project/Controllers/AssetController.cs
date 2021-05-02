using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2Project.Data;
using Assignment2Project.Models;
using Microsoft.AspNetCore.Authorization;
using Assignment2Project.Repository;

namespace Assignment2Project.Views
{
    
    public class AssetController : Controller
    {
        // Commented out code used in unit testing.
        // private readonly IUnitOfWork _unitOfWork; 

        // public AssetController(IUnitOfWork unitOfWork)
        // {
        //   _unitOfWork = unitOfWork;
        //  }


        private readonly ApplicationDbContext _context;

        public AssetController(ApplicationDbContext context)
        {
            _context = context;
        }
              
        [Authorize(Roles = "IT_Support")]
        // GET: Assets
        public async Task<IActionResult> Index(string SearchBy)
        {
            var data = _context.Assets.Where(x => x.AssetName != null);
           // var data = _unitOfWork.Assets.GetAll(x => x.AssetName != null);

            if (!String.IsNullOrEmpty(SearchBy))
            {
               data = data.Where(x => x.AssetName.Contains(SearchBy));
               // data = _unitOfWork.Assets.GetAll(x => x.AssetName.Contains(SearchBy));
            }

           // return View(await data);
            return View(await data.ToListAsync());
                      
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.Assets
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (assetsModel == null)
            {
                return NotFound();
            }

            return View(assetsModel);
        }

        [Authorize(Roles = "IT_Manager")]
        // GET: Assets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetId,AssetName")] AssetModel assetsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetsModel);
        }
        [Authorize(Roles = "IT_Manager")]
        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.Assets.FindAsync(id);
            if (assetsModel == null)
            {
                return NotFound();
            }
            return View(assetsModel);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,AssetName")] AssetModel assetsModel)
        {
            if (id != assetsModel.AssetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetsModel);
                    await _context.SaveChangesAsync();
                  //  await _unitOfWork.Assets.Update(assetsModel);
                 //  await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetsModelExists(assetsModel.AssetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assetsModel);
        }
        [Authorize(Roles = "IT_Manager")]
        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.Assets
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (assetsModel == null)
            {
                return NotFound();
            }

            return View(assetsModel);
        }
        [Authorize(Roles = "IT_Manager")]
        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetsModel = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(assetsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetsModelExists(int id)
        {
            return _context.Assets.Any(e => e.AssetId == id);
        }
    }
}
