using System;
using Core.Network.Models.Api.Dtos.Leaderboard;
using Databases.Leaderboard;
using Databases.Rank;
using Services.Leaderboard;
using Services.Network;
using Services.Scroll;
using Services.Sounds.Core;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using UI.MainMenu.Avatars.Databases;
using UI.MainMenu.Leaderboard.Views;
using UniRx;
using Zenject;

namespace UI.MainMenu.Leaderboard.Controllers.Impls
{
    public class LeaderboardController : UiController<LeaderboardView>, ILeaderboardController, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly INetworkCallbacks _networkCallbacks;
        private readonly LeaderboardContext _leaderboardContext;
        private readonly UserProfileContext _userProfileContext;
        private readonly ILeaderboardService _leaderboardService;
        private readonly IAvatarDatabase _avatarDatabase;
        private readonly IRankDatabase _rankDatabase;
        private readonly IScrollSoundService _scrollSoundService;
        private readonly ILeaderboardDatabase _leaderboardDatabase;

        private int nodesAmount;
        private int ?nodesOnLastRequestAmount;
        
        public LeaderboardController(
            SignalBus signalBus,
            INetworkCallbacks networkCallbacks,
            LeaderboardContext leaderboardContext,
            UserProfileContext userProfileContext,
            ILeaderboardService leaderboardService,
            IAvatarDatabase avatarDatabase,
            IRankDatabase rankDatabase,
            IScrollSoundService scrollSoundService,
            ILeaderboardDatabase leaderboardDatabase)
        {
            _signalBus = signalBus;
            _networkCallbacks = networkCallbacks;
            _leaderboardContext = leaderboardContext;
            _userProfileContext = userProfileContext;
            _leaderboardService = leaderboardService;
            _avatarDatabase = avatarDatabase;
            _rankDatabase = rankDatabase;
            _scrollSoundService = scrollSoundService;
            _leaderboardDatabase = leaderboardDatabase;
        }

        public void Initialize()
        {
            _networkCallbacks.OnGetLeaderboard += SetCurrentLeaderboardNode;
            _networkCallbacks.OnRefreshLeaderboard += SetCurrentLeaderboardNode;
            View.DialogView.BackButton.OnClickAsObservable().Subscribe(_ => _signalBus.BackWindow());
            View.LeaderboardOsaCollection.SubscribeOnInitialize(() =>
            {
                View.LeaderboardOsaCollection.UpdatedItem += OnLeaderboardNodeUpdatedItem;
                _leaderboardService.GetLeaderboardNodes(_leaderboardDatabase.DefaultRequestIndex);
                ResetOsa();
            });
            View.LeaderboardOsaCollection.OnVelocityChanged += _scrollSoundService.PlayLoopSound;
        }

        public void Dispose()
        {
            View.LeaderboardOsaCollection.UpdatedItem -= OnLeaderboardNodeUpdatedItem;
            _networkCallbacks.OnGetLeaderboard -= SetCurrentLeaderboardNode;
            _networkCallbacks.OnRefreshLeaderboard -= SetCurrentLeaderboardNode;
        }

        public void ResetOsa() => View.LeaderboardOsaCollection.Data.ResetItems(_leaderboardContext.GetEntities());

        protected virtual void OnLeaderboardNodeUpdatedItem(LeaderboardEntity entity, LeaderboardNodeItemView nodeItemView)
        {
            nodeItemView.Unlink();
            var avatarIcon = _avatarDatabase.GetAvatarById(entity.AvatarId.Value).Sprite;
            var badgeIcon = _rankDatabase.GetRankDataByRating(entity.Rating.Value).BadgeIcon;
            var username = entity.Nickname.Value;
            var rankScore = entity.RankScore.Value;
            var place = entity.Place.Value;
            nodeItemView.SetData(avatarIcon, badgeIcon,username, rankScore, place);
            
            if (int.Parse(entity.Place.Value) <= _leaderboardDatabase.TopPlacesAmount)
                entity.IsTopPlace = true;
            
            if (!entity.IsDisplayed)
            {
                entity.IsDisplayed = true;
                nodesAmount++;
            }
            
            nodeItemView.Link(entity, _leaderboardContext);
            var entitiesCount = _leaderboardContext.GetEntities().Length;
            
            if (CheckForUpdateConditions(nodesAmount, entitiesCount))
            {
                nodesOnLastRequestAmount = nodesAmount;
                _leaderboardService.GetLeaderboardNodes(nodesAmount);
            }
        }

        private void SetCurrentLeaderboardNode(LeaderboardDto leaderboardDto)
        {
            var rankData = _rankDatabase.GetRankDataByRating(leaderboardDto.currentUserPlacement.rating);
            var rankScore = leaderboardDto.currentUserPlacement.rating;
			
            if (rankData.IsMaxRank)
                rankScore -= rankData.LowerLimit;
            
            View.CurrentLeaderboardNodeItemView.SetData(
                _avatarDatabase.GetAvatarById(leaderboardDto.currentUserPlacement.avatarId).Sprite, 
                _rankDatabase.GetRankDataByRating(leaderboardDto.currentUserPlacement.rating).BadgeIcon,
                leaderboardDto.currentUserPlacement.nickname, 
                rankScore,
                leaderboardDto.currentUserPlacement.place);
        }

        private bool CheckForUpdateConditions(int nodesCount, int entitiesCount)
        {
            if (nodesCount == nodesOnLastRequestAmount)
                return false;
            
            if (nodesCount < entitiesCount)
                return false;

            if (nodesCount >= _leaderboardDatabase.MaxNodesAmount)
                return false;

            return true;
        }
    }
}
