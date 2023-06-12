using HseFootball.Context;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace HseFootball.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public List<News> News { get; set; }
        public static Team AddTeam(string name)
        {
            Team team = new Team { Name = name, Players = new List<Player>(), News = new List<News>()};
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Teams.Add(team);
                db.SaveChanges();
            }
            return team;
        }

        public static IEnumerable<Team> GetAllTeams()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Teams.Include(x => x.Players).ThenInclude(x => x.News).Include(x => x.News).ToList();
            }
        }

        public static Team? GetTeamByName(string? name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var team = db.Teams.Include(x => x.Players).ThenInclude(x => x.News).Include(x => x.News).FirstOrDefault(x => x.Name == name);
                return team;
            }
        }

        public static Team? GetTeamById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var team = db.Teams.Include(x => x.Players).ThenInclude(x => x.News).Include(x => x.News).FirstOrDefault(x => x.Id == id);
                return team;
            }
        }

        public static void ClearData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Teams.RemoveRange(db.Teams);
                db.SaveChanges();
            }
        }
    }
}
