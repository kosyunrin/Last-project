using UnityEngine;
using UnityGameFramework.Runtime;
namespace GameName
{
    public class cMainMangerForm : UGuiForm
    {
        private UGuiForm lastLogic = null;
        public int CurentRoom { get; set; }

        [SerializeField]
        private Transform[] RoomTrans = null;
        [SerializeField]
        private Transform LiftTrans = null;
        [SerializeField]
        [Range(0.5f, 10.0f)]  private float LiftSpeed = 1.0f;
#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            CurentRoom = 0;

        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            UpdateLift(elapseSeconds);
        }

    public void ClickGameHall()
        {
            
            if (lastLogic)
            {
                GameEntry.UI.CloseUIForm(lastLogic);
            }
            GameEntry.UI.OpenUIForm(UIFormId.GameHallInMain);
            lastLogic = GameEntry.UI.GetUIForm(UIFormId.GameHallInMain);
        }
        public void ClickOpreateHall()
        {
            if (lastLogic)
            {
                GameEntry.UI.CloseUIForm(lastLogic);
            }
            GameEntry.UI.OpenUIForm(UIFormId.OpreateHallInMain);
            lastLogic = GameEntry.UI.GetUIForm(UIFormId.OpreateHallInMain);
        }
        public void ClickStoreHall()
        {
            if (lastLogic)
            {
                GameEntry.UI.CloseUIForm(lastLogic);
            }
            GameEntry.UI.OpenUIForm(UIFormId.StoreHallInMain);
            lastLogic = GameEntry.UI.GetUIForm(UIFormId.StoreHallInMain);
        }
        public void ClickSettingHall()
        {
            if (lastLogic)
            {
                GameEntry.UI.CloseUIForm(lastLogic);
            }
            GameEntry.UI.OpenUIForm(UIFormId.SettingHallInMain);
            lastLogic = GameEntry.UI.GetUIForm(UIFormId.SettingHallInMain);
        }
        public void CloseAllHall()
        {
            //GameEntry.UI.CloseUIForm(GameEntry.UI.GetUIForm(UIFormId.GameHallInMain));
            GameEntry.UI.CloseUIForm(GameEntry.UI.GetUIForm(UIFormId.OpreateHallInMain));
            GameEntry.UI.CloseUIForm(GameEntry.UI.GetUIForm(UIFormId.StoreHallInMain));
            GameEntry.UI.CloseUIForm(GameEntry.UI.GetUIForm(UIFormId.SettingHallInMain));
        }
        public void UpdateLift(float dt)
        {
            switch (CurentRoom)
            {

                case 0:
                    {
                        var TargetPos = RoomTrans[CurentRoom].position;

                        var newPos = Mathf.Lerp(LiftTrans.position.y, TargetPos.y, LiftSpeed * dt);
                        LiftTrans.SetPositionY(newPos);
                        var dis = LiftTrans.position.y - TargetPos.y;
                        if (Mathf.Abs(dis) <= 0.1f) { ClickGameHall(); CurentRoom = 8; } 
                    }
                    break;
                case 1:
                    {
                        var TargetPos = RoomTrans[CurentRoom].position;

                        var newPos = Mathf.Lerp(LiftTrans.position.y, TargetPos.y, LiftSpeed * dt);
                        LiftTrans.SetPositionY(newPos);
                        var dis = LiftTrans.position.y - TargetPos.y;
                        if (Mathf.Abs(dis) <= 0.1f) { ClickOpreateHall(); CurentRoom = 8; }
                    }
                    break;
                case 2:
                    {
                        var TargetPos = RoomTrans[CurentRoom].position;

                        var newPos = Mathf.Lerp(LiftTrans.position.y, TargetPos.y, LiftSpeed * dt);
                        LiftTrans.SetPositionY(newPos);
                        var dis = LiftTrans.position.y - TargetPos.y;
                        if (Mathf.Abs(dis) <= 0.1f) { ClickStoreHall(); CurentRoom = 8;return; }
                    }
                    break;
                case 3:
                    {
                        var TargetPos = RoomTrans[CurentRoom].position;

                        var newPos = Mathf.Lerp(LiftTrans.position.y, TargetPos.y, LiftSpeed * dt);
                        LiftTrans.SetPositionY(newPos);
                        var dis = LiftTrans.position.y - TargetPos.y;
                        if (Mathf.Abs(dis) <= 0.1f) { ClickSettingHall(); CurentRoom = 8;  }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
