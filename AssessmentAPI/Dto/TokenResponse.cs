namespace AssessmentAPI.Dto
{
    /// <summary>
    /// Represents a DTO (Data Transfer Object) for token response.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Gets or sets the JWT token string.
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Default constructor for TokenResponse.
        /// </summary>
        public TokenResponse()
        {
            token = string.Empty;
        }

        /// <summary>
        /// Constructor for TokenResponse with token parameter.
        /// </summary>
        /// <param name="_token">JWT token string.</param>
        public TokenResponse(string _token)
        {
            token = _token;
        }
    }
}
