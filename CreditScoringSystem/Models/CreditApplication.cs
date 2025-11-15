using System.ComponentModel.DataAnnotations;

namespace CreditScoringSystem.Models
{
    public class CreditApplication
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "No Aplikasi is required")]
        [Display(Name = "No Aplikasi")]
        public string ApplicationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nama is required")]
        [Display(Name = "Nama")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tempat Lahir is required")]
        [Display(Name = "Tempat Lahir")]
        public string BirthPlace { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanggal Lahir is required")]
        [Display(Name = "Tanggal Lahir")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Jenis Kelamin is required")]
        [Display(Name = "Jenis Kelamin")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kode Pos is required")]
        [Display(Name = "Kode Pos")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Alamat is required")]
        [Display(Name = "Alamat")]
        public string Address { get; set; } = string.Empty;

        // Tab 1 Parameters
        [Required(ErrorMessage = "Umur Pemohon is required")]
        [Display(Name = "Umur Pemohon")]
        public string? AgeRange { get; set; }

        [Required(ErrorMessage = "Umur Pemohon + Tenor is required")]
        [Display(Name = "Umur Pemohon + Tenor")]
        public string? AgePlusTenor { get; set; }

        [Required(ErrorMessage = "Status Perkawinan is required")]
        [Display(Name = "Status Perkawinan")]
        public string? MaritalStatus { get; set; }

        [Required(ErrorMessage = "Pendidikan is required")]
        [Display(Name = "Pendidikan")]
        public string? Education { get; set; }

        // Tab 2 Parameters
        [Required(ErrorMessage = "Alamat Tempat Tinggal is required")]
        [Display(Name = "Alamat Tempat Tinggal")]
        public string? ResidenceAddressMatch { get; set; }

        [Required(ErrorMessage = "Kepemilikan tempat tinggal is required")]
        [Display(Name = "Kepemilikan tempat tinggal")]
        public string? ResidenceOwnership { get; set; }

        [Required(ErrorMessage = "Lama Menempati is required")]
        [Display(Name = "Lama Menempati")]
        public string? ResidenceDuration { get; set; }

        // Tab 3 Parameters
        [Required(ErrorMessage = "Kategori Perusahaan is required")]
        [Display(Name = "Kategori Perusahaan")]
        public string? CompanyCategory { get; set; }

        [Required(ErrorMessage = "Jabatan is required")]
        [Display(Name = "Jabatan")]
        public string? JobPosition { get; set; }

        [Required(ErrorMessage = "Lama Bekerja is required")]
        [Display(Name = "Lama Bekerja")]
        public string? WorkDuration { get; set; }

        [Required(ErrorMessage = "Pendapatan THP is required")]
        [Display(Name = "Pendapatan THP")]
        public string? TakeHomePay { get; set; }

        // Tab 4 Parameters
        [Required(ErrorMessage = "Rekening Bank is required")]
        [Display(Name = "Rekening Bank")]
        public string? BankAccount { get; set; }

        [Required(ErrorMessage = "Rata-Rata Saldo Per bulannya is required")]
        [Display(Name = "Rata-Rata Saldo Per bulannya")]
        public string? AverageBalance { get; set; }

        [Required(ErrorMessage = "Track record pembayaran angsuran is required")]
        [Display(Name = "Track record pembayaran angsuran")]
        public string? PaymentTrackRecord { get; set; }

        [Required(ErrorMessage = "Track Data SLIK is required")]
        [Display(Name = "Track Data SLIK")]
        public string? SlikData { get; set; }

        [Required(ErrorMessage = "Kepemilikan Kartu Kredit is required")]
        [Display(Name = "Kepemilikan Kartu Kredit")]
        public string? CreditCardOwnership { get; set; }

        // Tab 5 Parameters
        [Required(ErrorMessage = "Tenor is required")]
        [Display(Name = "Tenor")]
        public string? Tenor { get; set; }

        [Required(ErrorMessage = "Debt Service Ratio is required")]
        [Display(Name = "Debt Service Ratio")]
        public string? DebtServiceRatio { get; set; }

        // Tab 6 Parameters
        [Required(ErrorMessage = "Hasil Appraisal is required")]
        [Display(Name = "Hasil Appraisal")]
        public string? AppraisalResult { get; set; }

        [Required(ErrorMessage = "Luas Bangunan is required")]
        [Display(Name = "Luas Bangunan")]
        public string? BuildingArea { get; set; }

        [Required(ErrorMessage = "Tujuan dari pembiayaan is required")]
        [Display(Name = "Tujuan dari pembiayaan")]
        public string? FinancingPurpose { get; set; }

        [Required(ErrorMessage = "LTV is required")]
        [Display(Name = "LTV")]
        public string? LTV { get; set; }

        // Scoring Results
        public double Tab1Score { get; set; }
        public double Tab2Score { get; set; }
        public double Tab3Score { get; set; }
        public double Tab4Score { get; set; }
        public double Tab5Score { get; set; }
        public double Tab6Score { get; set; }
        public double TotalScore { get; set; }

        [Display(Name = "Risk Level")]
        public string RiskLevel { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}