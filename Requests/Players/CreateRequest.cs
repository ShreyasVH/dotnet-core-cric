using System;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Requests.Players
{
    public class CreateRequest
    {
        public string Name { get; set; }
        public long CountryId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Image { get; set; }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new BadRequestException("Invalid name");
            }
            
            if (null == DateOfBirth)
            {
                throw new BadRequestException("Invalid date of birth");
            }
        }
    }
}