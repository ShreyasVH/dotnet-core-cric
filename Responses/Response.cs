using System;

namespace Com.Dotnet.Cric.Responses
{
    public class Response
    {
        public bool Success { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }
        // Add more properties as needed

        public Response()
        {

        }

        public Response(Object data)
        {
            Success = true;
            Data = data;
            Message = "";
        }

        public Response(string message, bool success = false)
        {
            Success = success;
            Message = message;
        }
    }
}
