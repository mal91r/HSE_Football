using HseFootball.Models;

namespace HseFootball.Requests
{
    public record GetPlayerRequest(
        string Name,
        string Surname
        );
}
