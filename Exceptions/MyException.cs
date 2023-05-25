using System;

namespace Com.Dotnet.Cric.Exceptions
{
	public class MyException : Exception
	{
		public int HttpStatusCode { get; set; } = 500;
		public string Description { get; set; }

		public MyException()
		{
			Console.WriteLine("Empty MyException");
		}

		public MyException(string description)
		{
			Console.WriteLine("String MyException");
			Description = description;
		}
	}
}