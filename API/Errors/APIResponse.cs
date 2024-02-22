

namespace API.Errors
{
    public class APIResponse
    {
        
        public APIResponse(int statuscode, string message = null)
        {
            StatusCode = statuscode;
            Message = message?? GetDefaultMessage(statuscode);

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessage(int statuscode){
            return statuscode switch{
                400=>"You have made a Bad Request",
                401=>"You are Not Authorized",
                404=>"Resource Not Found",
                500=>"You have made a Server Order",
                _ =>  null
            };
        }
    }
}