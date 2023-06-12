using HseFootball.Context;
using HseFootball.Requests;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace HseFootball.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Player> Players { get; set; }
        public List<Team> Teams { get; set; }

        public static News AddNews(string title, string text, List<GetPlayerRequest> players, List<GetTeamRequest> teams)
        {
            var news = new News { Title = title, Text = text, Players = new List<Player>(), Teams = new List<Team>() };
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var playerRequest in players)
                {
                    var player = db.Players.Include(x => x.Team).Include(x => x.News).FirstOrDefault(x => x.Name == playerRequest.Name && x.Surname == playerRequest.Surname);
                    if (player != null)
                    {
                        news.Players.Add(player);
                    }
                }

                foreach (var teamRequest in teams)
                {
                    var team = db.Teams.Include(x => x.Players).Include(x => x.News).FirstOrDefault(x => x.Name == teamRequest.Name);
                    if (team != null)
                    {
                        news.Teams.Add(team);
                    }
                }

                db.News.Add(news);
                db.SaveChanges();
            }

            return news;
        }

        public static IEnumerable<News> GetAllNews()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var news = db.News.Include(p => p.Players).Include(p => p.Teams);
                return news.ToList();
            }
        }

        public static News? GetNewsById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.News.Include(n => n.Players).Include(n => n.Teams).FirstOrDefault(x => x.Id == id);
            }
        }

        public static void ClearData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.News.RemoveRange(db.News);
                db.SaveChanges();
            }
        }

    }
}
