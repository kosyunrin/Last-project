//------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace GameName
{
   
    public class SpritesAsset : ScriptableObject
    {
        /// <summary>
        /// 精灵数组
        /// </summary>
        [SerializeField] public List<SpriteItem> Sprites = new List<SpriteItem>();
    }
    [System.Serializable]
    public struct SpriteItem
    {
        /// <summary>
        /// 精灵ID
        /// </summary>
        public int  ID;

        /// <summary>
        /// 精灵
        /// </summary>
        public Sprite Sprite;
    }
}