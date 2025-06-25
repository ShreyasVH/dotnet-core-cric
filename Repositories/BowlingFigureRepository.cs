using System;
using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class BowlingFigureRepository: CustomRepository
    {
        public BowlingFigureRepository(AppDbContext dbContext): base(dbContext)
        {
        }

        public List<BowlingFigure> Add(List<BowlingFigureRequest> bowlingFigureRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var bowlingFigures = bowlingFigureRequests.Select(bowlingFigureRequest =>
                new BowlingFigure(bowlingFigureRequest, playerToMatchPlayerMap)).ToList();
            _dbContext.BowlingFigures.AddRange(bowlingFigures);
            //_dbContext.SaveChanges();
            return bowlingFigures;
        }

        public Dictionary<string, Dictionary<string, int>> GetBowlingStats(long playerId)
        {
            var statsFinal = new Dictionary<string, Dictionary<string, int>>();

            var query = "SELECT COUNT(*) AS innings, SUM(Balls) AS balls, SUM(Maidens) AS maidens, SUM(Runs) AS runs, SUM(Wickets) AS wickets, gt.Name AS gameType, COUNT(CASE WHEN (bf.Wickets >= 5 and bf.Wickets < 10) then 1 end) as fifers, COUNT(CASE WHEN (bf.Wickets = 10) then 1 end) as tenWickets FROM BowlingFigures bf inner join MatchPlayerMaps mpm on mpm.Id = bf.MatchPlayerId and mpm.PlayerId = :playerId INNER JOIN matches m ON m.Id = mpm.MatchId INNER JOIN Series s ON s.Id = m.SeriesId and m.IsOfficial = 1 inner join Teams t on t.Id = mpm.TeamId inner join TeamTypes tt on tt.Id = t.TypeId and tt.Name = 'INTERNATIONAL' inner join GameTypes gt on gt.Id = s.GameTypeId GROUP BY gt.Name";
            query = query.Replace(":playerId", playerId.ToString());
            var result = ExecuteQuery(query);

            foreach (var row in result)
            {
                var gameType = row.GetValueOrDefault("gameType", "").ToString();
                var innings = Convert.ToInt32(row.GetValueOrDefault("innings", 0));
                if (innings > 0)
                {
                    var stats = new Dictionary<string, int>
                    {
                        {"innings", innings},
                        {"runs", Convert.ToInt32(row.GetValueOrDefault("runs", 0))},
                        {"balls", Convert.ToInt32(row.GetValueOrDefault("balls", 0))},
                        {"maidens", Convert.ToInt32(row.GetValueOrDefault("maidens", 0))},
                        {"wickets", Convert.ToInt32(row.GetValueOrDefault("wickets", 0))},
                        {"fifers", Convert.ToInt32(row.GetValueOrDefault("fifers", 0))},
                        {"tenWickets", Convert.ToInt32(row.GetValueOrDefault("tenWickets", 0))}
                    };


                    statsFinal[gameType!] = stats;
                }
            }
            
            return statsFinal;
        }

        public List<BowlingFigure> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _dbContext.BowlingFigures.Where(bf => matchPlayerIds.Contains(bf.MatchPlayerId)).ToList();
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.BowlingFigures.RemoveRange(GetByMatchPlayerIds(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}