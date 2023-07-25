using System;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
	public class TourMiniResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set;  }

		public TourMiniResponse()
		{

		}

		public TourMiniResponse(Tour tour)
		{
			this.Id = tour.Id;
			this.Name = tour.Name;
			this.StartTime = tour.StartTime;
		}
	}
}