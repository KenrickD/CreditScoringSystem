using System.ComponentModel.DataAnnotations;

namespace CreditScoringSystem.Models
{
    public class CreditApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "No Aplikasi")]
        public string ApplicationNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tempat Lahir")]
        public string BirthPlace { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tanggal Lahir")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Jenis Kelamin")]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Kode Pos")]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Alamat")]
        public string Address { get; set; } = string.Empty;

        // Tab 1 Parameters
        [Display(Name = "Umur Pemohon")]
        public string? AgeRange { get; set; }

        [Display(Name = "Umur Pemohon + Tenor")]
        public string? AgePlusTenor { get; set; }

        [Display(Name = "Status Perkawinan")]
        public string? MaritalStatus { get; set; }

        [Display(Name = "Pendidikan")]
        public string? Education { get; set; }

        // Scoring Results
        public double Tab1Score { get; set; }
        public double TotalScore { get; set; }

        [Display(Name = "Risk Level")]
        public string RiskLevel { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}