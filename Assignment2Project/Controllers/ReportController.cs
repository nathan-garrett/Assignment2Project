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
        
        [Authorize] //users must authorized to see this view
        // GET: Report
        public async Task<IActionResult> Index(string SearchBy) 
        {
            var data = _context.Reports.Where(x => x.CreatedByUserEmail != null); //grabs the data from the database where CreatedByUserEmail is not null
            if (!String.IsNullOrEmpty(SearchBy)) // checks to see if SearchBy contains a search
            {
                ViewData["Search"] = SearchBy;
                data = data.Where(x => x.CreatedByUserEmail.Contains(SearchBy)); //checks to see if CreatedByUserEmail contains any values being searched for.
            }
            

            List<ReportModel> reports = await data.ToListAsync(); // adds the results to a list or ReportModel

            foreach(var report in reports)
            {
                report.Asset = await _context.Assets.FindAsync(report.AssetId); // Adds asset data to the report list.
            }

            return View(reports); //return view
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportViewModel reportVM = new ReportViewModel()
            {
                Report = new ReportModel()
            };
           
            reportVM.Report = await _context.Reports.FirstOrDefaultAsync(m => m.ReportId == id); //grabs the report data where the passed id is equal to the ReportId 

            var updateResolve = _context.UpdateResolve.Where(p => p.IssueId == id); //adds the UpdateResolve data what equals the passed id and stores it in updateResolve

            
            reportVM.Report.Asset = await _context.Assets.FindAsync(reportVM.Report.AssetId); // finds the asset data and stores it in reportVM
            

            reportVM.Report.UpdateResolve = await updateResolve.OrderByDescending(c => c.UpdateResolveDTS).ToListAsync(); //adds the store data in updateResolve to reportVM.

            if (reportVM.Report == null)
            {
                return NotFound();
            }

            if (reportVM == null)
            {
                return NotFound();
            }

            return View(reportVM);
        }
        [Authorize(Roles = "IT_Support")] //User Authenication user requires a certain role to see this view
        // GET: Report/Create
        public IActionResult Create()
        {
       
            ReportViewModel reportVM = new ReportViewModel() // Create a new ReportViewModel
            {
                Report = new ReportModel(), //Create a new ReportModel

                AssetList = _context.Assets.Select(i => new SelectListItem // Add assets to SelectListItem
                {
                    Text = i.AssetName,
                    Value = i.AssetId.ToString()
                }),
                PersonList = _context.Users.Select(y => new SelectListItem
                {
                    Text = y.UserName,
                    Value = y.UserName
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
                DateTime datetime = DateTime.Now; //set a variable to the current time and date

                reportViewModel.Report.CreatedByUserEmail = User.Identity.Name; // set CreatedByUserEmail to the currently logged in user
                reportViewModel.Report.ReportDTS = datetime; //set ReportDTS to the current date and time
                reportViewModel.Report.Status = TicketStatus.Open; //set Status to Open.
                _context.Add(reportViewModel.Report); // add new data to the databse
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); //redirect to another page

            }
            return View(reportViewModel);
        }
        [Authorize(Roles = "IT_Support")]
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

            reportVM.Report = await _context.Reports.FirstOrDefaultAsync(m => m.ReportId == id); 
           
            var updateResolve = _context.UpdateResolve.Where(p => p.IssueId == id);
            reportVM.Report.UpdateResolve = await updateResolve.OrderByDescending(c => c.UpdateResolveDTS).ToListAsync();
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
        public async Task<IActionResult> Edit(int? id, string updateResolveIssue, [Bind("Report, AssetId")] ReportViewModel reportViewModel)
        {
           

            if (id != reportViewModel.Report.ReportId) //Check to see if id is not found withn reportViewModel
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(updateResolveIssue != null ) // check updateResolveIssue contains data
                    {
                        reportViewModel.Report = await _context.Reports.FirstOrDefaultAsync(m => m.ReportId == id);
                        UpdateResolutionModel ur = new UpdateResolutionModel();
                        ur.Text = updateResolveIssue;
                        ur.UpdateResolveDTS = DateTime.Now;
                        ur.StaffMemberActioning = User.Identity.Name;
                        ur.IssueId = (int)id;
                        reportViewModel.Report.UpdateResolve.Add(ur);
                    }
                    
                   
                    _context.Update(reportViewModel.Report); //updates the database with new values
                    await _context.SaveChangesAsync(); //saves the changes
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
                return RedirectToAction(nameof(Edit));
            }
            return View(reportViewModel);
        }
        [Authorize(Roles = "IT_Manager")]
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

            var updateResolve = _context.UpdateResolve.Where(p => p.IssueId == id);
            
            _context.UpdateResolve.RemoveRange(updateResolve); // deletes store Update/Resolution comments matching the report id
            _context.Reports.Remove(reportModel); // deletes the report matching the id.
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("DeleteLog")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogEntry(int id)
        {

            var updateResolve = await _context.UpdateResolve.FindAsync(id);    //Where(p => p.UpdateResolveId == id);

            _context.UpdateResolve.Remove(updateResolve);            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit));
        }
        private bool ReportModelExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
              
    }
   
}
