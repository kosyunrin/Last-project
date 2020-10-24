using UnityEngine;
using UnityEngine.UI;



namespace GameName
{

    public class cItemHouseForm : UGuiForm
    {
        [SerializeField] ScrollRect Scroll = null;
        private RectTransform MoveScroll=null;
        private float YilieSize=0;
        //private GameObject Grids = null;
 


        public void ExitButtonAction()
        {
            GameEntry.UI.OpenUIForm(UIFormId.OpreateHallInMain);
        }
        /// <summary>
        /// Add Items Grids
        /// </summary>
        public void AddGrids(int num)
        {
            MoveScroll.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MoveScroll.rect.width + num*YilieSize);

        }


#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
            protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            MoveScroll = Scroll.content;
            YilieSize = 80 + 15;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
            protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);


        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
            protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);

        }
    }

}