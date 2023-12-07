using System;
using UnityEngine;

namespace Models.Avatars
{
    [Serializable]
    public class AvatarVo
    {
        public int Id;
        public Sprite Sprite;
        public string Name;
        public string Description;
    }
}