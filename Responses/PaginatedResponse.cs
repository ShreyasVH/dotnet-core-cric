namespace Com.Dotnet.Cric.Responses
{
	class PaginatedResponse<T>
	{
		public int TotalCount { get; set; }
		public List<T> Items { get; set; }
		public int Page { get; set; }
		public int Limit { get; set; }

		public PaginatedResponse()
		{

		}

		public PaginatedResponse(int totalCount, List<T> items, int page, int limit)
		{
			TotalCount = totalCount;
			Items = items;
			Page = page;
			Limit = limit;
		}
	}
}