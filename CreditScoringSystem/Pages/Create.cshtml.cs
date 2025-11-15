using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CreditScoringSystem.Data;
using CreditScoringSystem.Models;

namespace CreditScoringSystem.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
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

            // Calculate Tab 1 Score
            Application.Tab1Score = CalculateTab1Score();

            // For now, total score is just Tab 1 score (will be updated when more tabs are added)
            Application.TotalScore = Application.Tab1Score;

            // Determine Risk Level
            Application.RiskLevel = DetermineRiskLevel(Application.TotalScore);

            Application.CreatedAt = DateTime.Now;

            _context.CreditApplications.Add(Application);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Application created successfully!";
            return RedirectToPage("/Index");
        }

        private double CalculateTab1Score()
        {
            double tab1Score = 0;
            const double tab1Weight = 0.05; // 5%

            // Parameter 1: Umur Pemohon (Weight: 30%)
            double param1Score = Application.AgeRange switch
            {
                "56-65" => 25,
                "21-30" => 50,
                "31-45" => 100,
                "46-55" => 75,
                _ => 0
            };
            tab1Score += param1Score * 0.30;

            // Parameter 2: Umur Pemohon + Tenor (Weight: 10%)
            double param2Score = Application.AgePlusTenor switch
            {
                "Above" => 25,
                "Below" => 100,
                _ => 0
            };
            tab1Score += param2Score * 0.10;

            // Parameter 3: Status Perkawinan (Weight: 40%)
            double param3Score = Application.MaritalStatus switch
            {
                "SingleMoreThan2" => 25,
                "SingleLessEqual2" => 45,
                "Single0" => 65,
                "MarriedMoreThan2" => 85,
                "MarriedLessEqual2" => 100,
                _ => 0
            };
            tab1Score += param3Score * 0.40;

            // Parameter 4: Pendidikan (Weight: 20%)
            double param4Score = Application.Education switch
            {
                "HighSchool" => 25,
                "Diploma" => 50,
                "Bachelor" => 75,
                "Master" => 100,
                _ => 0
            };
            tab1Score += param4Score * 0.20;

            // Multiply by tab weight
            return tab1Score * tab1Weight;
        }

        private string DetermineRiskLevel(double totalScore)
        {
            if (totalScore <= 55)
                return "High Risk";
            else if (totalScore <= 70)
                return "Medium Risk";
            else
                return "Low Risk";
        }
    }
}