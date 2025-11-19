using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreditScoringSystem.Data;
using CreditScoringSystem.Models;
using CreditScoringSystem.Services;

namespace CreditScoringSystem.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly CreditScoringService _scoringService;

        public EditModel(ApplicationDbContext context, CreditScoringService scoringService)
        {
            _context = context;
            _scoringService = scoringService;
        }

        [BindProperty]
        public CreditApplication Application { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.CreditApplications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            Application = application;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _scoringService.CalculateAllScores(Application);

            _context.CreditApplications.Update(Application);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(Application.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Message"] = "Application updated successfully! Risk has been recalculated.";
            return RedirectToPage("/Index");
        }

        private bool ApplicationExists(int id)
        {
            return _context.CreditApplications.Any(e => e.Id == id);
        }
    }
}