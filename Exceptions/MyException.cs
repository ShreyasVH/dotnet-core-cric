using System;

namespace Com.Dotnet.Cric.Exceptions
{
	public class MyException : Exception
	{
		public int HttpStatusCode { get; set; } = 500;
		public string Description { get; set; }

		public MyException()
		{
			
		}

		public MyException(string description)
		{
			Description = description;
		}
	}
}