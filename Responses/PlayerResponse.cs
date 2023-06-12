namespace HseFootball.Responses
{
    public record PlayerResponse(
        int PlayerId,
        string Name,
        string Surname,
        int? TeamId,
        string? Team,
        List<ShortNewsResponse> News
    );
}
