using UnityEngine;
using UnityEngine.Serialization;

namespace Databases.Leaderboard.Impls
{
    [CreateAssetMenu(menuName = "Databases/Leaderboard/LeaderboardDatabase", fileName = "LeaderboardDatabase")]
    public class LeaderboardDatabase : ScriptableObject, ILeaderboardDatabase
    {
        [SerializeField] private int maxNodesAmount;
        [SerializeField] private int topPlacesAmount;
        [SerializeField] private int defaultRequestIndex;

        public int MaxNodesAmount => maxNodesAmount;
        public int TopPlacesAmount => topPlacesAmount;
        public int DefaultRequestIndex => defaultRequestIndex;
    }
}
