using HseFootball.Context;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HseFootball.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public List<News> News { get; set; }

        public static Player AddPlayer(string name, string surname, string? team = null)
        {
            Player player = new Player { Name = name, Surname = surname, News = new List<News>()};
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Players.Add(player);
                db.SaveChanges();
            }

            player = ChangeTeam(name, surname, team);
            return player;
        }

        public static IEnumerable<Player> GetAllPlayers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Players.Include(x => x.Team).Include(x => x.News).ToList();
            }
        }

        public static Player? GetPlayerByName(string name, string surname)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Players.Include(x => x.Team).Include(x => x.News).FirstOrDefault(x => x.Name == name && x.Surname == surname);
            }
        }

        public static Player? GetPlayerById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Players.Include(x => x.Team).Include(x => x.News).FirstOrDefault(x => x.Id == id);
            }
        }

        public static Player ChangeTeam(string playerName, string playerSurname, string teamName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var player = db.Players.Include(x => x.Team).Include(x => x.News).FirstOrDefault(x => x.Name == playerName && x.Surname == playerSurname);
                var team = db.Teams.Include(x => x.Players).Include(x => x.News).FirstOrDefault(x => x.Name == teamName);
                if (player.Team != team)
                {
                    player.Team = team;
                    db.SaveChanges();
                }
                return player;
            }
        }
        public static void ClearData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Players.RemoveRange(db.Players);
                db.SaveChanges();
            }
        }
    }
}
