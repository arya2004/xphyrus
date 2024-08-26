namespace EvaluationService.Dtos
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


        public ResponseDto()
        {
            IsSuccess = true;
            Message = "";
        }

        public ResponseDto(bool _isSuccess, string _message)
        {
            IsSuccess = _isSuccess;
            Message = _message;
        }
        public ResponseDto(object _result, bool _isSuccess, string _message)
        {
            Result = _result;
            IsSuccess = _isSuccess;
            Message = _message;
        }
    }
}
