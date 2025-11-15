using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreditScoringSystem.Data;
using CreditScoringSystem.Models;

namespace CreditScoringSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<CreditApplication> Applications { get; set; } = new();

        [TempData]
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {
            Applications = await _context.CreditApplications
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var application = await _context.CreditApplications.FindAsync(id);

            if (application != null)
            {
                _context.CreditApplications.Remove(application);
                await _context.SaveChangesAsync();
                Message = "Application deleted successfully.";
            }

            return RedirectToPage();
        }
    }
}