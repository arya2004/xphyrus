﻿using NexusAPI.Dto;
using NexusAPI.Models;

namespace NexusAPI.Service.IService
{
    public interface ICodingQuestionService
    {

        Task<ResponseDto> GetAsync(HttpContext httpContext, Guid codingQuestionId);


        Task<ResponseDto> Create(HttpContext httpContext, CodingQuestion codingQuestion, Guid testId);


        Task<ResponseDto> Delete(HttpContext httpContext, Guid codingQuestionId);


        Task<ResponseDto> Edit(HttpContext httpContext, CodingQuestion codingQuestion);


        Task<ResponseDto> GetAll(HttpContext httpContext);


        Task<ResponseDto> GetAllForTest(HttpContext httpContext, Guid testId);
    }

}
