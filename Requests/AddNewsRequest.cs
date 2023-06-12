namespace HseFootball.Requests
{
    public record AddNewsRequest(
        string Title,
        string Text,
        List<GetPlayerRequest> Players,
        List<GetTeamRequest> Teams
        );
}
