//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
using GameFramework.Event;
using UnityGameFramework.Runtime;
using GameFramework;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameName
{
    public class ProcedureMenu : ProcedureBase
    {
        private AboutForm m_gsf = null;
        private bool m_StartGame = false;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void StartGame()
        {
            m_StartGame = true;
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameFrameworkLog.Info("ProceduralHallRoom OnEnter");
            // GameEntry.UI.OpenUIForm(UIFormId.MenuForm,this);
            //GameEntry.UI.OpenUIForm(UIFormId.GameStartForm, this);
            GameEntry.UI.OpenUIForm(UIFormId.AboutStorryForm, this);


            GameFramework.DataTable.IDataTable<DRMyPackData> dtData = GameEntry.DataTable.GetDataTable<DRMyPackData>();
            DRMyPackData drdata = dtData.GetDataRow(50);
            Log.Info(drdata.Name);

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            var uiforms = GameEntry.UI.GetAllLoadedUIForms();
            foreach (UIForm child in uiforms)
            {
                var ugui = child.GetComponent<UGuiForm>();
                if (ugui)
                {
                    ugui.Close(isShutdown);
                }
            }
    
            
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
           
            if (m_StartGame)
            {
                procedureOwner.SetData<VarInt>("NextSceneId", GameEntry.Config.GetInt("Scene.GameMain"));
                procedureOwner.SetData<VarInt>("isLodding", 0);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            GameFrameworkLog.Info("ProceduralHallRoom OnEnter");

            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
           
              m_gsf = (AboutForm)ne.UIForm.Logic;
        }
    }
}
