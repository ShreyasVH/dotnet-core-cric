namespace Com.Dotnet.Cric.Exceptions
{
	public class BadRequestException : MyException
	{
		public BadRequestException()
		{
			
		}

		public BadRequestException(string description) : base(description)
		{
			HttpStatusCode = 400;
		}
	}
}