using System;
using Microsoft.EntityFrameworkCore;

using Com.Dotnet.Cric.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Com.Dotnet.Cric.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define your entities (models) as DbSet properties
        public DbSet<Country> Countries { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<TeamType> TeamTypes { get; set; }
        public DbSet<Team> Teams { get; set;  }

        public DbSet<Tour> Tours { get; set; }
        
        public DbSet<Player> Players { get; set; }

        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesType> SeriesTypes { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<SeriesTeamsMap> SeriesTeamsMap { get; set; }
        public DbSet<ManOfTheSeries> ManOfTheSeries { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<WinMarginType> WinMarginTypes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchPlayerMap> MatchPlayerMaps { get; set; }
        
        public DbSet<DismissalMode> DismissalModes { get; set; }
        public DbSet<BattingScore> BattingScores { get; set; }
        public DbSet<BowlingFigure> BowlingFigures { get; set; }
        public DbSet<FielderDismissal> FielderDismissals { get; set; }
        public DbSet<ExtrasType> ExtrasTypes { get; set; }
        public DbSet<Extras> Extras { get; set; }
        public DbSet<Captain> Captains { get; set; }
        
        public DbSet<WicketKeeper> WicketKeepers { get; set; }
        public DbSet<ManOfTheMatch> ManOfTheMatch { get; set; }
        
        public DbSet<Total> Totals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure any additional model-related settings or relationships here
            // For example:
            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.Products)
            //     .WithOne(p => p.User)
            //     .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Stadium>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // Specify the onDelete property

            modelBuilder.Entity<Stadium>()
                .HasIndex(s => new { s.Name, s.CountryId })
                .IsUnique();

            modelBuilder.Entity<Stadium>()
                .HasIndex(s => s.CountryId)
                .HasDatabaseName("country");

            modelBuilder.Entity<TeamType>()
                .HasIndex(tt => new { tt.Name })
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Country)
                .WithMany()
                .HasForeignKey(t => t.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasIndex(t => new { t.Name, t.CountryId, t.TypeId })
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasIndex(s => s.CountryId)
                .HasDatabaseName("country");

            modelBuilder.Entity<Tour>()
               .HasIndex(t => new { t.Name, t.StartTime })
               .IsUnique();
            
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                v => new DateTime(v.Year, v.Month, v.Day),
                v => new DateOnly(v.Year, v.Month, v.Day));

            modelBuilder.Entity<Player>()
                .Property(p => p.DateOfBirth)
                .HasConversion(dateOnlyConverter);
            
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Player>()
                .HasIndex(p => new { p.Name, p.CountryId, p.DateOfBirth })
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.CountryId)
                .HasDatabaseName("country");
            
            modelBuilder.Entity<SeriesType>()
                .HasIndex(st => new { st.Name })
                .IsUnique();
            
            modelBuilder.Entity<GameType>()
                .HasIndex(gt => new { gt.Name })
                .IsUnique();

            modelBuilder.Entity<Series>()
                .HasOne(s => s.HomeCountry)
                .WithMany()
                .HasForeignKey(s => s.HomeCountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Series>()
                .HasOne(s => s.Tour)
                .WithMany()
                .HasForeignKey(s => s.TourId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Series>()
                .HasOne(s => s.Type)
                .WithMany()
                .HasForeignKey(s => s.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Series>()
                .HasOne(s => s.GameType)
                .WithMany()
                .HasForeignKey(s => s.GameTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Series>()
                .HasIndex(t => new { t.Name, t.TourId, t.GameTypeId })
                .IsUnique();

            modelBuilder.Entity<Series>()
                .HasIndex(s => s.HomeCountryId)
                .HasDatabaseName("HomeCountry");
            
            modelBuilder.Entity<Series>()
                .HasIndex(s => s.TourId)
                .HasDatabaseName("Tour");
            
            modelBuilder.Entity<Series>()
                .HasIndex(s => s.TypeId)
                .HasDatabaseName("Type");
            
            modelBuilder.Entity<Series>()
                .HasIndex(s => s.GameTypeId)
                .HasDatabaseName("GameType");
            
            modelBuilder.Entity<SeriesTeamsMap>()
                .HasOne(s => s.Series)
                .WithMany()
                .HasForeignKey(s => s.SeriesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeriesTeamsMap>()
                .HasOne(s => s.Team)
                .WithMany()
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeriesTeamsMap>()
                .HasIndex(s => new { s.SeriesId, s.TeamId })
                .IsUnique();

            modelBuilder.Entity<SeriesTeamsMap>()
                .HasIndex(s => s.SeriesId)
                .HasDatabaseName("Series");
            
            modelBuilder.Entity<SeriesTeamsMap>()
                .HasIndex(s => s.TeamId)
                .HasDatabaseName("Team");
            
            modelBuilder.Entity<ManOfTheSeries>()
                .HasOne(m => m.Series)
                .WithMany()
                .HasForeignKey(m => m.SeriesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ManOfTheSeries>()
                .HasOne(m => m.Player)
                .WithMany()
                .HasForeignKey(m => m.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ManOfTheSeries>()
                .HasIndex(m => new { m.SeriesId, m.PlayerId })
                .IsUnique();

            modelBuilder.Entity<ManOfTheSeries>()
                .HasIndex(m => m.SeriesId)
                .HasDatabaseName("Series");
            
            modelBuilder.Entity<ManOfTheSeries>()
                .HasIndex(m => m.PlayerId)
                .HasDatabaseName("Player");
            
            modelBuilder.Entity<ResultType>()
                .HasIndex(rt => new { rt.Name })
                .IsUnique();
            
            modelBuilder.Entity<WinMarginType>()
                .HasIndex(wmt => new { wmt.Name })
                .IsUnique();

            modelBuilder.Entity<Match>()
                .HasIndex(m => new { m.StadiumId, m.StartTime })
                .IsUnique();
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.SeriesId)
                .HasDatabaseName("Series");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Series)
                .WithMany()
                .HasForeignKey(m => m.SeriesId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.Team1Id)
                .HasDatabaseName("Team1");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team1)
                .WithMany()
                .HasForeignKey(m => m.Team1Id)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.Team2Id)
                .HasDatabaseName("Team2");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team2)
                .WithMany()
                .HasForeignKey(m => m.Team2Id)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.TossWinnerId)
                .HasDatabaseName("TossWinner");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.TossWinner)
                .WithMany()
                .HasForeignKey(m => m.TossWinnerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.BatFirstId)
                .HasDatabaseName("BatFirst");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.BatFirst)
                .WithMany()
                .HasForeignKey(m => m.BatFirstId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.ResultTypeId)
                .HasDatabaseName("ResultType");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.ResultType)
                .WithMany()
                .HasForeignKey(m => m.ResultTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.WinnerId)
                .HasDatabaseName("Winner");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Winner)
                .WithMany()
                .HasForeignKey(m => m.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.WinMarginTypeId)
                .HasDatabaseName("WinMarginType");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.WinMarginType)
                .WithMany()
                .HasForeignKey(m => m.WinMarginTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasIndex(m => m.StadiumId)
                .HasDatabaseName("Stadium");
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Stadium)
                .WithMany()
                .HasForeignKey(m => m.StadiumId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasIndex(m => new { m.MatchId, m.PlayerId, m.TeamId })
                .HasDatabaseName("UK_MPM_Match_Player_Team")
                .IsUnique();
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasIndex(m => m.MatchId)
                .HasDatabaseName("Match");
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasOne(m => m.Match)
                .WithMany()
                .HasForeignKey(m => m.MatchId)
                .HasConstraintName("fk_mpm_match")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasIndex(m => m.PlayerId)
                .HasDatabaseName("Player");
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasOne(m => m.Player)
                .WithMany()
                .HasForeignKey(m => m.PlayerId)
                .HasConstraintName("fk_mpm_player")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasIndex(m => m.MatchId)
                .HasDatabaseName("Match");
            
            modelBuilder.Entity<MatchPlayerMap>()
                .HasOne(m => m.Team)
                .WithMany()
                .HasForeignKey(m => m.TeamId)
                .HasConstraintName("fk_mpm_team")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<DismissalMode>()
                .HasIndex(dm => new { dm.Name })
                .IsUnique();
            
            modelBuilder.Entity<BattingScore>()
                .HasIndex(bs => new { bs.MatchPlayerId, bs.Innings })
                .HasDatabaseName("UK_BS_Match_Player_Innings")
                .IsUnique();
            
            modelBuilder.Entity<BattingScore>()
                .HasIndex(bs => bs.MatchPlayerId)
                .HasDatabaseName("Match_Player");
            
            modelBuilder.Entity<BattingScore>()
                .HasOne(bs => bs.BatsmanMatchPlayer)
                .WithMany()
                .HasForeignKey(bs => bs.MatchPlayerId)
                .HasConstraintName("FK_BS_Match_Player")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<BattingScore>()
                .HasIndex(bs => bs.DismissalModeId)
                .HasDatabaseName("Dismissal_Mode");
            
            modelBuilder.Entity<BattingScore>()
                .HasOne(bs => bs.DismissalMode)
                .WithMany()
                .HasForeignKey(bs => bs.DismissalModeId)
                .HasConstraintName("FK_BS_Dismissal_Mode")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<BattingScore>()
                .HasIndex(bs => bs.BowlerId)
                .HasDatabaseName("Bowler");
            
            modelBuilder.Entity<BattingScore>()
                .HasOne(bs => bs.BowlerMatchPlayer)
                .WithMany()
                .HasForeignKey(bs => bs.BowlerId)
                .HasConstraintName("FK_BS_Bowler")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<BowlingFigure>()
                .HasIndex(bf => new { bf.MatchPlayerId, bf.Innings })
                .HasDatabaseName("UK_BF_Match_Player_Innings")
                .IsUnique();
            
            modelBuilder.Entity<BowlingFigure>()
                .HasIndex(bf => bf.MatchPlayerId)
                .HasDatabaseName("Match_Player");
            
            modelBuilder.Entity<BowlingFigure>()
                .HasOne(bf => bf.MatchPlayerMap)
                .WithMany()
                .HasForeignKey(bf => bf.MatchPlayerId)
                .HasConstraintName("FK_BF_Match_Player")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FielderDismissal>()
                .HasIndex(fd => new { fd.ScoreId, fd.MatchPlayerId })
                .HasDatabaseName("UK_FD_Score_Player_Team")
                .IsUnique();
            
            modelBuilder.Entity<FielderDismissal>()
                .HasIndex(fd => fd.ScoreId)
                .HasDatabaseName("Score");
            
            modelBuilder.Entity<FielderDismissal>()
                .HasOne(fd =>fd.BattingScore)
                .WithMany()
                .HasForeignKey(fd => fd.ScoreId)
                .HasConstraintName("FK_FD_Score")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FielderDismissal>()
                .HasIndex(fd => fd.MatchPlayerId)
                .HasDatabaseName("Match_Player");
            
            modelBuilder.Entity<FielderDismissal>()
                .HasOne(fd =>fd.MatchPlayerMap)
                .WithMany()
                .HasForeignKey(fd => fd.MatchPlayerId)
                .HasConstraintName("FK_FD_Match_Player")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ExtrasType>()
                .HasIndex(et => new { et.Name })
                .IsUnique();
            
            modelBuilder.Entity<Extras>()
                .HasIndex(e => new { e.MatchId, e.TypeId, e.BattingTeamId, e.Innings })
                .HasDatabaseName("UK_E_Match_Type_Batting_Innings")
                .IsUnique();
            
            modelBuilder.Entity<Extras>()
                .HasIndex(e => e.MatchId)
                .HasDatabaseName("Match");
            
            modelBuilder.Entity<Extras>()
                .HasOne(e => e.Match)
                .WithMany()
                .HasForeignKey(e => e.MatchId)
                .HasConstraintName("FK_E_Match")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Extras>()
                .HasIndex(e => e.TypeId)
                .HasDatabaseName("Type");
            
            modelBuilder.Entity<Extras>()
                .HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .HasConstraintName("FK_E_Type")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Extras>()
                .HasIndex(e => e.BattingTeamId)
                .HasDatabaseName("Batting_Team");
            
            modelBuilder.Entity<Extras>()
                .HasOne(e => e.BattingTeam)
                .WithMany()
                .HasForeignKey(e => e.BattingTeamId)
                .HasConstraintName("FK_E_Batting_Team")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Extras>()
                .HasIndex(e => e.MatchId)
                .HasDatabaseName("Match");
            
            modelBuilder.Entity<Extras>()
                .HasOne(e => e.BowlingTeam)
                .WithMany()
                .HasForeignKey(e => e.BowlingTeamId)
                .HasConstraintName("FK_E_Bowling_Team")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Captain>()
                .HasIndex(c => new { c.MatchPlayerId })
                .HasDatabaseName("UK_C_Match_Player")
                .IsUnique();
            
            modelBuilder.Entity<Captain>()
                .HasIndex(c => c.MatchPlayerId)
                .HasDatabaseName("Match_Player");
            
            modelBuilder.Entity<Captain>()
                .HasOne(c => c.MatchPlayerMap)
                .WithMany()
                .HasForeignKey(c => c.MatchPlayerId)
                .HasConstraintName("FK_C_Match_Player")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<WicketKeeper>()
                .HasIndex(wk => new { wk.MatchPlayerId })
                .HasDatabaseName("UK_WK_Match_Player")
                .IsUnique();
            
            modelBuilder.Entity<WicketKeeper>()
                .HasIndex(wk => wk.MatchPlayerId)
                .HasDatabaseName("Match_Player");
            
            modelBuilder.Entity<WicketKeeper>()
                .HasOne(wk => wk.MatchPlayerMap)
                .WithMany()
                .HasForeignKey(wk => wk.MatchPlayerId)
                .HasConstraintName("FK_WK_Match_Player")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ManOfTheMatch>()
                .HasIndex(motm => new { motm.MatchPlayerId })
                .HasDatabaseName("UK_MOTM_Match_Player")
                .IsUnique();
            
            modelBuilder.Entity<ManOfTheMatch>()
                .HasIndex(motm => motm.MatchPlayerId)
                .HasDatabaseName("Match_Player");
            
            modelBuilder.Entity<ManOfTheMatch>()
                .HasOne(motm => motm.MatchPlayerMap)
                .WithMany()
                .HasForeignKey(motm => motm.MatchPlayerId)
                .HasConstraintName("FK_MOTM_Match_Player")
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
