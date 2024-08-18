using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Dto.TeacherDto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class ClassroomService : IClassroomService
    {
        private readonly ApplicationDbContext _context;

        public ClassroomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> GetAsync(HttpContext httpContext, Guid id)
        {
            var responseDto = new ResponseDto();

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                responseDto.Message = "Classroom not found.";
                return responseDto;
            }

            responseDto.Result = classroom;
            responseDto.IsSuccess = true;
            return responseDto;
        }

        public async Task<ResponseDto> CreateAsync(ClassroomDTO classroomDto)
        {
            var responseDto = new ResponseDto();

            var classroom = new Classroom
            {
                Name = classroomDto.Name,
                Description = classroomDto.Description,

               
            };

            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();

            responseDto.Result = classroom;
            responseDto.IsSuccess = true;
            responseDto.Message = "Classroom created successfully.";
            return responseDto;
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, ClassroomDTO classroomDto)
        {
            var responseDto = new ResponseDto();

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                responseDto.Message = "Classroom not found.";
                return responseDto;
            }

            classroom.Name = classroomDto.Name;
            classroom.Description = classroomDto.Description;

            await _context.SaveChangesAsync();

            responseDto.Result = classroom;
            responseDto.IsSuccess = true;
            responseDto.Message = "Classroom updated successfully.";
            return responseDto;
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var responseDto = new ResponseDto();

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                responseDto.Message = "Classroom not found.";
                return responseDto;
            }

            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();

            responseDto.IsSuccess = true;
            responseDto.Message = "Classroom deleted successfully.";
            return responseDto;
        }
    }
}
