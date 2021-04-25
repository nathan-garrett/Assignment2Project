using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2Project.Data;
using Assignment2Project.Models;
using Assignment2Project.ViewModel;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(string SearchBy)
        {
            var data = _context.Reports.Where(x => x.CreatedByUserEmail != null);
            if (!String.IsNullOrEmpty(SearchBy))
            {
                ViewData["Search"] = SearchBy;
                data = data.Where(x => x.CreatedByUserEmail.Contains(SearchBy));
            }
            

            List<ReportModel> reports = await data.ToListAsync();

            foreach(var report in reports)
            {
                report.Asset = await _context.Assets.FindAsync(report.AssetId);
            }

            return View(reports);
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportModel = await _context.Reports
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
            ReportViewModel reportVM = new ReportViewModel()
            {
                Report = new ReportModel(),
                AssetList = _context.Assets.Select(i => new SelectListItem
                {
                    Text = i.AssetName,
                    Value = i.AssetId.ToString()
                })
            };

            return View(reportVM);
        }

        // POST: Report/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Report, AssetId")] ReportViewModel reportViewModel)
        {
            if (ModelState.IsValid)
            {
                DateTime datetime = DateTime.Now;

                reportViewModel.Report.CreatedByUserEmail = User.Identity.Name;
                reportViewModel.Report.ReportDTS = datetime;
                reportViewModel.Report.Status = TicketStatus.Open;
                _context.Add(reportViewModel.Report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(reportViewModel);
        }

        // GET: Report/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ReportViewModel reportVM = new ReportViewModel()
            {
                Report = new ReportModel(),
                AssetList = _context.Assets.Select(i => new SelectListItem
                {
                    Text = i.AssetName,
                    Value = i.AssetId.ToString()
                })
            };
            

            if (id == null)
            {
                return NotFound();
            }

            reportVM.Report = await _context.Reports.FindAsync(id);
            if (reportVM.Report == null)
            {
                return NotFound();
            }
            return View(reportVM);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Report, AssetId")] ReportViewModel reportViewModel)
        {
            if (id != reportViewModel.Report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportViewModel.Report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportModelExists(reportViewModel.Report.ReportId))
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
            return View(reportViewModel);
        }

        // GET: Report/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportModel = await _context.Reports
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
            var reportModel = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(reportModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportModelExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }

    //[HttpPost, ActionName("Asset")]
   // [ValidateAntiForgeryToken]
   // public async Task<IActionResult> Asset(string name, int id)
  // {
   //        // var reportAssets = await _context.Assets.
                          

       
        


    //    await _context.SaveChangesAsync();

    //    return RedirectToAction(nameof(Create), new { id = id });
            
 //   }

       
    }
   
}
