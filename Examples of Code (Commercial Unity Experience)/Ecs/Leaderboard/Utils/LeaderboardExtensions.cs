namespace Ecs.Leaderboard.Utils
{
    public static class LeaderboardExtensions
    {
        public static LeaderboardEntity CreateLeaderboardNode(this LeaderboardContext context, 
            string place, int avatarId, string nickname, int rating, bool current)
        {
            var leaderboardNode = context.CreateEntity();
            
            leaderboardNode.AddPlace(place);
            leaderboardNode.AddAvatarId(avatarId);
            leaderboardNode.AddNickname(nickname);
            leaderboardNode.AddRating(rating);
            leaderboardNode.IsLeaderboardCurrent = current;
            
            return leaderboardNode;
        }
    }
}