using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CreditScoringSystem.Data;
using CreditScoringSystem.Models;
using CreditScoringSystem.Services;

namespace CreditScoringSystem.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly CreditScoringService _scoringService;

        public CreateModel(ApplicationDbContext context, CreditScoringService scoringService)
        {
            _context = context;
            _scoringService = scoringService;
        }

        [BindProperty]
        public CreditApplication Application { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _scoringService.CalculateAllScores(Application);

            Application.CreatedAt = DateTime.Now;

            _context.CreditApplications.Add(Application);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Application created successfully!";
            return RedirectToPage("/Index");
        }
    }
}