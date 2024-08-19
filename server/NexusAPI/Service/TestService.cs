using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestService"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public TestService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<ResponseDto> GetAsync(HttpContext httpContext, Guid testId)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext), "HTTP context cannot be null.");
            }

            if (testId == Guid.Empty)
            {
                return new ResponseDto(false, "Invalid test ID.");
            }

            try
            {
                Test? test = await _applicationDbContext.Tests
                    .FirstOrDefaultAsync(t => t.TestId == testId);

                if (test == null)
                {
                    return new ResponseDto(false, "Test not found.");
                }

                return new ResponseDto(test, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while fetching the test.");
            }
        }

        public async Task<ResponseDto> Create(HttpContext httpContext, Test test, Guid classroomId)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            try
            {
                var classroom = await _applicationDbContext.Classrooms.FirstOrDefaultAsync(c => c.ClassroomId == classroomId);
                if (classroom == null)
                {
                    return new ResponseDto(false, "Classroom not found.");
                }

                test.Classroom = classroom;
                await _applicationDbContext.Tests.AddAsync(test);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Test added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while adding the test. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Delete(HttpContext httpContext, Guid testId)
        {
            if (testId == Guid.Empty)
            {
                return new ResponseDto(false, "Invalid test ID.");
            }

            try
            {
                var test = await _applicationDbContext.Tests.FirstOrDefaultAsync(t => t.TestId == testId);
                if (test == null)
                {
                    return new ResponseDto(false, "Test not found.");
                }

                _applicationDbContext.Tests.Remove(test);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Test deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while deleting the test. Please try again later.");
            }
        }

        public async Task<ResponseDto> Edit(HttpContext httpContext, Test test)
        {
            if (test == null)
            {
                return new ResponseDto(false, "Test data is null.");
            }

            try
            {
                var existingTest = await _applicationDbContext.Tests.FirstOrDefaultAsync(t => t.TestId == test.TestId);
                if (existingTest == null)
                {
                    return new ResponseDto(false, "Test not found.");
                }

                existingTest.Title = test.Title;
                existingTest.Description = test.Description;
                existingTest.StartDate = test.StartDate;
                existingTest.EndDate = test.EndDate;
                existingTest.Duration = test.Duration;
                existingTest.Classroom = test.Classroom;

                _applicationDbContext.Tests.Update(existingTest);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Test updated successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while updating the test. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                var tests = await _applicationDbContext.Tests.ToListAsync();

                if (tests == null || tests.Count == 0)
                {
                    return new ResponseDto(false, "No tests found.");
                }

                return new ResponseDto(tests, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving tests. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetAllForClassroom(HttpContext httpContext, Guid classroomId)
        {
            try
            {
                var tests = await _applicationDbContext.Tests
                    .Where(t => t.Classroom.ClassroomId == classroomId)
                    .ToListAsync();

                if (tests == null || tests.Count == 0)
                {
                    return new ResponseDto(false, "No tests found for the specified classroom.");
                }

                return new ResponseDto(tests, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving tests. Please try again later.");
            }
        }

    }
}
