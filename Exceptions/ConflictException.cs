namespace Com.Dotnet.Cric.Exceptions
{
	public class ConflictException : MyException
	{
		public ConflictException()
		{
			
		}

		public ConflictException(string entity) : base(entity + " already exists")
		{
			HttpStatusCode = 409;
		}
	}
}