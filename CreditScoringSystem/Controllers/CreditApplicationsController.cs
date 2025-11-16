using Microsoft.AspNetCore.Mvc;
using CreditScoringSystem.Data;
using CreditScoringSystem.Models;
using CreditScoringSystem.Services;
using CreditScoringSystem.DTOs;

namespace CreditScoringSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CreditScoringService _scoringService;
        private readonly ILogger<CreditApplicationsController> _logger;

        public CreditApplicationsController(
            ApplicationDbContext context,
            CreditScoringService scoringService,
            ILogger<CreditApplicationsController> logger)
        {
            _context = context;
            _scoringService = scoringService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new credit application with automatic scoring
        /// </summary>
        /// <param name="request">Credit application data</param>
        /// <returns>Created application with calculated scores and risk level</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreditApplicationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreditApplicationResponse>> CreateCreditApplication(
            [FromBody] CreditApplicationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map request to model
                var application = new CreditApplication
                {
                    ApplicationNumber = request.ApplicationNumber,
                    Name = request.Name,
                    BirthPlace = request.BirthPlace,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    PostalCode = request.PostalCode,
                    Address = request.Address,

                    // Tab 1 Parameters
                    AgeRange = request.AgeRange,
                    AgePlusTenor = request.AgePlusTenor,
                    MaritalStatus = request.MaritalStatus,
                    Education = request.Education,

                    // Tab 2 Parameters
                    ResidenceAddressMatch = request.ResidenceAddressMatch,
                    ResidenceOwnership = request.ResidenceOwnership,
                    ResidenceDuration = request.ResidenceDuration,

                    // Tab 3 Parameters
                    CompanyCategory = request.CompanyCategory,
                    JobPosition = request.JobPosition,
                    WorkDuration = request.WorkDuration,
                    TakeHomePay = request.TakeHomePay,

                    // Tab 4 Parameters
                    BankAccount = request.BankAccount,
                    AverageBalance = request.AverageBalance,
                    PaymentTrackRecord = request.PaymentTrackRecord,
                    SlikData = request.SlikData,
                    CreditCardOwnership = request.CreditCardOwnership,

                    // Tab 5 Parameters
                    Tenor = request.Tenor,
                    DebtServiceRatio = request.DebtServiceRatio,

                    // Tab 6 Parameters
                    AppraisalResult = request.AppraisalResult,
                    BuildingArea = request.BuildingArea,
                    FinancingPurpose = request.FinancingPurpose,
                    LTV = request.LTV,

                    CreatedAt = DateTime.Now
                };

                // Calculate all scores and determine risk level
                _scoringService.CalculateAllScores(application);

                // Save to database
                _context.CreditApplications.Add(application);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Credit application created successfully via API. ID: {Id}, ApplicationNumber: {ApplicationNumber}",
                    application.Id, application.ApplicationNumber);

                // Return response
                var response = new CreditApplicationResponse
                {
                    Id = application.Id,
                    ApplicationNumber = application.ApplicationNumber,
                    Name = application.Name,
                    Tab1Score = application.Tab1Score,
                    Tab2Score = application.Tab2Score,
                    Tab3Score = application.Tab3Score,
                    Tab4Score = application.Tab4Score,
                    Tab5Score = application.Tab5Score,
                    Tab6Score = application.Tab6Score,
                    TotalScore = application.TotalScore,
                    RiskLevel = application.RiskLevel,
                    CreatedAt = application.CreatedAt
                };

                return CreatedAtAction(nameof(GetCreditApplication), new { id = application.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating credit application via API");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Gets a credit application by ID
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>Credit application details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreditApplicationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreditApplicationResponse>> GetCreditApplication(int id)
        {
            var application = await _context.CreditApplications.FindAsync(id);

            if (application == null)
            {
                return NotFound(new { message = $"Application with ID {id} not found." });
            }

            var response = new CreditApplicationResponse
            {
                Id = application.Id,
                ApplicationNumber = application.ApplicationNumber,
                Name = application.Name,
                Tab1Score = application.Tab1Score,
                Tab2Score = application.Tab2Score,
                Tab3Score = application.Tab3Score,
                Tab4Score = application.Tab4Score,
                Tab5Score = application.Tab5Score,
                Tab6Score = application.Tab6Score,
                TotalScore = application.TotalScore,
                RiskLevel = application.RiskLevel,
                CreatedAt = application.CreatedAt
            };

            return Ok(response);
        }
    }

}