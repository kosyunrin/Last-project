using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;

namespace GameName
{
    public class cHallManger
    {
        /// <summary>
        /// 加载图标成功的回调函数
        /// </summary>
        private LoadAssetCallbacks
           OnLoadSpriteAssetSuccess = new LoadAssetCallbacks(loadSpritsAssetCallback);
        /// <summary>
        /// 加载实体成功标识字典,int为id,bool为成功与否的布尔类型
        /// </summary>
        private Dictionary<int, bool> m_LoadedFlag = new Dictionary<int, bool>();
        private bool isLoadSuccess = false;
        public cHallManger(object any)
        {
            m_LoadedFlag.Clear();
            isLoadSuccess = false;
            m_LoadedFlag.Add(1, false);
            GameEntry.Resource.LoadAsset(AssetUtility.GetSpriteAsset("ItemSprite"), OnLoadSpriteAssetSuccess);
        }
        public static SpritesAsset ItemsSprite { get; set; }
       

        public void UpDate()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                m_LoadedFlag[1] = true;
            }
            if (!isLoadSuccess)
            {
                IEnumerator<bool> iter = m_LoadedFlag.Values.GetEnumerator();
                while (iter.MoveNext())
                {
                    if (!iter.Current)
                    {
                        return;
                    }
                }
                Debug.Log(0);
                GameEntry.Event.Fire(this, ReferencePool.Acquire<LoadNextResourcesSuccessArgs>().Fill(true));
                isLoadSuccess = true;
            }
        }
        public void Leave()
        {
        }

        /// <summary>
        /// 加载自定义asset成功回调函数
        /// </summary>
        /// <param name="assetname"></param>
        /// <param name="asset"></param>
        /// <param name="duration"></param>
        /// <param name="userdata"></param>
        private static void loadSpritsAssetCallback(string assetname, object asset, float duration, object userdata)
        {
            ItemsSprite = asset as SpritesAsset;
        }
    }

}
