using System;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
	public class TourResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set;  }

		public TourResponse()
		{

		}

		public TourResponse(Tour tour)
		{
			this.Id = tour.Id;
			this.Name = tour.Name;
			this.StartTime = tour.StartTime;
		}
	}
}