using System;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Requests.Countries
{
    public class CreateRequest
    {
        public string Name { get; set; }

        public void Validate()
        {
            if(String.IsNullOrEmpty(Name))
            {
                throw new BadRequestException("Invalid name");
            }
        }
    }
}
