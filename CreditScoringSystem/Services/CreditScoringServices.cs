using CreditScoringSystem.Models;

namespace CreditScoringSystem.Services
{
    public class CreditScoringService
    {
        public double CalculateTab1Score(CreditApplication application)
        {
            double tab1Score = 0;
            const double tab1Weight = 0.05;

            double param1Score = application.AgeRange switch
            {
                "56-65" => 25,
                "21-30" => 50,
                "31-45" => 100,
                "46-55" => 75,
                _ => 0
            };
            tab1Score += param1Score * 0.30;

            double param2Score = application.AgePlusTenor switch
            {
                "Above" => 25,
                "Below" => 100,
                _ => 0
            };
            tab1Score += param2Score * 0.10;

            double param3Score = application.MaritalStatus switch
            {
                "SingleMoreThan2" => 25,
                "SingleLessEqual2" => 45,
                "Single0" => 65,
                "MarriedMoreThan2" => 85,
                "MarriedLessEqual2" => 100,
                _ => 0
            };
            tab1Score += param3Score * 0.40;

            double param4Score = application.Education switch
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

        public double CalculateTab2Score(CreditApplication application)
        {
            double tab2Score = 0;
            const double tab2Weight = 0.05;

            double param1Score = application.ResidenceAddressMatch switch
            {
                "NotMatch" => 25,
                "Match" => 100,
                _ => 0
            };
            tab2Score += param1Score * 0.40;

            double param2Score = application.ResidenceOwnership switch
            {
                "Others" => 25,
                "Rent" => 50,
                "OwnInstallment" => 75,
                "Own" => 100,
                _ => 0
            };
            tab2Score += param2Score * 0.30;

            double param3Score = application.ResidenceDuration switch
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

        public double CalculateTab3Score(CreditApplication application)
        {
            double tab3Score = 0;
            const double tab3Weight = 0.20;

            double param1Score = application.CompanyCategory switch
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

            double param2Score = application.JobPosition switch
            {
                "Staff" => 25,
                "Director" => 75,
                "Commissioner" => 100,
                _ => 0
            };
            tab3Score += param2Score * 0.20;

            double param3Score = application.WorkDuration switch
            {
                "LessThan2" => 0,
                "2to5" => 25,
                "5to10" => 75,
                "MoreThan10" => 100,
                _ => 0
            };
            tab3Score += param3Score * 0.20;

            double param4Score = application.TakeHomePay switch
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

        public double CalculateTab4Score(CreditApplication application)
        {
            double tab4Score = 0;
            const double tab4Weight = 0.15;

            double param1Score = application.BankAccount switch
            {
                "None" => 25,
                "Savings" => 50,
                "Checking" => 75,
                "SavingsCheckingDeposit" => 100,
                _ => 0
            };
            tab4Score += param1Score * 0.10;

            double param2Score = application.AverageBalance switch
            {
                "LessThan10" => 25,
                "10to25" => 50,
                "25to50" => 75,
                "MoreThan50" => 100,
                _ => 0
            };
            tab4Score += param2Score * 0.15;

            double param3Score = application.PaymentTrackRecord switch
            {
                "NewBorrower" => 25,
                "LateButtSmooth" => 50,
                "OnTime" => 100,
                _ => 0
            };
            tab4Score += param3Score * 0.15;

            double param4Score = application.SlikData switch
            {
                "Col3to5" => 0,
                "ArrearsLess3" => 50,
                "NoFacility" => 75,
                "Smooth" => 100,
                _ => 0
            };
            tab4Score += param4Score * 0.40;

            double param5Score = application.CreditCardOwnership switch
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

        public double CalculateTab5Score(CreditApplication application)
        {
            double tab5Score = 0;
            const double tab5Weight = 0.30;

            double param1Score = application.Tenor switch
            {
                "MoreThan15" => 25,
                "10to15" => 50,
                "5to10" => 75,
                "LessThan5" => 100,
                _ => 0
            };
            tab5Score += param1Score * 0.25;

            double param2Score = application.DebtServiceRatio switch
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

        public double CalculateTab6Score(CreditApplication application)
        {
            double tab6Score = 0;
            const double tab6Weight = 0.25;

            double param1Score = application.AppraisalResult switch
            {
                "NotRecommended" => 0,
                "Marketable" => 100,
                _ => 0
            };
            tab6Score += param1Score * 0.10;

            double param2Score = application.BuildingArea switch
            {
                "MoreThan200" => 25,
                "100to200" => 50,
                "45to100" => 75,
                "LessThan45" => 100,
                _ => 0
            };
            tab6Score += param2Score * 0.20;

            double param3Score = application.FinancingPurpose switch
            {
                "Others" => 25,
                "Rent" => 50,
                "Renovation" => 75,
                "FirstOwn" => 100,
                _ => 0
            };
            tab6Score += param3Score * 0.10;

            double param4Score = application.LTV switch
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

        public void CalculateAllScores(CreditApplication application)
        {
            application.Tab1Score = CalculateTab1Score(application);
            application.Tab2Score = CalculateTab2Score(application);
            application.Tab3Score = CalculateTab3Score(application);
            application.Tab4Score = CalculateTab4Score(application);
            application.Tab5Score = CalculateTab5Score(application);
            application.Tab6Score = CalculateTab6Score(application);

            application.TotalScore = application.Tab1Score + application.Tab2Score +
                                    application.Tab3Score + application.Tab4Score +
                                    application.Tab5Score + application.Tab6Score;

            application.RiskLevel = DetermineRiskLevel(application.TotalScore);
        }

        public string DetermineRiskLevel(double totalScore)
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