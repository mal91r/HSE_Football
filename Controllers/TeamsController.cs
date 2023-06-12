using HseFootball.Models;
using HseFootball.Requests;
using HseFootball.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HseFootball.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        public TeamsController()
        {

        }

        [HttpPost("new")]
        public TeamResponse AddTeam(
            AddTeamRequest request)
        {
            var team = Team.AddTeam(request.Name);
            return new TeamResponse(team.Id, team.Name, team.Players
                .Select(p => new PlayerResponse(p.Id, p.Name, p.Surname, p.TeamId , p.Team?.Name, p.News
                    .Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList())).ToList(), team.News
                .Select(t => new ShortNewsResponse(t.Id, t.Title, t.Text)).ToList());
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            return View("/Views/TeamsPageView.cshtml", Team.GetAllTeams()
                .Select(x => new TeamResponse(x.Id, x.Name, x.Players.Select(p => new PlayerResponse(p.Id, p.Name, p.Surname, p.TeamId,p.Team?.Name, p.News
                .Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList())).ToList(), x.News
                .Select(t => new ShortNewsResponse(t.Id, t.Title, t.Text)).ToList())).ToList());
        }

        [HttpPost("get-team")]
        public TeamResponse GetTeamByName(
            GetTeamRequest request)
        {
            var team = Team.GetTeamByName(request.Name);
            if (team == null)
            {
                throw new ArgumentException($"Команда {request.Name} не найдена!");
            }

            return new TeamResponse(team.Id, team.Name, team.Players
                .Select(p => new PlayerResponse(p.Id, p.Name, p.Surname, p.TeamId, p.Team?.Name, p.News
                .Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList())).ToList(), team.News
                .Select(t => new ShortNewsResponse(t.Id, t.Title, t.Text)).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult GetTeamById(
            [FromRoute(Name="id")]int id)
        {
            var team = Team.GetTeamById(id);
            if (team == null)
            {
                throw new ArgumentException($"Команда с id={id} не найдена!");
            }

            return View("/Views/TeamPageView.cshtml", new TeamResponse(team.Id, team.Name, team.Players
                .Select(p => new PlayerResponse(p.Id, p.Name, p.Surname, p.TeamId, p.Team?.Name, p.News
                .Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList())).ToList(), team.News
                .Select(t => new ShortNewsResponse(t.Id, t.Title, t.Text)).ToList()));
        }

        [HttpDelete]
        public ActionResult ClearData()
        {
            Team.ClearData();
            return Ok();
        }
    }
}