using System;
using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;
using Microsoft.EntityFrameworkCore;

namespace Com.Dotnet.Cric.Repositories
{
    public class BattingScoreRepository: CustomRepository
    {
        public BattingScoreRepository(AppDbContext dbContext): base(dbContext)
        {
            
        }

        public List<BattingScore> Add(List<BattingScoreRequest> battingScoreRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var battingScores = battingScoreRequests.Select(battingScoreRequest => new BattingScore(battingScoreRequest, playerToMatchPlayerMap)).ToList();
            _dbContext.BattingScores.AddRange(battingScores);
            //_dbContext.SaveChanges();
            return battingScores;
        }

        public Dictionary<string, Dictionary<string, int>> GetDismissalStats(long playerId)
        {
            var stats = new Dictionary<string, Dictionary<string, int>>();

            var query = "SELECT dm.Name AS dismissalMode, COUNT(*) AS count, gt.Name as gameType FROM BattingScores bs INNER JOIN MatchPlayerMaps mpm on mpm.Id = bs.MatchPlayerId inner join DismissalModes dm ON mpm.PlayerId = :playerId AND bs.DismissalModeId IS NOT NULL and dm.Id = bs.DismissalModeId and dm.Name != 'Retired Hurt' inner join Matches m on m.Id = mpm.MatchId and m.IsOfficial = 1 inner join Series s on s.Id = m.SeriesId inner join Teams t on t.Id = mpm.TeamId inner join TeamTypes tt on tt.Id = t.TypeId and tt.Name = 'International' inner join GameTypes gt on gt.Id = s.GameTypeId GROUP BY gt.Name, dm.Name";
            query = query.Replace(":playerId", playerId.ToString());
            var result = ExecuteQuery(query);

            foreach (var row in result)
            {
                var gameType = row.GetValueOrDefault("gameType", "").ToString();
                var dismissalMode = row.GetValueOrDefault("dismissalMode", "").ToString();
                var count = Convert.ToInt32(row.GetValueOrDefault("count", 0));
                if (!stats.ContainsKey(gameType!))
                {
                    stats[gameType] = new Dictionary<string, int>();
                }

                stats[gameType][dismissalMode!] = count;
            }

            return stats;
        }
        
        public Dictionary<string, Dictionary<string, int>> GetBattingStats(long playerId)
        {
            var statsFinal = new Dictionary<string, Dictionary<string, int>>();

            var query = "SELECT COUNT(*) AS innings, SUM(Runs) AS runs, SUM(Balls) AS balls, SUM(Fours) AS fours, SUM(Sixes) AS sixes, MAX(Runs) AS highest, gt.Name as gameType, count(CASE WHEN (bs.Runs >= 50 and bs.Runs < 100) then 1 end) as fifties, count(CASE WHEN (bs.Runs >= 100 and bs.Runs < 200) then 1 end) as hundreds, count(CASE WHEN (bs.Runs >= 200 and bs.Runs < 300) then 1 end) as twoHundreds, count(CASE WHEN (bs.Runs >= 300 and bs.Runs < 400) then 1 end) as threeHundreds, count(CASE WHEN (bs.Runs >= 400 and bs.Runs < 500) then 1 end) as fourHundreds FROM BattingScores bs inner join MatchPlayerMaps mpm on mpm.PlayerId = :playerId and  mpm.Id = bs.MatchPlayerId inner join Matches m on m.Id = mpm.MatchId and m.IsOfficial = 1 inner join Series s on s.Id = m.SeriesId inner join Teams t on t.Id = mpm.TeamId inner join TeamTypes tt on tt.Id = t.TypeId and tt.Name = 'International' inner join GameTypes gt on gt.Id = s.GameTypeId group by gt.Name";
            query = query.Replace(":playerId", playerId.ToString());
            var result = ExecuteQuery(query);

            foreach (var row in result)
            {
                var innings = Convert.ToInt32(row.GetValueOrDefault("innings", 0));
                if (innings > 0)
                {
                    var stats = new Dictionary<string, int>();

                    stats["innings"] = innings;
                    stats["runs"] = Convert.ToInt32(row.GetValueOrDefault("runs", 0));
                    stats["balls"] = Convert.ToInt32(row.GetValueOrDefault("balls", 0));
                    stats["fours"] = Convert.ToInt32(row.GetValueOrDefault("fours", 0));
                    stats["sixes"] = Convert.ToInt32(row.GetValueOrDefault("sixes", 0));
                    stats["highest"] = Convert.ToInt32(row.GetValueOrDefault("highest", 0));
                    stats["fifties"] = Convert.ToInt32(row.GetValueOrDefault("fifties", 0));
                    stats["hundreds"] = Convert.ToInt32(row.GetValueOrDefault("hundreds", 0));
                    stats["twoHundreds"] = Convert.ToInt32(row.GetValueOrDefault("twoHundreds", 0));
                    stats["threeHundreds"] = Convert.ToInt32(row.GetValueOrDefault("threeHundreds", 0));
                    stats["fourHundreds"] = Convert.ToInt32(row.GetValueOrDefault("fourHundreds", 0));

                    var gameType = row.GetValueOrDefault("gameType", "").ToString();
                    statsFinal[gameType!] = stats;
                }
            }
            
            return statsFinal;
        }

        public List<BattingScore> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _dbContext.BattingScores.Where(bs => matchPlayerIds.Contains(bs.MatchPlayerId)).ToList();
        }

        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.BattingScores.RemoveRange(GetByMatchPlayerIds(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}