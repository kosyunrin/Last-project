//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
using GameFramework.Event;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameName
{
    public class ProcedureMain : ProcedureBase
    {
        private cGameManger mGame = null;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }


        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
           mGame = new cGameManger(this);
           mGame.Init();
        }
        

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {

            mGame.Leave();
            //GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);



            
            
            base.OnLeave(procedureOwner, isShutdown);
           
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            mGame.UpDate();

            //if (m_StartGame)
            //{
            //    procedureOwner.SetData<VarInt>("NextSceneId", GameEntry.Config.GetInt("Scene.GameMain"));
            //    //procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
            //    ChangeState<ProcedureChangeScene>(procedureOwner);
            //}

        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            GameFrameworkLog.Info("ProceduralHallRoom OnEnter");

            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

        }

       
    }
}
