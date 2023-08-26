//using Microsoft.EntityFrameworkCore;
//using StackExchange.Redis;
//using System.Text.Json;
//using Xphyrus.CreationAPI.Data;
//using Xphyrus.CreationAPI.Models;
//using Xphyrus.CreationAPI.Models.Dto;
//using Xphyrus.CreationAPI.Service.IService;

//namespace Xphyrus.CreationAPI.Service
//{
//    public class SpacesService : ISpacesService
//    {   
//        private readonly IDatabase _database;
//        private readonly ApplicatioDbContext _applicatioDbContext;
//        public SpacesService(IConnectionMultiplexer redis, ApplicatioDbContext applicatioDbContext)
//        {
//            _database = redis.GetDatabase();
//            _applicatioDbContext = applicatioDbContext;
//        }

//        public async Task<Assesment> CreateSpace(Assesment spaces)
//        {
//            _applicatioDbContext.spaces.AddAsync(spaces);
//            _applicatioDbContext.SaveChanges();
//            Assesment spaces1 = await _applicatioDbContext.spaces.FirstOrDefaultAsync(i => i.Id == spaces.Id);
//            return spaces1;
            
//        }

//        public async Task<bool> DeleteSpace(string spackId)
//        {
//            Assesment? spaces = await _applicatioDbContext.spaces.FirstOrDefaultAsync(u => u.Id == spackId);
//            if(spaces == null) return false;
//            _applicatioDbContext.spaces.Remove(spaces);
//            _applicatioDbContext.SaveChanges();
//            return true;
//        }

//        public Task<Assesment> GetAdminSpaces(string spackId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Assesment> GetStudentSpaces(string spackId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Assesment> UpdateSpace(Assesment spaces)
//        {
//            throw new NotImplementedException();
//        }

//        Task<AdminSpacesDto> ISpacesService.GetAdminSpaces(string spackId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
