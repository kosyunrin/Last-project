using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameName
{
    public class cGameManger
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
        public cGameManger(object any)
        {
            m_LoadedFlag.Clear();
            isLoadSuccess = false;
            //m_LoadedFlag.Add(1, false);
            GameEntry.Resource.LoadAsset(AssetUtility.GetSpriteAsset("ItemSprite"), OnLoadSpriteAssetSuccess);
        }
        public static SpritesAsset ItemsSprite { get; set; }

        public void Init()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            CreatPlayer();
            CreatEnemyBoss();


        }
        public void UpDate()
        {
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    m_LoadedFlag[1] = true;
            //}
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
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
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
        private void OnShowEntitySuccess(object sender, GameEventArgs ne)
        {
            ShowEntitySuccessEventArgs e = (ShowEntitySuccessEventArgs)ne;
            EntityData m_entityData = e.UserData as EntityData;
            m_LoadedFlag[m_entityData.Id] = true;


            var Loading=
            GameEntry.UI.GetUIForm(UIFormId.LoadingFrom);

            var xd = GameEntry.Entity.GetEntity(m_entityData.Id);
            if (xd)
            {
                if (Loading && xd.Id == -1)
                {
                    cLoadingForm logic = (cLoadingForm)Loading.UIForm.Logic;
                    logic.mPlayerManagaer = xd;
                }
            }
            
        }
        private void CreatPlayer()
        {
            GameEntry.Entity.CreatePlayerManager(new TestData(GameEntry.Entity.GenerateSerialId(), 10000)
            {
                Position = new Vector3(0, 5, 0),

            });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
        }
        private void CreatEnemyBoss()
        {
            GameEntry.Entity.CreatePlayerManager(new TestData(GameEntry.Entity.GenerateSerialId(), 10001)
            {
                Position = new Vector3(1.3f, 0, -17),

            });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
        }
    }

}
