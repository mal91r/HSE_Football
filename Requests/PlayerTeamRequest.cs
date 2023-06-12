namespace HseFootball.Requests
{
    public record PlayerTeamRequest(
            GetPlayerRequest PlayerRequest,
            GetTeamRequest TeamRequest
        );
}
