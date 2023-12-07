namespace Databases.Leaderboard
{
    public interface ILeaderboardDatabase
    {
        int MaxNodesAmount { get; }
        int TopPlacesAmount { get; }
        int DefaultRequestIndex { get; }
    }
}
