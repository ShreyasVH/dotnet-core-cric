using System;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Requests.Teams
{
    public class CreateRequest
    {
        public string Name { get; set; }
        public long CountryId { get; set; }
        public int TypeId { get; set; }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new BadRequestException("Invalid name");
            }
        }
    }
}
