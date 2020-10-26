using UnityEngine;
using System;
using Invector.vCharacterController;
using static SKFramework.PLATE.EventPlate;

namespace SKFramework.EVENT
{
    public   class AnimStateMachineSUB : StateMachineBehaviour
    {
        public GameStates State = GameStates.isCombo | GameStates.OpenSkill;
       
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
           // SKF.Event.Fire(eEventsType.AnimSubMachineEnter, this);
            SKF.Event.Fire(IntancePlayerObject.intance.eWhenAnyAnimState, State);
        }
        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineExit(animator, stateMachinePathHash);
            SKF.Event.Fire(IntancePlayerObject.intance.eWhenAnyAnimState, State);
        }
    }


    [Flags]
    public enum GameStates
    {
        isCombo =0x1, OpenSkill = 0x2
    }
}
