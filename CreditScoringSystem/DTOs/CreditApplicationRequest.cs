namespace CreditScoringSystem.DTOs
{
    public class CreditApplicationRequest
    {
        // Personal Information
        public string ApplicationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BirthPlace { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Tab 1 Parameters
        public string? AgeRange { get; set; }
        public string? AgePlusTenor { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Education { get; set; }

        // Tab 2 Parameters
        public string? ResidenceAddressMatch { get; set; }
        public string? ResidenceOwnership { get; set; }
        public string? ResidenceDuration { get; set; }

        // Tab 3 Parameters
        public string? CompanyCategory { get; set; }
        public string? JobPosition { get; set; }
        public string? WorkDuration { get; set; }
        public string? TakeHomePay { get; set; }

        // Tab 4 Parameters
        public string? BankAccount { get; set; }
        public string? AverageBalance { get; set; }
        public string? PaymentTrackRecord { get; set; }
        public string? SlikData { get; set; }
        public string? CreditCardOwnership { get; set; }

        // Tab 5 Parameters
        public string? Tenor { get; set; }
        public string? DebtServiceRatio { get; set; }

        // Tab 6 Parameters
        public string? AppraisalResult { get; set; }
        public string? BuildingArea { get; set; }
        public string? FinancingPurpose { get; set; }
        public string? LTV { get; set; }
    }
}
