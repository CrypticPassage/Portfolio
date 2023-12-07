using System;
using Core.Network.Models.Api.Dtos.Leaderboard;

namespace Services.Leaderboard
{
    public interface ILeaderboardService
    {
        void GetLeaderboardNodes(int lastNode);
        void ResetDisplayedNodes();
    }
}