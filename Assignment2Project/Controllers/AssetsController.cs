using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2Project.Data;
using ReportManagementSystem.Models;

namespace Assignment2Project.Views
{
    public class AssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssetsModel.ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.AssetsModel
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (assetsModel == null)
            {
                return NotFound();
            }

            return View(assetsModel);
        }

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
        public async Task<IActionResult> Create([Bind("AssetId,AssetName")] AssetsModel assetsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetsModel);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.AssetsModel.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,AssetName")] AssetsModel assetsModel)
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

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.AssetsModel
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (assetsModel == null)
            {
                return NotFound();
            }

            return View(assetsModel);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetsModel = await _context.AssetsModel.FindAsync(id);
            _context.AssetsModel.Remove(assetsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetsModelExists(int id)
        {
            return _context.AssetsModel.Any(e => e.AssetId == id);
        }
    }
}
