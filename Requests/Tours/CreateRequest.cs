using Com.Dotnet.Cric.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Dotnet.Cric.Requests.Tours
{
    public class CreateRequest
    {
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new BadRequestException("Invalid name");
            }
            Console.WriteLine(StartTime);

            if (null == StartTime)
            {
                throw new BadRequestException("Invalid start time");
            }
        }
    }
}
