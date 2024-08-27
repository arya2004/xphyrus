using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;

using NexusAPI.Models;
using NexusAPI.Service.IService;
using System.Security.Claims;

namespace NexusAPI.Service
{
    public class ClassroomService : IClassroomService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ClassroomService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<ResponseDto> Create(HttpContext httpContext, Classroom classroom)
        {
            if (classroom == null)
            {
                throw new ArgumentNullException(nameof(classroom));
            }

            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                classroom.Teacher = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (classroom.Teacher == null)
                {
                    return new ResponseDto(false, "Teacher not found.");
                }

                await _applicationDbContext.Classrooms.AddAsync(classroom);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Classroom added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while adding the classroom. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Delete(HttpContext httpContext, Guid classroomId)
        {
            try
            {
                var classroom = await _applicationDbContext.Classrooms.FirstOrDefaultAsync(c => c.ClassroomId == classroomId);
                if (classroom == null)
                {
                    return new ResponseDto(false, "Classroom not found.");
                }

                _applicationDbContext.Classrooms.Remove(classroom);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Classroom deleted successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while deleting the classroom. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Edit(HttpContext httpContext, Classroom classroom)
        {
            if (classroom == null)
            {
                throw new ArgumentNullException(nameof(classroom));
            }

            try
            {
                var existingClassroom = await _applicationDbContext.Classrooms.FirstOrDefaultAsync(c => c.ClassroomId == classroom.ClassroomId);
                if (existingClassroom == null)
                {
                    return new ResponseDto(false, "Classroom not found.");
                }

                _applicationDbContext.Entry(existingClassroom).CurrentValues.SetValues(classroom);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Classroom edited successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while editing the classroom. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Get(HttpContext httpContext, Guid classroomId)
        {
            try
            {
                var classroom = await _applicationDbContext.Classrooms.FirstOrDefaultAsync(c => c.ClassroomId == classroomId);
                if (classroom == null)
                {
                    return new ResponseDto(false, "Classroom not found.");
                }

                return new ResponseDto(classroom, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving the classroom. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                var classrooms = await _applicationDbContext.Classrooms.ToListAsync();
                if (classrooms == null || classrooms.Count == 0)
                {
                    return new ResponseDto(false, "No classrooms found.");
                }

                return new ResponseDto(classrooms, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving classrooms. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetMy(HttpContext httpContext)
        {
            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                var classrooms = await _applicationDbContext.Classrooms.Where(c => c.Teacher != null && c.Teacher.Id == userId).ToListAsync();
                if (classrooms == null || classrooms.Count == 0)
                {
                    return new ResponseDto(false, "No classrooms found for the teacher.");
                }

                return new ResponseDto(classrooms, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving your classrooms. Please try again later.");
            }
        }


    }
}
