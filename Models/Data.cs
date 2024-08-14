using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models
{
    public class Data : DbContext
    {
        public Data() { }

        public Data(DbContextOptionsBuilder op) { }

        //public Data(DbContextOptions options) : base(options) { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerFriend> PlayerFriends { get; set; }
        public DbSet<GameTeam> GameTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string constr = "database=HandFootScoring;server=adubzpc; Trusted_Connection=true;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(constr, op =>
            {

            });
        }

        
    }
}
