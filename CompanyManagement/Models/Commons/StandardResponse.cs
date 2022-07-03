namespace CompanyManagement.Models.Commons
{
    public class StandardResponse
    {
        public StandardResponse()
        {
            Status = ResponseStatus.SUCCESS;
        }

        public StandardResponse(ResponseStatus status)
        {
            Status = status;
        }
        public StandardResponse(ResponseStatus status,string message)
        {
            Status = status;
            Message = message;
        }

        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public enum ResponseStatus : int
        {
            SUCCESS = 0,
            ERROR = 1
        }
    }
}
