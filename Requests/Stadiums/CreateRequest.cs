using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Requests.Stadiums
{
    public class CreateRequest
    {
        public string Name { get; set; }
        public string City{ get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new BadRequestException("Invalid name");
            }

            if (String.IsNullOrEmpty(City))
            {
                throw new BadRequestException("Invalid city");
            }
        }
    }
}
