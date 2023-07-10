using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class ResultTypeResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ResultTypeResponse()
        {

        }

        public ResultTypeResponse(ResultType resultType)
        {
            Id = resultType.Id;
            Name = resultType.Name;
        }
    }
}