using System;
using System.Collections.Generic;
using Databases.Skills;
using Databases.Skills.Impls;
using Enums.Hero;
using Models.Avatars;
using Models.Skills;
using UI.MainMenu.Avatars.Vo;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.MainMenu.Avatars.Databases
{
    [CreateAssetMenu(menuName = "Databases/Avatars/AvatarDatabase", fileName = "AvatarDatabase")]
    public class AvatarDatabase : ScriptableObject, IAvatarDatabase
    {
        [SerializeField] private AvatarVo[] avatars;
        [SerializeField] private AvatarStyleSettingsVo[] avatarStyles;
        private Dictionary<int, AvatarVo> _avatarsDictionary;
        private Dictionary<bool, AvatarStyleSettingsVo> _avatarStylesDictionary;

        public AvatarVo[] Avatars => avatars;

        private void OnEnable()
        {
            _avatarsDictionary = new Dictionary<int, AvatarVo>();

            foreach (var avatar in avatars)
                _avatarsDictionary.Add(avatar.Id, avatar);

            _avatarStylesDictionary = new Dictionary<bool, AvatarStyleSettingsVo>();

            foreach (var style in avatarStyles)
                _avatarStylesDictionary.Add(style.IsAvailable, style);
        }

        public AvatarVo GetAvatarById(int id)
        {
            try
            {
                return _avatarsDictionary[id];
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"[{nameof(AvatarDatabase)}] AvatarVo by type {id.ToString()} was not present in the dictionary. {e.StackTrace}");
            }
        }

        public AvatarStyleSettingsVo GetStyleByType(bool type)
        {
            try
            {
                return _avatarStylesDictionary[type];
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"[{nameof(AvatarDatabase)}] AvatarStyleSettingsVo by type {type.ToString()} was not present in the dictionary. {e.StackTrace}");
            }
        }
    }
}