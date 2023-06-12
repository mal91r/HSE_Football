using HseFootball.Models;
using HseFootball.Requests;
using HseFootball.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HseFootball.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        [HttpPost("new")]
        public NewsResponse AddNews(
            AddNewsRequest request)
        {
            var news = News.AddNews(request.Title, request.Text, request.Players, request.Teams);
            return new NewsResponse(news.Id, news.Title, news.Text,
                news.Players.Select(x => new NewsPlayerResponse(x.Id, x.Name, x.Surname)).ToList(),
                news.Teams.Select(x => new NewsTeamResponse(x.Id, x.Name)).ToList());
        }

        [HttpGet]
        public ActionResult GetAllNews()
        {
            var news = News.GetAllNews()
                .Select(n => new NewsResponse(n.Id, n.Title, n.Text, n.Players
                    .Select(p => new NewsPlayerResponse(p.Id, p.Name, p.Surname)).ToList(), n.Teams
                    .Select(t => new NewsTeamResponse(t.Id, t.Name)).ToList())).ToList();
            return View("/Views/NewsPaperPageView.cshtml", news);
        }

        [HttpGet("{id}")]
        public ActionResult GetNewsById(
            [FromRoute(Name = "id")] int id)
        {
            var news = News.GetNewsById(id);
            if (news == null)
            {
                throw new ArgumentException($"Новость c id={id} не найдена!");
            }
            return View("/Views/NewsPageView.cshtml", new NewsResponse(news.Id, news.Title, news.Text, 
                news.Players.Select(x => new NewsPlayerResponse(x.Id, x.Name, x.Surname)).ToList(), 
                news.Teams.Select(x => new NewsTeamResponse(x.Id, x.Name)).ToList()));
        }

        [HttpDelete]
        public ActionResult ClearData()
        {
            News.ClearData();
            return Ok();
        }
    }
}
