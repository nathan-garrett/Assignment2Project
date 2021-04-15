using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2Project.Data;
using Assignment2Project.Models;

namespace Assignment2Project.Views
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Report
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportModel.ToListAsync());
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportModel = await _context.ReportModel
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reportModel == null)
            {
                return NotFound();
            }

            return View(reportModel);
        }

        // GET: Report/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,RType,IssueDetails,ReportDTS")] ReportModel reportModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportModel);
        }

        // GET: Report/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportModel = await _context.ReportModel.FindAsync(id);
            if (reportModel == null)
            {
                return NotFound();
            }
            return View(reportModel);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,RType,IssueDetails,ReportDTS")] ReportModel reportModel)
        {
            if (id != reportModel.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportModelExists(reportModel.ReportId))
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
            return View(reportModel);
        }

        // GET: Report/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportModel = await _context.ReportModel
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reportModel == null)
            {
                return NotFound();
            }

            return View(reportModel);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportModel = await _context.ReportModel.FindAsync(id);
            _context.ReportModel.Remove(reportModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportModelExists(int id)
        {
            return _context.ReportModel.Any(e => e.ReportId == id);
        }
    }
}
