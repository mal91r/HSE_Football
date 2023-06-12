namespace HseFootball.Responses
{
    public record TeamResponse(
        int Id,
        string Name,
        List<PlayerResponse> Players,
        List<ShortNewsResponse> News
        );
}
