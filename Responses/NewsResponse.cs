using HseFootball.Requests;

namespace HseFootball.Responses
{
    public record NewsResponse(
        int Id,
        string Title,
        string Text,
        List<NewsPlayerResponse> Players,
        List<NewsTeamResponse> Teams
        );

}
