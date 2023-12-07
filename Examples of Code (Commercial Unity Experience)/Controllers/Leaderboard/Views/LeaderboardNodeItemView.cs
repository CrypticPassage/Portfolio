using System;
using Ecs.Utils.View.Impls;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils.OSA.Grid.Interfaces;
using Ecs.Leaderboard.Components;

namespace UI.MainMenu.Leaderboard.Views
{
    public class LeaderboardNodeItemView : LinkedUiView<LeaderboardEntity>, ILeaderboardCurrentAddedListener, ILeaderboardCurrentRemovedListener,
        ITopPlaceAddedListener, ITopPlaceRemovedListener, IGridItemView
    {
        [Header("Info")]
        [SerializeField] private TMP_Text leaderboardPlace;
        [SerializeField] private TMP_Text rankValue;
        [SerializeField] private TMP_Text nickname;
        [SerializeField] private Image playerAvatar;
        [SerializeField] private Image badgeImage;
        [Header("PlaceBackgrounds")]
        [SerializeField] private Image topPlaceBackground;
        [SerializeField] private Image currentPlaceBackground;
        [Header("LeaderboardBackgrounds")]
        [SerializeField] private Image topLeaderboardBackground;
        [SerializeField] private Image currentLeaderboardBackground;
        [Header("Others")]
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private GameObject content;
        
        public TMP_Text LeaderboardPlace => leaderboardPlace;
        public TMP_Text RankValue => rankValue;
        public TMP_Text Nickname => nickname;
        public Image PlayerAvatar => playerAvatar;
        public Image BadgeImage => badgeImage;
        public LayoutElement LayoutElement => layoutElement;
        public GameObject Content => content;

        protected override void Listen(LeaderboardEntity entity)
        {
            entity.AddLeaderboardCurrentAddedListener(this);
            entity.AddLeaderboardCurrentRemovedListener(this);
            entity.AddTopPlaceAddedListener(this);
            entity.AddTopPlaceRemovedListener(this);
            
            if(entity.IsTopPlace)
                OnTopPlaceAdded(entity);
            else
                OnTopPlaceRemoved(entity);

            if (entity.IsLeaderboardCurrent)
                OnLeaderboardCurrentAdded(entity);
            else
                OnLeaderboardCurrentRemoved(entity);
        }

        protected override void Unlisten(LeaderboardEntity entity)
        {
            entity.RemoveLeaderboardCurrentAddedListener(this);
            entity.RemoveLeaderboardCurrentRemovedListener(this);
            entity.RemoveTopPlaceAddedListener(this);
            entity.RemoveTopPlaceRemovedListener(this);
        }

        protected override void InternalClear() { }
        
        public void SetData(Sprite avatarIcon, Sprite badgeIcon, string nick, int rankScore, string place)
        {
            playerAvatar.sprite = avatarIcon;
            badgeImage.sprite = badgeIcon;
            nickname.text = nick;
            rankValue.text = rankScore.ToString();
            leaderboardPlace.text = place;
        }
        
        public void OnLeaderboardCurrentAdded(LeaderboardEntity entity)
        {
            currentPlaceBackground.enabled = true;
            currentLeaderboardBackground.enabled = true;
        }
        
        public void OnLeaderboardCurrentRemoved(LeaderboardEntity entity)
        {
            currentPlaceBackground.enabled = false;
            currentLeaderboardBackground.enabled = false;
            
        }
        
        public void OnTopPlaceAdded(LeaderboardEntity entity)
        {
            topPlaceBackground.enabled = true;
            topLeaderboardBackground.enabled = true;
            
        }
        
        public void OnTopPlaceRemoved(LeaderboardEntity entity)
        {
            topLeaderboardBackground.enabled = false;
            topPlaceBackground.enabled = false;
        }
    }
}
