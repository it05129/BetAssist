using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetStatsAssist.Classes;

namespace BetStatsAssistWeb.DataAccessLayer
{
    public class BetStatsAssistDal :DbContext
    {
        public DbSet<LEAGUE> Leagues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LEAGUE>().ToTable("LEAGUES");
            base.OnModelCreating(modelBuilder);
        }
    }
}
