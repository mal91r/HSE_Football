using HseFootball.Models;

namespace HseFootball.Requests
{
    public record AddPlayerRequest(
        string Name,
        string Surname,
        string? Team
        );
}
