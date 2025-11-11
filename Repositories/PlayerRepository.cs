using System;
using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Players;
using Com.Dotnet.Cric.Responses;
using dotnet.Requests;
using Microsoft.IdentityModel.Tokens;

namespace Com.Dotnet.Cric.Repositories
{
    public class PlayerRepository: CustomRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerRepository(AppDbContext dbContext): base(dbContext)
        {
            
        }

        public Player Create(CreateRequest createRequest)
        {
            Player player = new Player(createRequest);
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
            return player;
        }

        public Player GetByNameAndCountryIdAndDateOfBirth(string name, long countryId, DateOnly dateOfBirth)
        {
            return _dbContext.Players.FirstOrDefault(p => p.Name == name && p.CountryId == countryId && p.DateOfBirth == dateOfBirth);
        }
        
        public List<Player> GetAll(int page, int limit)
        {
            return _dbContext.Players.OrderBy(t => t.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Players.Count();
        }

        public List<Player> GetByIds(List<long> ids)
        {
            return _dbContext.Players.Where(player => ids.Contains(player.Id)).ToList();
        }
        
        public Player GetById(long id)
        {
            return _dbContext.Players.Find(id);
        }

        public void Remove(long id)
        {
            _dbContext.Players.Remove(GetById(id));
        }
        
        public List<Player> Search(string keyword, int page, int limit)
        {
            return _dbContext.Players.Where(player => player.Name.ToLower().Contains(keyword.ToLower())).OrderBy(t => t.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int SearchCount(string keyword)
        {
            return _dbContext.Players.Count(player => player.Name.ToLower().Contains(keyword.ToLower()));
        }
        
        public string GetFieldNameWithTablePrefix(string field)
        {
            var fieldName = field switch
            {
                "gameType" => "s.GameTypeId",
                "stadium" => "m.StadiumId",
                "team" => "t.Id",
                "opposingTeam" => "(CASE WHEN t.id = m.Team1Id THEN m.Team2Id ELSE m.Team1Id END)",
                "teamType" => "t.TypeId",
                "country" => "p.CountryId",
                "series" => "s.Id",
                "year" => "YEAR(m.StartTime)",
                "playerName" => "p.Name",
                _ => ""
            };

            return fieldName;
        }
        
        public string GetFieldNameForDisplay(string field)
        {
            var fieldName = field switch
            {
                "runs" => "runs",
                "balls" => "balls",
                "innings" => "innings",
                "notOuts" => "notouts",
                "fifties" => "fifties",
                "hundreds" => "hundreds",
                "highest" => "highest",
                "fours" => "fours",
                "sixes" => "sixes",
                "wickets" => "wickets",
                "maidens" => "maidens",
                "fifers" => "fifers",
                "tenWickets" => "tenWickets",
                "fielderCatches" => "fielderCatches",
                "keeperCatches" => "keeperCatches",
                "stumpings" => "stumpings",
                "runOuts" => "runOuts",
                _ => ""
            };

            return fieldName;
        }

        public StatsResponse GetBattingStats(FilterRequest filterRequest)
        {
            var statsResponse = new StatsResponse();
            var statList = new List<Dictionary<string, string>>();
            
            var countQuery = "select count(distinct p.Id) as count from BattingScores bs " +
                                "inner join MatchPlayerMaps mpm on mpm.Id = bs.MatchPlayerId " +
                                "inner join Players p on p.Id = mpm.PlayerId " +
                                "inner join Matches m on m.Id = mpm.MatchId " +
                                "inner join Series s on s.Id = m.SeriesId " +
                                "inner join Stadiums st on st.Id = m.StadiumId " +
                                "inner join Teams t on t.Id = mpm.TeamId";
            
            var query = "select p.Id as playerId, p.Name AS name, sum(bs.Runs) AS runs, count(0) AS innings, sum(bs.Balls) AS balls, sum(bs.Fours) AS fours, sum(bs.Sixes) AS sixes, max(bs.Runs) AS highest, count((case when (bs.DismissalModeId is null) then 1 end)) AS notouts, count((case when ((bs.Runs >= 50) and (bs.Runs < 100)) then 1 end)) AS fifties, count((case when ((bs.Runs >= 100)) then 1 end)) AS hundreds from BattingScores bs " +
                    "inner join MatchPlayerMaps mpm on mpm.Id = bs.MatchPlayerId " +
                    "inner join Players p on p.Id = mpm.PlayerId " +
                    "inner join Matches m on m.Id = mpm.MatchId " +
                    "inner join Series s on s.Id = m.SeriesId " +
                    "inner join Stadiums st on st.Id = m.StadiumId " +
                    "inner join Teams t on t.Id = mpm.TeamId";
            
            var whereQueryParts = new List<string>();
            foreach (var (field, valueList) in filterRequest.Filters)
            {
                var fieldNameWithTablePrefix = GetFieldNameWithTablePrefix(field);
                if (!string.IsNullOrEmpty(fieldNameWithTablePrefix) && !valueList.IsNullOrEmpty())
                {
                    whereQueryParts.Add(fieldNameWithTablePrefix + " in (" + string.Join(", ", valueList) + ")");
                }
            }

            foreach (var (field, rangeValues) in filterRequest.RangeFilters)
            {
                var fieldNameWithTablePrefix = GetFieldNameWithTablePrefix(field);
                if (!string.IsNullOrEmpty(fieldNameWithTablePrefix) && !rangeValues.IsNullOrEmpty())
                {
                    if (rangeValues.ContainsKey("from"))
                    {
                        whereQueryParts.Add(fieldNameWithTablePrefix + " >= " + rangeValues["from"]);
                    }
                    
                    if (rangeValues.ContainsKey("to"))
                    {
                        whereQueryParts.Add(fieldNameWithTablePrefix + " <= " + rangeValues["to"]);
                    }
                }
            }
            
            if (!whereQueryParts.IsNullOrEmpty())
            {
                countQuery += " where " + string.Join(" and ", whereQueryParts);
                query += " where " + string.Join(" and ", whereQueryParts);
            }

            query += " group by p.Id, p.Name";

            var sortList = new List<string>();
            foreach (var (field, value) in filterRequest.SortMap)
            {
                var sortFieldName = GetFieldNameForDisplay(field);
                if (!sortFieldName.IsNullOrEmpty())
                {
                    sortList.Add(GetFieldNameForDisplay(field) + " " + value);
                }
            }

            if (sortList.IsNullOrEmpty())
            {
                sortList.Add(GetFieldNameForDisplay("runs") + " desc");
            }

            query += " order by " + string.Join(", ", sortList);

            query += " offset " + filterRequest.Offset + " rows fetch next " + Math.Min(30, filterRequest.Count) + " rows only";

            var countResult = ExecuteQuery(countQuery);
            statsResponse.Count = Convert.ToInt32(countResult[0].GetValueOrDefault("count", 0));

            var result = ExecuteQuery(query);
            foreach (var row in result)
            {
                var innings = Convert.ToInt32(row.GetValueOrDefault("innings", 0));
                if (innings <= 0) continue;
                var stats = new Dictionary<string, string>
                {
                    {"id", row.GetValueOrDefault("playerId", "").ToString()},
                    {"name", row.GetValueOrDefault("name", "").ToString()},
                    {"innings", innings.ToString()},
                    {"runs", row.GetValueOrDefault("runs", "").ToString()},
                    {"balls", row.GetValueOrDefault("balls", "").ToString()},
                    {"notOuts", row.GetValueOrDefault("notouts", "").ToString()},
                    {"fours", row.GetValueOrDefault("fours", "").ToString()},
                    {"sixes", row.GetValueOrDefault("sixes", "").ToString()},
                    {"highest", row.GetValueOrDefault("highest", "").ToString()},
                    {"fifties", row.GetValueOrDefault("fifties", "").ToString()},
                    {"hundreds", row.GetValueOrDefault("hundreds", "").ToString()}
                };
                
                statList.Add(stats);
            }

            statsResponse.Stats = statList;
            return statsResponse;
        }

    }
}