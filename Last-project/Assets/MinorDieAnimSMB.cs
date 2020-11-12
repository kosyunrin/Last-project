using UnityEngine;
using System.Collections.Generic;
using SKFramework;

namespace ENEMY_AI
{
    public class MinorDieAnimSMB : skAnimBaseSMB<SKMinorMonsterLogic>
    {
        private float time = 0;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateEnter(animator, stateInfo, layerIndex);
            //animator.ResetTrigger(SKMinorMonsterLogic.hashTDeath);
            //SKF.Game.HiddenEnemyXiaoBing(m_Logic.mController);
            time = 0.0f;
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
            if(time<5.0f)
            {
                time += Time.deltaTime;
            }
            else
            {
                SKF.Game.HiddenEnemyXiaoBing(m_Logic.mController);
            }
        }


        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateExit(animator, stateInfo, layerIndex);
        }


    }


}




