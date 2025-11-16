namespace CreditScoringSystem.DTOs
{
    public class CreditApplicationResponse
    {
        public int Id { get; set; }
        public string ApplicationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Tab1Score { get; set; }
        public double Tab2Score { get; set; }
        public double Tab3Score { get; set; }
        public double Tab4Score { get; set; }
        public double Tab5Score { get; set; }
        public double Tab6Score { get; set; }
        public double TotalScore { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
