using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class ExtrasTypeResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ExtrasTypeResponse(ExtrasType extrasType)
        {
            this.Id = extrasType.Id;
            this.Name = extrasType.Name;
        }
    }
}