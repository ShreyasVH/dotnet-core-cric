using System;
using System.Collections.Generic;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Requests.Series
{
    public class CreateRequest
    {
        public string Name { get; set; }
        public long HomeCountryId { get; set; }
        public int TourId { get; set; }
        public int TypeId { get; set; }
        public int GameTypeId { get; set; }
        public DateTime? StartTime { get; set; }
        public List<long> Teams { get; set; }
        
        public List<long> ManOfTheSeriesList { get; set; }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new BadRequestException("Invalid name");
            }

            if (null == Teams || Teams.Count < 2)
            {
                throw new BadRequestException("Invalid teams");
            }

            if (null == StartTime)
            {
                throw new BadRequestException("Invalid start time");
            }
        }
    }
}