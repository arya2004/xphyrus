using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class CodingQuestionService : ICodingQuestionService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingQuestionService"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public CodingQuestionService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<ResponseDto> GetAsync(HttpContext httpContext, Guid codingQuestionId)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext), "HTTP context cannot be null.");
            }

            if (codingQuestionId == Guid.Empty)
            {
                return new ResponseDto(false, "Invalid coding question ID.");
            }

            try
            {
                CodingQuestion? question = await _applicationDbContext.CodingQuestions
                    .FirstOrDefaultAsync(q => q.CodingQuestionId == codingQuestionId);

                if (question == null)
                {
                    return new ResponseDto(false, "Coding question not found.");
                }

                return new ResponseDto(question, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while fetching the coding question.");
            }
        }

        public async Task<ResponseDto> Create(HttpContext httpContext, CodingQuestion codingQuestion)
        {
            if (codingQuestion == null)
            {
                throw new ArgumentNullException(nameof(codingQuestion));
            }

            try
            {
                await _applicationDbContext.CodingQuestions.AddAsync(codingQuestion);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Coding question added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while adding the coding question. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Delete(HttpContext httpContext, Guid codingQuestionId)
        {
            if (codingQuestionId == Guid.Empty)
            {
                return new ResponseDto(false, "Invalid coding question ID.");
            }

            try
            {
                var question = await _applicationDbContext.CodingQuestions
                    .FirstOrDefaultAsync(q => q.CodingQuestionId == codingQuestionId);

                if (question == null)
                {
                    return new ResponseDto(false, "Coding question not found.");
                }

                _applicationDbContext.CodingQuestions.Remove(question);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Coding question deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while deleting the coding question.");
            }
        }

        public async Task<ResponseDto> Edit(HttpContext httpContext, CodingQuestion codingQuestion)
        {
            if (codingQuestion == null)
            {
                throw new ArgumentNullException(nameof(codingQuestion));
            }

            try
            {
                _applicationDbContext.CodingQuestions.Update(codingQuestion);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Coding question updated successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while updating the coding question. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                var questions = await _applicationDbContext.CodingQuestions.ToListAsync();

                if (questions == null || questions.Count == 0)
                {
                    return new ResponseDto(false, "No coding questions found.");
                }

                return new ResponseDto(questions, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving coding questions.");
            }
        }

        public async Task<ResponseDto> GetAllByDifficulty(HttpContext httpContext, Difficulty difficulty)
        {
            try
            {
                var questions = await _applicationDbContext.CodingQuestions
                    .Where(q => q.Difficulty == difficulty)
                    .ToListAsync();

                if (questions == null || questions.Count == 0)
                {
                    return new ResponseDto(false, $"No coding questions found for difficulty level: {difficulty}.");
                }

                return new ResponseDto(questions, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving coding questions by difficulty.");
            }
        }
    }
}
