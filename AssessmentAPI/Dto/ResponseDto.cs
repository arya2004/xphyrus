namespace AssessmentAPI.Dto
{
    /// <summary>
    /// Represents a response DTO (Data Transfer Object) for API responses.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Gets or sets the result object of the response.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Default constructor for ResponseDto.
        /// </summary>
        public ResponseDto()
        {
            IsSuccess = true;
            Message = "";
        }

        /// <summary>
        /// Constructor for ResponseDto with custom success and message parameters.
        /// </summary>
        /// <param name="_isSuccess">Boolean indicating success status.</param>
        /// <param name="_message">Message associated with the response.</param>
        public ResponseDto(bool _isSuccess, string _message)
        {
            IsSuccess = _isSuccess;
            Message = _message;
        }

        /// <summary>
        /// Constructor for ResponseDto with result, success status, and message parameters.
        /// </summary>
        /// <param name="_result">Result object of the response.</param>
        /// <param name="_isSuccess">Boolean indicating success status.</param>
        /// <param name="_message">Message associated with the response.</param>
        public ResponseDto(object _result, bool _isSuccess, string _message)
        {
            Result = _result;
            IsSuccess = _isSuccess;
            Message = _message;
        }
    }
}
