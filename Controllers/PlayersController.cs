using HseFootball.Models;
using HseFootball.Requests;
using HseFootball.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HseFootball.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : Controller
    {
        public PlayersController(ILogger<PlayersController> logger)
        {

        }

        [HttpPost("new")]
        public PlayerResponse AddPlayer(
            AddPlayerRequest request)
        {
            var player = Player.AddPlayer(request.Name, request.Surname, request.Team);
            return new PlayerResponse(player.Id, player.Name, player.Surname, player.TeamId, player.Team?.Name, player.News?.Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList());
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            return View("/Views/PlayersPageView.cshtml",Player.GetAllPlayers()
                .Select(x => new PlayerResponse(x.Id, x.Name, x.Surname, x.TeamId, x.Team?.Name, x.News.Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList())).ToList());
        }

        [HttpPost("get-player")]
        public PlayerResponse GetPlayerByName(
            GetPlayerRequest request)
        {
            var player = Player.GetPlayerByName(request.Name, request.Surname);
            if (player == null)
            {
                throw new ArgumentException($"Игрок {request.Name} {request.Surname} не найден!");
            }
            return new PlayerResponse(player.Id, player.Name, player.Surname, player.TeamId, player.Team?.Name, player.News.Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult GetPlayerById(
            [FromRoute(Name="id")]int id)
        {
            var player = Player.GetPlayerById(id);
            if (player == null)
            {
                throw new ArgumentException($"Игрок c id={id} не найден!");
            }
            return View("/Views/PlayerPageView.cshtml",new PlayerResponse(player.Id, player.Name, player.Surname, player.TeamId, player.Team?.Name, player.News.Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList()));
        }

        [HttpPost("change-team")]
        public PlayerResponse ChangePlayerTeam(
            PlayerTeamRequest request)
        {
            var player = Player.ChangeTeam(request.PlayerRequest.Name, request.PlayerRequest.Surname, request.TeamRequest.Name);
            return new PlayerResponse(player.Id, player.Name, player.Surname, player.TeamId, player.Team?.Name, player.News.Select(n => new ShortNewsResponse(n.Id, n.Title, n.Text)).ToList());
        }

        [HttpDelete]
        public ActionResult ClearData()
        {
            Player.ClearData();
            return Ok();
        }

    }
}