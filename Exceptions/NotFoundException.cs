namespace Com.Dotnet.Cric.Exceptions
{
	public class NotFoundException : MyException
	{
		public NotFoundException()
		{
			
		}

		public NotFoundException(string entity) : base(entity + " not found")
		{
			HttpStatusCode = 404;
		}
	}
}