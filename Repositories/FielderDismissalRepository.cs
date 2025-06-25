using System;
using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class FielderDismissalRepository: CustomRepository
    {
        public FielderDismissalRepository(AppDbContext dbContext): base(dbContext)
        {
            
        }

        public List<FielderDismissal> Add(Dictionary<int, List<long>> scoreFielderMap, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var fielderDismissals = new List<FielderDismissal>();

            foreach (var scoreId in scoreFielderMap.Keys)
            {
                foreach (var playerId in scoreFielderMap[scoreId])
                {
                    fielderDismissals.Add(new FielderDismissal()
                    {
                        ScoreId = scoreId,
                        MatchPlayerId = playerToMatchPlayerMap[playerId]
                    });
                }
            }
            
            _dbContext.FielderDismissals.AddRange(fielderDismissals);
            //_dbContext.SaveChanges();
            return fielderDismissals;
        }

        public Dictionary<string, Dictionary<string, int>> GetFieldingStats(long playerId)
        {
            var statsFinal = new Dictionary<string, Dictionary<string, int>>();

            var query = "select dm.Name as dismissalMode, count(*) as count, gt.Name as gameType from FielderDismissals fd inner join MatchPlayerMaps mpm on mpm.Id = fd.MatchPlayerId inner join BattingScores bs on bs.Id = fd.ScoreId and mpm.PlayerId = :playerId inner join DismissalModes dm on dm.Id = bs.DismissalModeId inner join Matches m on m.Id = mpm.MatchId and m.IsOfficial = 1 inner join Series s on s.Id = m.SeriesId inner join Teams t on t.Id = mpm.TeamId inner join TeamTypes tt on tt.Id = t.TypeId and tt.Name = 'International' inner join GameTypes gt on gt.Id = s.GameTypeId group by gt.Name, dm.Name";
            query = query.Replace(":playerId", playerId.ToString());
            var result = ExecuteQuery(query);

            foreach (var row in result)
            {
                var gameType = row.GetValueOrDefault("gameType", "").ToString();
                var dismissalMode = row.GetValueOrDefault("dismissalMode", "").ToString();
                var count = Convert.ToInt32(row.GetValueOrDefault("count", 0));
                if (!statsFinal.ContainsKey(gameType!))
                {
                    statsFinal[gameType] = new Dictionary<string, int>();
                }

                statsFinal[gameType][dismissalMode!] = count;
            }
            
            return statsFinal;
        }

        public List<FielderDismissal> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _dbContext.FielderDismissals.Where(fd => matchPlayerIds.Contains(fd.MatchPlayerId)).ToList();
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.FielderDismissals.RemoveRange(GetByMatchPlayerIds(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}