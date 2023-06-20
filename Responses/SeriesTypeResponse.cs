using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class SeriesTypeResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public SeriesTypeResponse()
        {

        }

        public SeriesTypeResponse(SeriesType seriesType)
        {
            this.Id = seriesType.Id;
            this.Name = seriesType.Name;
        }
    }
}