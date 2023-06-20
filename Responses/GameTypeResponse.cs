using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class GameTypeResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public GameTypeResponse()
        {

        }

        public GameTypeResponse(GameType gameType)
        {
            this.Id = gameType.Id;
            this.Name = gameType.Name;
        }
    }
}