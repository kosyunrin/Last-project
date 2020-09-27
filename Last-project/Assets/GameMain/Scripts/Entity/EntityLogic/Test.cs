using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameName
{
    public class Test : Entity
    {
        private TestData mData = null;
        public Camera mMainCamera { get; set; }
        public GameObject mPlayer { get; set; }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            mMainCamera = this.GetComponentInChildren<Camera>();
            mPlayer = transform.Find("Player").gameObject;
            
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            mData = userData as TestData;
            mPlayer.transform.position = mData.Position;
            mPlayer.transform.rotation = mData.Rotation;
            Visible = false;

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}
