using System;
using Core.Network.Models.Api.Dtos.Leaderboard;
using Core.Services;
using Databases.Rank;
using Ecs.Leaderboard.Utils;
using Enums.Timer;
using Services.Network;
using Services.Timer;
using UnityEngine;
using Zenject;

namespace Services.Leaderboard.Impls
{
    public class LeaderboardService : ILeaderboardService, IInitializable, IDisposable
    {
        private readonly INetworkCallbacks _networkCallbacks;
        private readonly INetworkRequests _networkRequests;
        private readonly LeaderboardContext _leaderboardContext;
        private readonly ITimerService _timerService;
        private readonly ITimeProvider _timeProvider;

        public LeaderboardService(
            INetworkCallbacks networkCallbacks,
            INetworkRequests networkRequests,
            LeaderboardContext leaderboardContext,
            ITimerService timerService,
            ITimeProvider timeProvider)
        {
            _networkCallbacks = networkCallbacks;
            _networkRequests = networkRequests;
            _leaderboardContext = leaderboardContext;
            _timerService = timerService;
            _timeProvider = timeProvider;
        }

        public void Initialize()
        {

            _networkCallbacks.OnGetLeaderboard += OnGetLeaderboard;
            _networkCallbacks.OnRefreshLeaderboard += OnGetLeaderboard;
        }

        public void Dispose()
        {
            _networkCallbacks.OnGetLeaderboard -= OnGetLeaderboard;
            _networkCallbacks.OnRefreshLeaderboard -= OnGetLeaderboard;
        }
        
        public void GetLeaderboardNodes(int lastNode) => _networkRequests.GetLeaderboardNodes(lastNode);

        public void RefreshLeaderboard(int nodesAmount) => _networkRequests.RefreshLeaderboard(nodesAmount);

        public void ResetDisplayedNodes()
        {
            var displayedEntities = _leaderboardContext.GetAllDisplayedGroup();
            foreach (var entity in displayedEntities) 
                entity.IsDisplayed = false;
        }

        private void OnGetLeaderboard(LeaderboardDto leaderboardDto)
        {
            SetTimer(leaderboardDto.nextRefreshTime);
            
            foreach (var dto in leaderboardDto.placement)
            {
                var entity = _leaderboardContext.GetEntityWithPlace(dto.place);
                
                if (entity != null)
                {
                    entity.ReplacePlace(dto.place);
                    entity.ReplaceAvatarId(dto.avatarId);
                    entity.ReplaceNickname(dto.nickname);
                    entity.ReplaceRating(dto.rating);
                    entity.IsLeaderboardCurrent = dto.isCurrent;
                }
                else
                {
                    _leaderboardContext.CreateLeaderboardNode(
                        dto.place,
                        dto.avatarId,
                        dto.nickname,
                        dto.rating,
                        dto.isCurrent);
                }
            }
        }

        private void SetTimer(long time)
        {
            time = (time - _timeProvider.NetworkTimeMM.Value) / 1000 + _timeProvider.NetworkTimeS.Value;
            _timerService.CreateBackwardTimer(time, ETimerType.Leaderboard,
                () => RefreshLeaderboard(_leaderboardContext.GetEntities().Length));
        }
    }
}