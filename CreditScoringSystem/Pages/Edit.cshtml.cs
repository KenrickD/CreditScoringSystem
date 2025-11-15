using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreditScoringSystem.Data;
using CreditScoringSystem.Models;

namespace CreditScoringSystem.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
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

            // Recalculate all tab scores
            Application.Tab1Score = CalculateTab1Score();
            Application.Tab2Score = CalculateTab2Score();
            Application.Tab3Score = CalculateTab3Score();
            Application.Tab4Score = CalculateTab4Score();
            Application.Tab5Score = CalculateTab5Score();
            Application.Tab6Score = CalculateTab6Score();

            // Recalculate total score
            Application.TotalScore = Application.Tab1Score + Application.Tab2Score +
                                    Application.Tab3Score + Application.Tab4Score +
                                    Application.Tab5Score + Application.Tab6Score;

            // Re-determine Risk Level
            Application.RiskLevel = DetermineRiskLevel(Application.TotalScore);

            _context.Attach(Application).State = EntityState.Modified;

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

        private double CalculateTab1Score()
        {
            double tab1Score = 0;
            const double tab1Weight = 0.05;

            double param1Score = Application.AgeRange switch
            {
                "56-65" => 25,
                "21-30" => 50,
                "31-45" => 100,
                "46-55" => 75,
                _ => 0
            };
            tab1Score += param1Score * 0.30;

            double param2Score = Application.AgePlusTenor switch
            {
                "Above" => 25,
                "Below" => 100,
                _ => 0
            };
            tab1Score += param2Score * 0.10;

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

            double param4Score = Application.Education switch
            {
                "HighSchool" => 25,
                "Diploma" => 50,
                "Bachelor" => 75,
                "Master" => 100,
                _ => 0
            };
            tab1Score += param4Score * 0.20;

            return tab1Score * tab1Weight;
        }

        private double CalculateTab2Score()
        {
            double tab2Score = 0;
            const double tab2Weight = 0.05;

            double param1Score = Application.ResidenceAddressMatch switch
            {
                "NotMatch" => 25,
                "Match" => 100,
                _ => 0
            };
            tab2Score += param1Score * 0.40;

            double param2Score = Application.ResidenceOwnership switch
            {
                "Others" => 25,
                "Rent" => 50,
                "OwnInstallment" => 75,
                "Own" => 100,
                _ => 0
            };
            tab2Score += param2Score * 0.30;

            double param3Score = Application.ResidenceDuration switch
            {
                "LessThan2" => 25,
                "2to5" => 50,
                "5to8" => 75,
                "MoreThan8" => 100,
                _ => 0
            };
            tab2Score += param3Score * 0.30;

            return tab2Score * tab2Weight;
        }

        private double CalculateTab3Score()
        {
            double tab3Score = 0;
            const double tab3Weight = 0.20;

            double param1Score = Application.CompanyCategory switch
            {
                "Government" => 100,
                "BUMD" => 25,
                "PrivateNoRating" => 100,
                "PrivateWithRating" => 25,
                "PrivateCat1" => 75,
                "PrivateCat2" => 50,
                "PrivateCat3" => 0,
                _ => 0
            };
            tab3Score += param1Score * 0.20;

            double param2Score = Application.JobPosition switch
            {
                "Staff" => 25,
                "Director" => 75,
                "Commissioner" => 100,
                _ => 0
            };
            tab3Score += param2Score * 0.20;

            double param3Score = Application.WorkDuration switch
            {
                "LessThan2" => 0,
                "2to5" => 25,
                "5to10" => 75,
                "MoreThan10" => 100,
                _ => 0
            };
            tab3Score += param3Score * 0.20;

            double param4Score = Application.TakeHomePay switch
            {
                "LessThan10" => 25,
                "10to25" => 50,
                "25to50" => 75,
                "MoreThan50" => 100,
                _ => 0
            };
            tab3Score += param4Score * 0.40;

            return tab3Score * tab3Weight;
        }

        private double CalculateTab4Score()
        {
            double tab4Score = 0;
            const double tab4Weight = 0.15;

            double param1Score = Application.BankAccount switch
            {
                "None" => 25,
                "Savings" => 50,
                "Checking" => 75,
                "SavingsCheckingDeposit" => 100,
                _ => 0
            };
            tab4Score += param1Score * 0.10;

            double param2Score = Application.AverageBalance switch
            {
                "LessThan10" => 25,
                "10to25" => 50,
                "25to50" => 75,
                "MoreThan50" => 100,
                _ => 0
            };
            tab4Score += param2Score * 0.15;

            double param3Score = Application.PaymentTrackRecord switch
            {
                "NewBorrower" => 25,
                "LateButtSmooth" => 50,
                "OnTime" => 100,
                _ => 0
            };
            tab4Score += param3Score * 0.15;

            double param4Score = Application.SlikData switch
            {
                "Col3to5" => 0,
                "ArrearsLess3" => 50,
                "NoFacility" => 75,
                "Smooth" => 100,
                _ => 0
            };
            tab4Score += param4Score * 0.40;

            double param5Score = Application.CreditCardOwnership switch
            {
                "None" => 25,
                "Basic" => 50,
                "Gold" => 75,
                "Platinum" => 100,
                _ => 0
            };
            tab4Score += param5Score * 0.20;

            return tab4Score * tab4Weight;
        }

        private double CalculateTab5Score()
        {
            double tab5Score = 0;
            const double tab5Weight = 0.30;

            double param1Score = Application.Tenor switch
            {
                "MoreThan15" => 25,
                "10to15" => 50,
                "5to10" => 75,
                "LessThan5" => 100,
                _ => 0
            };
            tab5Score += param1Score * 0.25;

            double param2Score = Application.DebtServiceRatio switch
            {
                "MoreThan50" => 0,
                "40to50" => 50,
                "30to40" => 75,
                "LessThan30" => 100,
                _ => 0
            };
            tab5Score += param2Score * 0.75;

            return tab5Score * tab5Weight;
        }

        private double CalculateTab6Score()
        {
            double tab6Score = 0;
            const double tab6Weight = 0.25;

            double param1Score = Application.AppraisalResult switch
            {
                "NotRecommended" => 0,
                "Marketable" => 100,
                _ => 0
            };
            tab6Score += param1Score * 0.10;

            double param2Score = Application.BuildingArea switch
            {
                "MoreThan200" => 25,
                "100to200" => 50,
                "45to100" => 75,
                "LessThan45" => 100,
                _ => 0
            };
            tab6Score += param2Score * 0.20;

            double param3Score = Application.FinancingPurpose switch
            {
                "Others" => 25,
                "Rent" => 50,
                "Renovation" => 75,
                "FirstOwn" => 100,
                _ => 0
            };
            tab6Score += param3Score * 0.10;

            double param4Score = Application.LTV switch
            {
                "MoreThan100" => 0,
                "MoreThan90" => 25,
                "MoreThan80" => 50,
                "MoreThan70" => 75,
                "LessThan70" => 100,
                _ => 0
            };
            tab6Score += param4Score * 0.60;

            return tab6Score * tab6Weight;
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