using SimpleUi.Abstracts;
using UI.MainMenu.Leaderboard.OSA;
using UI.Popups.Info;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.MainMenu.Leaderboard.Views
{
    public class LeaderboardView : UiView
    {
        [SerializeField] private DialogView dialogView;
        [SerializeField] private LeaderboardNodeItemView currentPlayerLeaderboardNodeItemView;
        [SerializeField] private LeaderboardOsaCollection leaderboardOsaCollection;

        public DialogView DialogView => dialogView;
        public LeaderboardNodeItemView CurrentLeaderboardNodeItemView => currentPlayerLeaderboardNodeItemView;
        public LeaderboardOsaCollection LeaderboardOsaCollection => leaderboardOsaCollection;
    }
}
